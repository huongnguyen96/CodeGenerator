
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

import './UserMaster.scss';
import userMasterRepository from './UserMasterRepository';
import { USER_ROUTE } from 'config/route-consts';
import { User } from 'models/User';
import { UserSearch } from 'models/UserSearch';

const {Column} = Table;

function UserMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(USER_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new UserSearch());
  }

  function reloadList() {
    setSearch(new UserSearch(search));
  }

  function handleDelete(id: string) {
    return () => {
      confirm({
        title: translate('userMaster.deletion.title'),
        content: translate('userMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          UserMasterRepository.delete(id)
            .subscribe(
              () => {
                notification.success({
                  message: translate('userMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('userMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<UserSearch>(new UserSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<User, UserSearch>(
    search,
    setSearch,
    userMasterRepository.list,
    userRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('userMaster.title')}
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
             pagination={
               total,
             }
      >
        <Column key="index"
                title={translate('userMaster.index')}
                render={renderIndex<User, UserSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('userMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<User>('id', sorter)}
        />
         <Column key="username"
                dataIndex="username"
                title={translate('userMaster.username')}
                sorter
                sortOrder={getColumnSortOrder<User>('username', sorter)}
        />
         <Column key="password"
                dataIndex="password"
                title={translate('userMaster.password')}
                sorter
                sortOrder={getColumnSortOrder<User>('password', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string) => {
                  return (
                    <>
                      <Link to={path.join(USER_ROUTE, id)}>
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

export default withRouter(UserMaster);
