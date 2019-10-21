
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

import './CustomerGroupingMaster.scss';
import customerGroupingMasterRepository from './CustomerGroupingMasterRepository';
import { CUSTOMER_GROUPING_ROUTE } from 'config/route-consts';
import { CustomerGrouping } from 'models/CustomerGrouping';
import { CustomerGroupingSearch } from 'models/CustomerGroupingSearch';

const {Column} = Table;

function CustomerGroupingMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(CUSTOMER_GROUPING_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new CustomerGroupingSearch());
  }

  function reloadList() {
    setSearch(new CustomerGroupingSearch(search));
  }

  function handleDelete(customerGrouping: CustomerGrouping) {
    return () => {
      confirm({
        title: translate('customerGroupingMaster.deletion.title'),
        content: translate('customerGroupingMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          customerGroupingMasterRepository.delete(customerGrouping)
            .subscribe(
              () => {
                notification.success({
                  message: translate('customerGroupingMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('customerGroupingMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<CustomerGroupingSearch>(new CustomerGroupingSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<CustomerGrouping, CustomerGroupingSearch>(
    search,
    setSearch,
    customerGroupingMasterRepository.list,
    customerGroupingMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('customerGroupingMaster.title')}
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
                title={translate('customerGroupingMaster.index')}
                render={renderIndex<CustomerGrouping, CustomerGroupingSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('customerGroupingMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<CustomerGrouping>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('customerGroupingMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<CustomerGrouping>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, customerGrouping: CustomerGrouping) => {
                  return (
                    <>
                      <Link to={path.join(CUSTOMER_GROUPING_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(customerGrouping)}>
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

export default withRouter(CustomerGroupingMaster);
