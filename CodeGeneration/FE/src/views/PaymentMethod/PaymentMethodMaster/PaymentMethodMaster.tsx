
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

import { PAYMENT_METHOD_ROUTE } from 'config/route-consts';
import { PaymentMethod } from 'models/PaymentMethod';
import { PaymentMethodSearch } from 'models/PaymentMethodSearch';
import './PaymentMethodMaster.scss';
import paymentMethodMasterRepository from './PaymentMethodMasterRepository';

const {Column} = Table;

function PaymentMethodMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PAYMENT_METHOD_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new PaymentMethodSearch());
  }

  function reloadList() {
    setSearch(new PaymentMethodSearch(search));
  }

  function handleDelete(paymentMethod: PaymentMethod) {
    return () => {
      confirm({
        title: translate('paymentMethodMaster.deletion.title'),
        content: translate('paymentMethodMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          paymentMethodMasterRepository.delete(paymentMethod)
            .subscribe(
              () => {
                notification.success({
                  message: translate('paymentMethodMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('paymentMethodMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<PaymentMethodSearch>(new PaymentMethodSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<PaymentMethod, PaymentMethodSearch>(
    search,
    setSearch,
    paymentMethodMasterRepository.list,
    paymentMethodMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('paymentMethodMaster.title')}
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
                title={translate('paymentMethodMaster.index')}
                render={renderIndex<PaymentMethod, PaymentMethodSearch>(search)}
        />

         <Column key="code"
                dataIndex="code"
                title={translate('paymentMethodMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<PaymentMethod>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('paymentMethodMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<PaymentMethod>('name', sorter)}
        />
         <Column key="description"
                dataIndex="description"
                title={translate('paymentMethodMaster.description')}
                sorter
                sortOrder={getColumnSortOrder<PaymentMethod>('description', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, paymentMethod: PaymentMethod) => {
                  return (
                    <>
                      <Link to={path.join(PAYMENT_METHOD_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(paymentMethod)}>
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

export default withRouter(PaymentMethodMaster);
