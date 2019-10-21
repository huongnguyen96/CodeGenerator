
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

import './PartnerMaster.scss';
import partnerMasterRepository from './PartnerMasterRepository';
import { PARTNER_ROUTE } from 'config/route-consts';
import { Partner } from 'models/Partner';
import { PartnerSearch } from 'models/PartnerSearch';

const {Column} = Table;

function PartnerMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PARTNER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new PartnerSearch());
  }

  function reloadList() {
    setSearch(new PartnerSearch(search));
  }

  function handleDelete(partner: Partner) {
    return () => {
      confirm({
        title: translate('partnerMaster.deletion.title'),
        content: translate('partnerMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          partnerMasterRepository.delete(partner)
            .subscribe(
              () => {
                notification.success({
                  message: translate('partnerMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('partnerMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<PartnerSearch>(new PartnerSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Partner, PartnerSearch>(
    search,
    setSearch,
    partnerMasterRepository.list,
    partnerMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('partnerMaster.title')}
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
                title={translate('partnerMaster.index')}
                render={renderIndex<Partner, PartnerSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('partnerMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Partner>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('partnerMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Partner>('name', sorter)}
        />
         <Column key="phone"
                dataIndex="phone"
                title={translate('partnerMaster.phone')}
                sorter
                sortOrder={getColumnSortOrder<Partner>('phone', sorter)}
        />
         <Column key="contactPerson"
                dataIndex="contactPerson"
                title={translate('partnerMaster.contactPerson')}
                sorter
                sortOrder={getColumnSortOrder<Partner>('contactPerson', sorter)}
        />
         <Column key="address"
                dataIndex="address"
                title={translate('partnerMaster.address')}
                sorter
                sortOrder={getColumnSortOrder<Partner>('address', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, partner: Partner) => {
                  return (
                    <>
                      <Link to={path.join(PARTNER_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(partner)}>
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

export default withRouter(PartnerMaster);
