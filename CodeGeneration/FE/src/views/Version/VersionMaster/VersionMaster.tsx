
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

import './VersionMaster.scss';
import versionMasterRepository from './VersionMasterRepository';
import { VERSION_ROUTE } from 'config/route-consts';
import { Version } from 'models/Version';
import { VersionSearch } from 'models/VersionSearch';

const {Column} = Table;

function VersionMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(VERSION_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new VersionSearch());
  }

  function reloadList() {
    setSearch(new VersionSearch(search));
  }

  function handleDelete(version: Version) {
    return () => {
      confirm({
        title: translate('versionMaster.deletion.title'),
        content: translate('versionMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          versionMasterRepository.delete(version)
            .subscribe(
              () => {
                notification.success({
                  message: translate('versionMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('versionMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<VersionSearch>(new VersionSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Version, VersionSearch>(
    search,
    setSearch,
    versionMasterRepository.list,
    versionMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('versionMaster.title')}
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
                title={translate('versionMaster.index')}
                render={renderIndex<Version, VersionSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('versionMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Version>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('versionMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Version>('name', sorter)}
        />
         <Column key="versionGroupingId"
                dataIndex="versionGroupingId"
                title={translate('versionMaster.versionGroupingId')}
                sorter
                sortOrder={getColumnSortOrder<Version>('versionGroupingId', sorter)}
        />
         <Column key="versionGrouping"
                dataIndex="versionGrouping"
                title={translate('versionMaster.versionGrouping')}
                sorter
                sortOrder={getColumnSortOrder<Version>('versionGrouping', sorter)}
                render={(versionGrouping: VersionGrouping) => {
                       return (
                         <>
                           {versionGrouping.name}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, version: Version) => {
                  return (
                    <>
                      <Link to={path.join(VERSION_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(version)}>
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

export default withRouter(VersionMaster);
