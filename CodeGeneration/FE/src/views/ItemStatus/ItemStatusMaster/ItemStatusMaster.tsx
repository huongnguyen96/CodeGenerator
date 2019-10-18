
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

import './ItemStatusMaster.scss';
import itemStatusMasterRepository from './ItemStatusMasterRepository';
import { ITEM_STATUS_ROUTE } from 'config/route-consts';
import { ItemStatus } from 'models/ItemStatus';
import { ItemStatusSearch } from 'models/ItemStatusSearch';

const {Column} = Table;

function ItemStatusMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ItemStatus_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ItemStatusSearch());
  }

  function reloadList() {
    setSearch(new ItemStatusSearch(search));
  }

  function handleDelete(id: string) {
    return () => {
      confirm({
        title: translate('itemStatusMaster.deletion.title'),
        content: translate('itemStatusMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          itemStatusMasterRepository.delete(id)
            .subscribe(
              () => {
                notification.success({
                  message: translate('itemStatusMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('itemStatusMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ItemStatusSearch>(new ItemStatusSearch());

  const [
    districts,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ItemStatus, ItemStatusSearch>(
    search,
    setSearch,
    itemStatusMasterRepository.list,
    itemStatusMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('itemStatusMaster.title')}
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
             pagination={
               total,
             }
      >
        <Column key="index"
                title={translate('itemStatusMaster.index')}
                render={renderIndex<ItemStatus, ItemStatusSearch>(search)}
        />
        
        <Column key="id"    
                dataIndex="id"
                title={translate('itemStatusMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<ItemStatus>('id', sorter)}
        />
        <Column key="code"    
                dataIndex="code"
                title={translate('itemStatusMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<ItemStatus>('code', sorter)}
        />
        <Column key="name"    
                dataIndex="name"
                title={translate('itemStatusMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ItemStatus>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string) => {
                  return (
                    <>
                      <Link to={path.join(ITEM_STATUS_ROUTE, id)}>
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

export default withRouter(ItemStatusMaster);
