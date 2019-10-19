
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

import { SUPPLIER_ROUTE } from 'config/route-consts';
import { Supplier } from 'models/Supplier';
import { SupplierSearch } from 'models/SupplierSearch';
import './SupplierMaster.scss';
import supplierMasterRepository from './SupplierMasterRepository';

const {Column} = Table;

function SupplierMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(SUPPLIER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new SupplierSearch());
  }

  function reloadList() {
    setSearch(new SupplierSearch(search));
  }

  function handleDelete(supplier: Supplier) {
    return () => {
      confirm({
        title: translate('supplierMaster.deletion.title'),
        content: translate('supplierMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          supplierMasterRepository.delete(supplier)
            .subscribe(
              () => {
                notification.success({
                  message: translate('supplierMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('supplierMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<SupplierSearch>(new SupplierSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Supplier, SupplierSearch>(
    search,
    setSearch,
    supplierMasterRepository.list,
    supplierMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('supplierMaster.title')}
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
                title={translate('supplierMaster.index')}
                render={renderIndex<Supplier, SupplierSearch>(search)}
        />

         <Column key="id"
                dataIndex="id"
                title={translate('supplierMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Supplier>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('supplierMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Supplier>('name', sorter)}
        />
         <Column key="phone"
                dataIndex="phone"
                title={translate('supplierMaster.phone')}
                sorter
                sortOrder={getColumnSortOrder<Supplier>('phone', sorter)}
        />
         <Column key="contactPerson"
                dataIndex="contactPerson"
                title={translate('supplierMaster.contactPerson')}
                sorter
                sortOrder={getColumnSortOrder<Supplier>('contactPerson', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('supplierMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<Supplier>('address', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, supplier: Supplier) => {
                  return (
                    <>
                      <Link to={path.join(SUPPLIER_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(supplier)}>
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

export default withRouter(SupplierMaster);
