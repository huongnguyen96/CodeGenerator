
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

import './EVoucherMaster.scss';
import eVoucherMasterRepository from './EVoucherMasterRepository';
import { E_VOUCHER_ROUTE } from 'config/route-consts';
import { EVoucher } from 'models/EVoucher';
import { EVoucherSearch } from 'models/EVoucherSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

const {Column} = Table;

function EVoucherMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(E_VOUCHER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new EVoucherSearch());
  }

  function reloadList() {
    setSearch(new EVoucherSearch(search));
  }

  function handleDelete(eVoucher: EVoucher) {
    return () => {
      confirm({
        title: translate('eVoucherMaster.deletion.title'),
        content: translate('eVoucherMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          eVoucherMasterRepository.delete(eVoucher)
            .subscribe(
              () => {
                notification.success({
                  message: translate('eVoucherMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('eVoucherMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<EVoucherSearch>(new EVoucherSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<EVoucher, EVoucherSearch>(
    search,
    setSearch,
    eVoucherMasterRepository.list,
    eVoucherMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('eVoucherMaster.title')}
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
                title={translate('eVoucherMaster.index')}
                render={renderIndex<EVoucher, EVoucherSearch>(search)}
        />
        
         <Column key="name"
                dataIndex="name"
                title={translate('eVoucherMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('name', sorter)}
        />
         <Column key="start"
                dataIndex="start"
                title={translate('eVoucherMaster.start')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('start', sorter)}
        />
         <Column key="end"
                dataIndex="end"
                title={translate('eVoucherMaster.end')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('end', sorter)}
        />
         <Column key="quantity"
                dataIndex="quantity"
                title={translate('eVoucherMaster.quantity')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('quantity', sorter)}
        />
         <Column key="customer"
                dataIndex="customer"
                title={translate('eVoucherMaster.customer')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('customer', sorter)}
                render={(customer: Customer) => {
                       return (
                         <>
                           {customer && customer.id}
                         </>
                       );
                     }}
        />
         <Column key="product"
                dataIndex="product"
                title={translate('eVoucherMaster.product')}
                sorter
                sortOrder={getColumnSortOrder<EVoucher>('product', sorter)}
                render={(product: Product) => {
                       return (
                         <>
                           {product && product.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, eVoucher: EVoucher) => {
                  return (
                    <>
                      <Link to={path.join(E_VOUCHER_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(eVoucher)}>
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

export default withRouter(EVoucherMaster);
