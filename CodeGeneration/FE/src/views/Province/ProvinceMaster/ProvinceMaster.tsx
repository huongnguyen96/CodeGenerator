
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

import { PROVINCE_ROUTE } from 'config/route-consts';
import { Province } from 'models/Province';
import { ProvinceSearch } from 'models/ProvinceSearch';
import './ProvinceMaster.scss';
import provinceMasterRepository from './ProvinceMasterRepository';

const {Column} = Table;

function ProvinceMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PROVINCE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ProvinceSearch());
  }

  function reloadList() {
    setSearch(new ProvinceSearch(search));
  }

  function handleDelete(province: Province) {
    return () => {
      confirm({
        title: translate('provinceMaster.deletion.title'),
        content: translate('provinceMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          provinceMasterRepository.delete(province)
            .subscribe(
              () => {
                notification.success({
                  message: translate('provinceMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('provinceMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ProvinceSearch>(new ProvinceSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Province, ProvinceSearch>(
    search,
    setSearch,
    provinceMasterRepository.list,
    provinceMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('provinceMaster.title')}
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
                title={translate('provinceMaster.index')}
                render={renderIndex<Province, ProvinceSearch>(search)}
        />

         <Column key="name"
                dataIndex="name"
                title={translate('provinceMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Province>('name', sorter)}
        />
         <Column key="orderNumber"
                dataIndex="orderNumber"
                title={translate('provinceMaster.orderNumber')}
                sorter
                sortOrder={getColumnSortOrder<Province>('orderNumber', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, province: Province) => {
                  return (
                    <>
                      <Link to={path.join(PROVINCE_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(province)}>
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

export default withRouter(ProvinceMaster);
