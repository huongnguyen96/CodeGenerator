
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

import './ItemTypeMaster.scss';
import itemTypeMasterRepository from './ItemTypeMasterRepository';
import { ITEM_TYPE_ROUTE } from 'config/route-consts';
import { ItemType } from 'models/ItemType';
import { ItemTypeSearch } from 'models/ItemTypeSearch';


const {Column} = Table;

function ItemTypeMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ITEM_TYPE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ItemTypeSearch());
  }

  function reloadList() {
    setSearch(new ItemTypeSearch(search));
  }

  function handleDelete(itemType: ItemType) {
    return () => {
      confirm({
        title: translate('itemTypeMaster.deletion.title'),
        content: translate('itemTypeMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          itemTypeMasterRepository.delete(itemType)
            .subscribe(
              () => {
                notification.success({
                  message: translate('itemTypeMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('itemTypeMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ItemTypeSearch>(new ItemTypeSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ItemType, ItemTypeSearch>(
    search,
    setSearch,
    itemTypeMasterRepository.list,
    itemTypeMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('itemTypeMaster.title')}
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
                title={translate('itemTypeMaster.index')}
                render={renderIndex<ItemType, ItemTypeSearch>(search)}
        />
        
         <Column key="code"
                dataIndex="code"
                title={translate('itemTypeMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<ItemType>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('itemTypeMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ItemType>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, itemType: ItemType) => {
                  return (
                    <>
                      <Link to={path.join(ITEM_TYPE_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(itemType)}>
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

export default withRouter(ItemTypeMaster);
