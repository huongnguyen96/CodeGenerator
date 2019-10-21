
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

import './VersionGroupingMaster.scss';
import versionGroupingMasterRepository from './VersionGroupingMasterRepository';
import { VERSION_GROUPING_ROUTE } from 'config/route-consts';
import { VersionGrouping } from 'models/VersionGrouping';
import { VersionGroupingSearch } from 'models/VersionGroupingSearch';

const {Column} = Table;

function VersionGroupingMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(VERSION_GROUPING_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new VersionGroupingSearch());
  }

  function reloadList() {
    setSearch(new VersionGroupingSearch(search));
  }

  function handleDelete(versionGrouping: VersionGrouping) {
    return () => {
      confirm({
        title: translate('versionGroupingMaster.deletion.title'),
        content: translate('versionGroupingMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          versionGroupingMasterRepository.delete(versionGrouping)
            .subscribe(
              () => {
                notification.success({
                  message: translate('versionGroupingMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('versionGroupingMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<VersionGroupingSearch>(new VersionGroupingSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<VersionGrouping, VersionGroupingSearch>(
    search,
    setSearch,
    versionGroupingMasterRepository.list,
    versionGroupingMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('versionGroupingMaster.title')}
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
                title={translate('versionGroupingMaster.index')}
                render={renderIndex<VersionGrouping, VersionGroupingSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('versionGroupingMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<VersionGrouping>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('versionGroupingMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<VersionGrouping>('name', sorter)}
        />
         <Column key="itemId"
                dataIndex="itemId"
                title={translate('versionGroupingMaster.itemId')}
                sorter
                sortOrder={getColumnSortOrder<VersionGrouping>('itemId', sorter)}
        />
         <Column key="item"
                dataIndex="item"
                title={translate('versionGroupingMaster.item')}
                sorter
                sortOrder={getColumnSortOrder<VersionGrouping>('item', sorter)}
                render={(item: Item) => {
                       return (
                         <>
                           {item.name}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, versionGrouping: VersionGrouping) => {
                  return (
                    <>
                      <Link to={path.join(VERSION_GROUPING_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(versionGrouping)}>
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

export default withRouter(VersionGroupingMaster);
