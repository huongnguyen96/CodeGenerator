
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

import './WarehouseMaster.scss';
import warehouseMasterRepository from './WarehouseMasterRepository';
import { WAREHOUSE_ROUTE } from 'config/route-consts';
import { Warehouse } from 'models/Warehouse';
import { WarehouseSearch } from 'models/WarehouseSearch';

const {Column} = Table;

function WarehouseMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(WAREHOUSE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new WarehouseSearch());
  }

  function reloadList() {
    setSearch(new WarehouseSearch(search));
  }

  function handleDelete(warehouse: Warehouse) {
    return () => {
      confirm({
        title: translate('warehouseMaster.deletion.title'),
        content: translate('warehouseMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          warehouseMasterRepository.delete(warehouse)
            .subscribe(
              () => {
                notification.success({
                  message: translate('warehouseMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('warehouseMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<WarehouseSearch>(new WarehouseSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Warehouse, WarehouseSearch>(
    search,
    setSearch,
    warehouseMasterRepository.list,
    warehouseMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('warehouseMaster.title')}
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
                title={translate('warehouseMaster.index')}
                render={renderIndex<Warehouse, WarehouseSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('warehouseMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('warehouseMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('name', sorter)}
        />
         <Column key="phone"
                dataIndex="phone"
                title={translate('warehouseMaster.phone')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('phone', sorter)}
        />
         <Column key="email"
                dataIndex="email"
                title={translate('warehouseMaster.email')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('email', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('warehouseMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('address', sorter)}
        />
         <Column key="partnerId"
                dataIndex="partnerId"
                title={translate('warehouseMaster.partnerId')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('partnerId', sorter)}
        />
         <Column key="partner"
                dataIndex="partner"
                title={translate('warehouseMaster.partner')}
                sorter
                sortOrder={getColumnSortOrder<Warehouse>('partner', sorter)}
                render={(partner: Partner) => {
                       return (
                         <>
                           {partner.name}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, warehouse: Warehouse) => {
                  return (
                    <>
                      <Link to={path.join(WAREHOUSE_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(warehouse)}>
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

export default withRouter(WarehouseMaster);
