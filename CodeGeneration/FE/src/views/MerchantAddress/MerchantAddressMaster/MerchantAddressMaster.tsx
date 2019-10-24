
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

import { MERCHANT_ADDRESS_ROUTE } from 'config/route-consts';
import { MerchantAddress } from 'models/MerchantAddress';
import { MerchantAddressSearch } from 'models/MerchantAddressSearch';
import './MerchantAddressMaster.scss';
import merchantAddressMasterRepository from './MerchantAddressMasterRepository';

import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';

const {Column} = Table;

function MerchantAddressMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(MERCHANT_ADDRESS_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new MerchantAddressSearch());
  }

  function reloadList() {
    setSearch(new MerchantAddressSearch(search));
  }

  function handleDelete(merchantAddress: MerchantAddress) {
    return () => {
      confirm({
        title: translate('merchantAddressMaster.deletion.title'),
        content: translate('merchantAddressMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          merchantAddressMasterRepository.delete(merchantAddress)
            .subscribe(
              () => {
                notification.success({
                  message: translate('merchantAddressMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('merchantAddressMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<MerchantAddressSearch>(new MerchantAddressSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<MerchantAddress, MerchantAddressSearch>(
    search,
    setSearch,
    merchantAddressMasterRepository.list,
    merchantAddressMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('merchantAddressMaster.title')}
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
                title={translate('merchantAddressMaster.index')}
                render={renderIndex<MerchantAddress, MerchantAddressSearch>(search)}
        />

         <Column key="code"
                dataIndex="code"
                title={translate('merchantAddressMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<MerchantAddress>('code', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('merchantAddressMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<MerchantAddress>('address', sorter)}
        />
         <Column key="contact"
                dataIndex="contact"
                title={translate('merchantAddressMaster.contact')}
                sorter
                sortOrder={getColumnSortOrder<MerchantAddress>('contact', sorter)}
        />
         <Column key="phone"
                dataIndex="phone"
                title={translate('merchantAddressMaster.phone')}
                sorter
                sortOrder={getColumnSortOrder<MerchantAddress>('phone', sorter)}
        />
         <Column key="merchant"
                dataIndex="merchant"
                title={translate('merchantAddressMaster.merchant')}
                sorter
                sortOrder={getColumnSortOrder<MerchantAddress>('merchant', sorter)}
                render={(merchant: Merchant) => {
                       return (
                         <>
                           {merchant && merchant.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, merchantAddress: MerchantAddress) => {
                  return (
                    <>
                      <Link to={path.join(MERCHANT_ADDRESS_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(merchantAddress)}>
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

export default withRouter(MerchantAddressMaster);
