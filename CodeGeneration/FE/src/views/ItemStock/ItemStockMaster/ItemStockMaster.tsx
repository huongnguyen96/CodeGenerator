
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

import { ITEM_STOCK_ROUTE } from 'config/route-consts';
import { ItemStock } from 'models/ItemStock';
import { ItemStockSearch } from 'models/ItemStockSearch';
import './ItemStockMaster.scss';
import itemStockMasterRepository from './ItemStockMasterRepository';

const {Column} = Table;

function ItemStockMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ITEM_STOCK_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ItemStockSearch());
  }

  function reloadList() {
    setSearch(new ItemStockSearch(search));
  }

  function handleDelete(itemStock: ItemStock) {
    return () => {
      confirm({
        title: translate('itemStockMaster.deletion.title'),
        content: translate('itemStockMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          itemStockMasterRepository.delete(itemStock)
            .subscribe(
              () => {
                notification.success({
                  message: translate('itemStockMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('itemStockMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ItemStockSearch>(new ItemStockSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ItemStock, ItemStockSearch>(
    search,
    setSearch,
    itemStockMasterRepository.list,
    itemStockMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('itemStockMaster.title')}
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
             }}
      >
        <Column key="index"
                title={translate('itemStockMaster.index')}
                render={renderIndex<ItemStock, ItemStockSearch>(search)}
        />

         <Column key="id"
                dataIndex="id"
                title={translate('itemStockMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('id', sorter)}
        />
         <Column key="itemId"
                dataIndex="itemId"
                title={translate('itemStockMaster.itemId')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('itemId', sorter)}
        />
         <Column key="warehouseId"
                dataIndex="warehouseId"
                title={translate('itemStockMaster.warehouseId')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('warehouseId', sorter)}
        />
         <Column key="unitOfMeasureId"
                dataIndex="unitOfMeasureId"
                title={translate('itemStockMaster.unitOfMeasureId')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('unitOfMeasureId', sorter)}
        />
         <Column key="quantity"
                dataIndex="quantity"
                title={translate('itemStockMaster.quantity')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('quantity', sorter)}
        />
         <Column key="item"
                dataIndex="item"
                title={translate('itemStockMaster.item')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('item', sorter)}
                render={(item: Item) => {
                       return (
                         <>
                           {item.name}
                         </>
                       );
                     }}
        />
         <Column key="unitOfMeasure"
                dataIndex="unitOfMeasure"
                title={translate('itemStockMaster.unitOfMeasure')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('unitOfMeasure', sorter)}
                render={(unitOfMeasure: UnitOfMeasure) => {
                       return (
                         <>
                           {unitOfMeasure.name}
                         </>
                       );
                     }}
        />
         <Column key="warehouse"
                dataIndex="warehouse"
                title={translate('itemStockMaster.warehouse')}
                sorter
                sortOrder={getColumnSortOrder<ItemStock>('warehouse', sorter)}
                render={(warehouse: Warehouse) => {
                       return (
                         <>
                           {warehouse.name}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, itemStock: ItemStock) => {
                  return (
                    <>
                      <Link to={path.join(ITEM_STOCK_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(itemStock)}>
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

export default withRouter(ItemStockMaster);
