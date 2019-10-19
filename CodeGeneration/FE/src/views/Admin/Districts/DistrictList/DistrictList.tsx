import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle/CardTitle';
import {useList} from 'core/hooks/useList';
import {confirm, getColumnSortOrder, notification, renderIndex} from 'helpers';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import { Link, RouteComponentProps, withRouter } from 'react-router-dom';

import { ADMIN_DISTRICTS_ROUTE } from 'config/route-consts';
import { District } from 'models/District';
import { DistrictSearch } from 'models/DistrictSearch';
import './DistrictList.scss';
import districtListRepository from './DistrictListRepository';

const {Column} = Table;

function DistrictList(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ADMIN_DISTRICTS_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DistrictSearch());
  }

  function reloadList() {
    setSearch(new DistrictSearch(search));
  }

  function handleDelete(id: string) {
    return () => {
      confirm({
        title: translate('districts.deletion.title'),
        content: translate('districts.deletion.content'),
        okType: 'danger',
        onOk: () => {
          districtListRepository.delete(id)
            .subscribe(
              () => {
                notification.success({
                  message: translate('districts.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('districts.deletion.error'),
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
    districts,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<District, DistrictSearch>(
    search,
    setSearch,
    districtListRepository.list,
    districtListRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('districts.title')}
                 allowAdd
                 onAdd={handleAdd}
                 allowClear
                 onClear={handleClear}
      />
    }>
      <Table dataSource={districts}
             rowKey="id"
             loading={loading}
             onChange={handleChange}
             pagination={{
               total,
             }}
      >
        <Column key="index"
                title={translate('districts.index')}
                render={renderIndex<District, DistrictSearch>(search)}
        />
        <Column key="code"
                dataIndex="code"
                title={translate('districts.code')}
                sorter
                sortOrder={getColumnSortOrder<District>('code', sorter)}
        />
        <Column key="name"
                dataIndex="name"
                title={translate('districts.name')}
                sorter
                sortOrder={getColumnSortOrder<District>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string) => {
                  return (
                    <>
                      <Link to={path.join(ADMIN_DISTRICTS_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(id)}>
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

export default withRouter(DistrictList);
