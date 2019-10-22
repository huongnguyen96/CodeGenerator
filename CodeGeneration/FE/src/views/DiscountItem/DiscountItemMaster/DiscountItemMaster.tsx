
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle/CardTitle';
import {useList} from 'core/hooks/useList';
import {confirm, getColumnSortOrder, notification, renderIndex } from 'helpers';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import { Link,RouteComponentProps, withRouter } from 'react-router-dom';

import './DiscountItemMaster.scss';
import discountItemMasterRepository from './DiscountItemMasterRepository';
import { DISCOUNT_ITEM_ROUTE } from 'config/route-consts';
import { DiscountItem } from 'models/DiscountItem';
import { DiscountItemSearch } from 'models/DiscountItemSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

const {Column} = Table;

function DiscountItemMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(DISCOUNT_ITEM_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DiscountItemSearch());
  }

  function reloadList() {
    setSearch(new DiscountItemSearch(search));
  }

  function handleDelete(discountItem: DiscountItem) {
    return () => {
      confirm({
        title: translate('discountItemMaster.deletion.title'),
        content: translate('discountItemMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          discountItemMasterRepository.delete(discountItem)
            .subscribe(
              () => {
                notification.success({
                  message: translate('discountItemMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('discountItemMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<DiscountItemSearch>(new DiscountItemSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<DiscountItem, DiscountItemSearch>(
    search,
    setSearch,
    discountItemMasterRepository.list,
    discountItemMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('discountItemMaster.title')}
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
                title={translate('discountItemMaster.index')}
                render={renderIndex<DiscountItem, DiscountItemSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('discountItemMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('id', sorter)}
        />
         <Column key="unitId"
                dataIndex="unitId"
                title={translate('discountItemMaster.unitId')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('unitId', sorter)}
        />
         <Column key="discountValue"
                dataIndex="discountValue"
                title={translate('discountItemMaster.discountValue')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('discountValue', sorter)}
        />
         <Column key="discountId"
                dataIndex="discountId"
                title={translate('discountItemMaster.discountId')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('discountId', sorter)}
        />
         <Column key="discount"
                dataIndex="discount"
                title={translate('discountItemMaster.discount')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('discount', sorter)}
                render={(discount: Discount) => {
                       return (
                         <>
                           {discount.id}
                         </>
                       );
                     }}
        />
         <Column key="unit"
                dataIndex="unit"
                title={translate('discountItemMaster.unit')}
                sorter
                sortOrder={getColumnSortOrder<DiscountItem>('unit', sorter)}
                render={(unit: Unit) => {
                       return (
                         <>
                           {unit.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, discountItem: DiscountItem) => {
                  return (
                    <>
                      <Link to={path.join(DISCOUNT_ITEM_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(discountItem)}>
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

export default withRouter(DiscountItemMaster);
