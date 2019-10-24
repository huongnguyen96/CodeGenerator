
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

import { MERCHANT_ROUTE } from 'config/route-consts';
import { Merchant } from 'models/Merchant';
import { MerchantSearch } from 'models/MerchantSearch';
import './MerchantMaster.scss';
import merchantMasterRepository from './MerchantMasterRepository';

const {Column} = Table;

function MerchantMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(MERCHANT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new MerchantSearch());
  }

  function reloadList() {
    setSearch(new MerchantSearch(search));
  }

  function handleDelete(merchant: Merchant) {
    return () => {
      confirm({
        title: translate('merchantMaster.deletion.title'),
        content: translate('merchantMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          merchantMasterRepository.delete(merchant)
            .subscribe(
              () => {
                notification.success({
                  message: translate('merchantMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('merchantMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<MerchantSearch>(new MerchantSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Merchant, MerchantSearch>(
    search,
    setSearch,
    merchantMasterRepository.list,
    merchantMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('merchantMaster.title')}
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
                title={translate('merchantMaster.index')}
                render={renderIndex<Merchant, MerchantSearch>(search)}
        />

         <Column key="name"
                dataIndex="name"
                title={translate('merchantMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Merchant>('name', sorter)}
        />
         <Column key="phone"
                dataIndex="phone"
                title={translate('merchantMaster.phone')}
                sorter
                sortOrder={getColumnSortOrder<Merchant>('phone', sorter)}
        />
         <Column key="contactPerson"
                dataIndex="contactPerson"
                title={translate('merchantMaster.contactPerson')}
                sorter
                sortOrder={getColumnSortOrder<Merchant>('contactPerson', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('merchantMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<Merchant>('address', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, merchant: Merchant) => {
                  return (
                    <>
                      <Link to={path.join(MERCHANT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(merchant)}>
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

export default withRouter(MerchantMaster);
