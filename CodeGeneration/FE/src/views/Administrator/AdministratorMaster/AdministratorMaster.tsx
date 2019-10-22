
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

import './AdministratorMaster.scss';
import administratorMasterRepository from './AdministratorMasterRepository';
import { ADMINISTRATOR_ROUTE } from 'config/route-consts';
import { Administrator } from 'models/Administrator';
import { AdministratorSearch } from 'models/AdministratorSearch';


const {Column} = Table;

function AdministratorMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ADMINISTRATOR_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new AdministratorSearch());
  }

  function reloadList() {
    setSearch(new AdministratorSearch(search));
  }

  function handleDelete(administrator: Administrator) {
    return () => {
      confirm({
        title: translate('administratorMaster.deletion.title'),
        content: translate('administratorMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          administratorMasterRepository.delete(administrator)
            .subscribe(
              () => {
                notification.success({
                  message: translate('administratorMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('administratorMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<AdministratorSearch>(new AdministratorSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Administrator, AdministratorSearch>(
    search,
    setSearch,
    administratorMasterRepository.list,
    administratorMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('administratorMaster.title')}
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
                title={translate('administratorMaster.index')}
                render={renderIndex<Administrator, AdministratorSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('administratorMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Administrator>('id', sorter)}
        />
         <Column key="username"
                dataIndex="username"
                title={translate('administratorMaster.username')}
                sorter
                sortOrder={getColumnSortOrder<Administrator>('username', sorter)}
        />
         <Column key="displayName"
                dataIndex="displayName"
                title={translate('administratorMaster.displayName')}
                sorter
                sortOrder={getColumnSortOrder<Administrator>('displayName', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, administrator: Administrator) => {
                  return (
                    <>
                      <Link to={path.join(ADMINISTRATOR_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(administrator)}>
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

export default withRouter(AdministratorMaster);
