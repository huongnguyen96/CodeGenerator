
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

import './WardMaster.scss';
import wardMasterRepository from './WardMasterRepository';
import { WARD_ROUTE } from 'config/route-consts';
import { Ward } from 'models/Ward';
import { WardSearch } from 'models/WardSearch';

import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

const {Column} = Table;

function WardMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(WARD_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new WardSearch());
  }

  function reloadList() {
    setSearch(new WardSearch(search));
  }

  function handleDelete(ward: Ward) {
    return () => {
      confirm({
        title: translate('wardMaster.deletion.title'),
        content: translate('wardMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          wardMasterRepository.delete(ward)
            .subscribe(
              () => {
                notification.success({
                  message: translate('wardMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('wardMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<WardSearch>(new WardSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Ward, WardSearch>(
    search,
    setSearch,
    wardMasterRepository.list,
    wardMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('wardMaster.title')}
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
                title={translate('wardMaster.index')}
                render={renderIndex<Ward, WardSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('wardMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Ward>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('wardMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Ward>('name', sorter)}
        />
         <Column key="orderNumber"
                dataIndex="orderNumber"
                title={translate('wardMaster.orderNumber')}
                sorter
                sortOrder={getColumnSortOrder<Ward>('orderNumber', sorter)}
        />
         <Column key="districtId"
                dataIndex="districtId"
                title={translate('wardMaster.districtId')}
                sorter
                sortOrder={getColumnSortOrder<Ward>('districtId', sorter)}
        />
         <Column key="district"
                dataIndex="district"
                title={translate('wardMaster.district')}
                sorter
                sortOrder={getColumnSortOrder<Ward>('district', sorter)}
                render={(district: District) => {
                       return (
                         <>
                           {district.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, ward: Ward) => {
                  return (
                    <>
                      <Link to={path.join(WARD_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(ward)}>
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

export default withRouter(WardMaster);
