
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

import './OrderMaster.scss';
import orderMasterRepository from './OrderMasterRepository';
import { ORDER_ROUTE } from 'config/route-consts';
import { Order } from 'models/Order';
import { OrderSearch } from 'models/OrderSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';

const {Column} = Table;

function OrderMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ORDER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new OrderSearch());
  }

  function reloadList() {
    setSearch(new OrderSearch(search));
  }

  function handleDelete(order: Order) {
    return () => {
      confirm({
        title: translate('orderMaster.deletion.title'),
        content: translate('orderMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          orderMasterRepository.delete(order)
            .subscribe(
              () => {
                notification.success({
                  message: translate('orderMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('orderMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<OrderSearch>(new OrderSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Order, OrderSearch>(
    search,
    setSearch,
    orderMasterRepository.list,
    orderMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('orderMaster.title')}
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
                title={translate('orderMaster.index')}
                render={renderIndex<Order, OrderSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('orderMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Order>('id', sorter)}
        />
         <Column key="customerId"
                dataIndex="customerId"
                title={translate('orderMaster.customerId')}
                sorter
                sortOrder={getColumnSortOrder<Order>('customerId', sorter)}
        />
         <Column key="createdDate"
                dataIndex="createdDate"
                title={translate('orderMaster.createdDate')}
                sorter
                sortOrder={getColumnSortOrder<Order>('createdDate', sorter)}
        />
         <Column key="voucherCode"
                dataIndex="voucherCode"
                title={translate('orderMaster.voucherCode')}
                sorter
                sortOrder={getColumnSortOrder<Order>('voucherCode', sorter)}
        />
         <Column key="total"
                dataIndex="total"
                title={translate('orderMaster.total')}
                sorter
                sortOrder={getColumnSortOrder<Order>('total', sorter)}
        />
         <Column key="voucherDiscount"
                dataIndex="voucherDiscount"
                title={translate('orderMaster.voucherDiscount')}
                sorter
                sortOrder={getColumnSortOrder<Order>('voucherDiscount', sorter)}
        />
         <Column key="campaignDiscount"
                dataIndex="campaignDiscount"
                title={translate('orderMaster.campaignDiscount')}
                sorter
                sortOrder={getColumnSortOrder<Order>('campaignDiscount', sorter)}
        />
         <Column key="customer"
                dataIndex="customer"
                title={translate('orderMaster.customer')}
                sorter
                sortOrder={getColumnSortOrder<Order>('customer', sorter)}
                render={(customer: Customer) => {
                       return (
                         <>
                           {customer.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, order: Order) => {
                  return (
                    <>
                      <Link to={path.join(ORDER_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(order)}>
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

export default withRouter(OrderMaster);
