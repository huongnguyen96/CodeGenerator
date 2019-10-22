
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

import './ShippingAddressMaster.scss';
import shippingAddressMasterRepository from './ShippingAddressMasterRepository';
import { SHIPPING_ADDRESS_ROUTE } from 'config/route-consts';
import { ShippingAddress } from 'models/ShippingAddress';
import { ShippingAddressSearch } from 'models/ShippingAddressSearch';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

const {Column} = Table;

function ShippingAddressMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(SHIPPING_ADDRESS_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ShippingAddressSearch());
  }

  function reloadList() {
    setSearch(new ShippingAddressSearch(search));
  }

  function handleDelete(shippingAddress: ShippingAddress) {
    return () => {
      confirm({
        title: translate('shippingAddressMaster.deletion.title'),
        content: translate('shippingAddressMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          shippingAddressMasterRepository.delete(shippingAddress)
            .subscribe(
              () => {
                notification.success({
                  message: translate('shippingAddressMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('shippingAddressMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ShippingAddressSearch>(new ShippingAddressSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ShippingAddress, ShippingAddressSearch>(
    search,
    setSearch,
    shippingAddressMasterRepository.list,
    shippingAddressMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('shippingAddressMaster.title')}
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
                title={translate('shippingAddressMaster.index')}
                render={renderIndex<ShippingAddress, ShippingAddressSearch>(search)}
        />
        
         <Column key="fullName"
                dataIndex="fullName"
                title={translate('shippingAddressMaster.fullName')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('fullName', sorter)}
        />
         <Column key="companyName"
                dataIndex="companyName"
                title={translate('shippingAddressMaster.companyName')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('companyName', sorter)}
        />
         <Column key="phoneNumber"
                dataIndex="phoneNumber"
                title={translate('shippingAddressMaster.phoneNumber')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('phoneNumber', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('shippingAddressMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('address', sorter)}
        />
         <Column key="isDefault"
                dataIndex="isDefault"
                title={translate('shippingAddressMaster.isDefault')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('isDefault', sorter)}
        />
         <Column key="customer"
                dataIndex="customer"
                title={translate('shippingAddressMaster.customer')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('customer', sorter)}
                render={(customer: Customer) => {
                       return (
                         <>
                           {customer && customer.id}
                         </>
                       );
                     }}
        />
         <Column key="district"
                dataIndex="district"
                title={translate('shippingAddressMaster.district')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('district', sorter)}
                render={(district: District) => {
                       return (
                         <>
                           {district && district.id}
                         </>
                       );
                     }}
        />
         <Column key="province"
                dataIndex="province"
                title={translate('shippingAddressMaster.province')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('province', sorter)}
                render={(province: Province) => {
                       return (
                         <>
                           {province && province.id}
                         </>
                       );
                     }}
        />
         <Column key="ward"
                dataIndex="ward"
                title={translate('shippingAddressMaster.ward')}
                sorter
                sortOrder={getColumnSortOrder<ShippingAddress>('ward', sorter)}
                render={(ward: Ward) => {
                       return (
                         <>
                           {ward && ward.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, shippingAddress: ShippingAddress) => {
                  return (
                    <>
                      <Link to={path.join(SHIPPING_ADDRESS_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(shippingAddress)}>
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

export default withRouter(ShippingAddressMaster);
