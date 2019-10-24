
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

import './OrderContentMaster.scss';
import orderContentMasterRepository from './OrderContentMasterRepository';
import { ORDER_CONTENT_ROUTE } from 'config/route-consts';
import { OrderContent } from 'models/OrderContent';
import { OrderContentSearch } from 'models/OrderContentSearch';

import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';

const {Column} = Table;

function OrderContentMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ORDER_CONTENT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new OrderContentSearch());
  }

  function reloadList() {
    setSearch(new OrderContentSearch(search));
  }

  function handleDelete(orderContent: OrderContent) {
    return () => {
      confirm({
        title: translate('orderContentMaster.deletion.title'),
        content: translate('orderContentMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          orderContentMasterRepository.delete(orderContent)
            .subscribe(
              () => {
                notification.success({
                  message: translate('orderContentMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('orderContentMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<OrderContentSearch>(new OrderContentSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<OrderContent, OrderContentSearch>(
    search,
    setSearch,
    orderContentMasterRepository.list,
    orderContentMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('orderContentMaster.title')}
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
                title={translate('orderContentMaster.index')}
                render={renderIndex<OrderContent, OrderContentSearch>(search)}
        />
        
         <Column key="itemName"
                dataIndex="itemName"
                title={translate('orderContentMaster.itemName')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('itemName', sorter)}
        />
         <Column key="firstVersion"
                dataIndex="firstVersion"
                title={translate('orderContentMaster.firstVersion')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('firstVersion', sorter)}
        />
         <Column key="secondVersion"
                dataIndex="secondVersion"
                title={translate('orderContentMaster.secondVersion')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('secondVersion', sorter)}
        />
         <Column key="thirdVersion"
                dataIndex="thirdVersion"
                title={translate('orderContentMaster.thirdVersion')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('thirdVersion', sorter)}
        />
         <Column key="price"
                dataIndex="price"
                title={translate('orderContentMaster.price')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('price', sorter)}
        />
         <Column key="discountPrice"
                dataIndex="discountPrice"
                title={translate('orderContentMaster.discountPrice')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('discountPrice', sorter)}
        />
         <Column key="order"
                dataIndex="order"
                title={translate('orderContentMaster.order')}
                sorter
                sortOrder={getColumnSortOrder<OrderContent>('order', sorter)}
                render={(order: Order) => {
                       return (
                         <>
                           {order && order.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, orderContent: OrderContent) => {
                  return (
                    <>
                      <Link to={path.join(ORDER_CONTENT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(orderContent)}>
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

export default withRouter(OrderContentMaster);
