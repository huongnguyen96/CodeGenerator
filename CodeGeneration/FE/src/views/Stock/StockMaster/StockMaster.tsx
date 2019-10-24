
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle/CardTitle';
import {useList} from 'core/hooks/useList';
import {confirm, getColumnSortOrder, notification, renderIndex } from 'helpers';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import { Link, RouteComponentProps, withRouter } from 'react-router-dom';

import { STOCK_ROUTE } from 'config/route-consts';
import { Stock } from 'models/Stock';
import { StockSearch } from 'models/StockSearch';
import './StockMaster.scss';
import stockMasterRepository from './StockMasterRepository';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
import {WarehouseSearch} from 'models/WarehouseSearch';

const {Column} = Table;

function StockMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(STOCK_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new StockSearch());
  }

  function reloadList() {
    setSearch(new StockSearch(search));
  }

  function handleDelete(stock: Stock) {
    return () => {
      confirm({
        title: translate('stockMaster.deletion.title'),
        content: translate('stockMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          stockMasterRepository.delete(stock)
            .subscribe(
              () => {
                notification.success({
                  message: translate('stockMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('stockMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<StockSearch>(new StockSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Stock, StockSearch>(
    search,
    setSearch,
    stockMasterRepository.list,
    stockMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('stockMaster.title')}
                 allowAdd
                 onAdd={handleAdd}
                 allowClear
                 onClear={handleClear}
      />
    }>
      <Table dataSource={list}
             rowKey="id"
             loading={loading}
             onChange={handleChange}
             pagination={{
               total,
               pageSize: search.take,
             }}
      >
        <Column key="index"
                title={translate('stockMaster.index')}
                render={renderIndex<Stock, StockSearch>(search)}
        />

         <Column key="quantity"
                dataIndex="quantity"
                title={translate('stockMaster.quantity')}
                sorter
                sortOrder={getColumnSortOrder<Stock>('quantity', sorter)}
        />
         <Column key="item"
                dataIndex="item"
                title={translate('stockMaster.item')}
                sorter
                sortOrder={getColumnSortOrder<Stock>('item', sorter)}
                render={(item: Item) => {
                       return (
                         <>
                           {item && item.id}
                         </>
                       );
                     }}
        />
         <Column key="warehouse"
                dataIndex="warehouse"
                title={translate('stockMaster.warehouse')}
                sorter
                sortOrder={getColumnSortOrder<Stock>('warehouse', sorter)}
                render={(warehouse: Warehouse) => {
                       return (
                         <>
                           {warehouse && warehouse.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, stock: Stock) => {
                  return (
                    <>
                      <Link to={path.join(STOCK_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(stock)}>
                        {translate('general.actions.delete')}
                      </Button>
                    </>
                  );
                }}
        />
      </Table>
    </Card>
  );
}

export default withRouter(StockMaster);
