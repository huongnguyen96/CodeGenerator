
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

import { DISTRICT_ROUTE } from 'config/route-consts';
import { District } from 'models/District';
import { DistrictSearch } from 'models/DistrictSearch';
import './DistrictMaster.scss';
import districtMasterRepository from './DistrictMasterRepository';

import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';

const {Column} = Table;

function DistrictMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(DISTRICT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DistrictSearch());
  }

  function reloadList() {
    setSearch(new DistrictSearch(search));
  }

  function handleDelete(district: District) {
    return () => {
      confirm({
        title: translate('districtMaster.deletion.title'),
        content: translate('districtMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          districtMasterRepository.delete(district)
            .subscribe(
              () => {
                notification.success({
                  message: translate('districtMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('districtMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<DistrictSearch>(new DistrictSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<District, DistrictSearch>(
    search,
    setSearch,
    districtMasterRepository.list,
    districtMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('districtMaster.title')}
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
                title={translate('districtMaster.index')}
                render={renderIndex<District, DistrictSearch>(search)}
        />

         <Column key="name"
                dataIndex="name"
                title={translate('districtMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<District>('name', sorter)}
        />
         <Column key="orderNumber"
                dataIndex="orderNumber"
                title={translate('districtMaster.orderNumber')}
                sorter
                sortOrder={getColumnSortOrder<District>('orderNumber', sorter)}
        />
         <Column key="province"
                dataIndex="province"
                title={translate('districtMaster.province')}
                sorter
                sortOrder={getColumnSortOrder<District>('province', sorter)}
                render={(province: Province) => {
                       return (
                         <>
                           {province && province.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, district: District) => {
                  return (
                    <>
                      <Link to={path.join(DISTRICT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(district)}>
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

export default withRouter(DistrictMaster);
