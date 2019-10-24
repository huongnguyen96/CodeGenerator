
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

import './OrderStatusMaster.scss';
import orderStatusMasterRepository from './OrderStatusMasterRepository';
import { ORDER_STATUS_ROUTE } from 'config/route-consts';
import { OrderStatus } from 'models/OrderStatus';
import { OrderStatusSearch } from 'models/OrderStatusSearch';


const {Column} = Table;

function OrderStatusMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ORDER_STATUS_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new OrderStatusSearch());
  }

  function reloadList() {
    setSearch(new OrderStatusSearch(search));
  }

  function handleDelete(orderStatus: OrderStatus) {
    return () => {
      confirm({
        title: translate('orderStatusMaster.deletion.title'),
        content: translate('orderStatusMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          orderStatusMasterRepository.delete(orderStatus)
            .subscribe(
              () => {
                notification.success({
                  message: translate('orderStatusMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('orderStatusMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<OrderStatusSearch>(new OrderStatusSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<OrderStatus, OrderStatusSearch>(
    search,
    setSearch,
    orderStatusMasterRepository.list,
    orderStatusMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('orderStatusMaster.title')}
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
                title={translate('orderStatusMaster.index')}
                render={renderIndex<OrderStatus, OrderStatusSearch>(search)}
        />
        
         <Column key="code"
                dataIndex="code"
                title={translate('orderStatusMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<OrderStatus>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('orderStatusMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<OrderStatus>('name', sorter)}
        />
         <Column key="description"
                dataIndex="description"
                title={translate('orderStatusMaster.description')}
                sorter
                sortOrder={getColumnSortOrder<OrderStatus>('description', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, orderStatus: OrderStatus) => {
                  return (
                    <>
                      <Link to={path.join(ORDER_STATUS_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(orderStatus)}>
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

export default withRouter(OrderStatusMaster);
