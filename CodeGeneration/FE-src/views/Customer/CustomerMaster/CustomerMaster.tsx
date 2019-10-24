
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

import './CustomerMaster.scss';
import customerMasterRepository from './CustomerMasterRepository';
import { CUSTOMER_ROUTE } from 'config/route-consts';
import { Customer } from 'models/Customer';
import { CustomerSearch } from 'models/CustomerSearch';


const {Column} = Table;

function CustomerMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(CUSTOMER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new CustomerSearch());
  }

  function reloadList() {
    setSearch(new CustomerSearch(search));
  }

  function handleDelete(customer: Customer) {
    return () => {
      confirm({
        title: translate('customerMaster.deletion.title'),
        content: translate('customerMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          customerMasterRepository.delete(customer)
            .subscribe(
              () => {
                notification.success({
                  message: translate('customerMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('customerMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<CustomerSearch>(new CustomerSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Customer, CustomerSearch>(
    search,
    setSearch,
    customerMasterRepository.list,
    customerMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('customerMaster.title')}
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
                title={translate('customerMaster.index')}
                render={renderIndex<Customer, CustomerSearch>(search)}
        />
        
         <Column key="username"
                dataIndex="username"
                title={translate('customerMaster.username')}
                sorter
                sortOrder={getColumnSortOrder<Customer>('username', sorter)}
        />
         <Column key="displayName"
                dataIndex="displayName"
                title={translate('customerMaster.displayName')}
                sorter
                sortOrder={getColumnSortOrder<Customer>('displayName', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, customer: Customer) => {
                  return (
                    <>
                      <Link to={path.join(CUSTOMER_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(customer)}>
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

export default withRouter(CustomerMaster);
