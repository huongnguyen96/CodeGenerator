
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

import './ItemMaster.scss';
import itemMasterRepository from './ItemMasterRepository';
import { ITEM_ROUTE } from 'config/route-consts';
import { Item } from 'models/Item';
import { ItemSearch } from 'models/ItemSearch';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';

const {Column} = Table;

function ItemMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ITEM_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ItemSearch());
  }

  function reloadList() {
    setSearch(new ItemSearch(search));
  }

  function handleDelete(item: Item) {
    return () => {
      confirm({
        title: translate('itemMaster.deletion.title'),
        content: translate('itemMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          itemMasterRepository.delete(item)
            .subscribe(
              () => {
                notification.success({
                  message: translate('itemMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('itemMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ItemSearch>(new ItemSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Item, ItemSearch>(
    search,
    setSearch,
    itemMasterRepository.list,
    itemMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('itemMaster.title')}
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
                title={translate('itemMaster.index')}
                render={renderIndex<Item, ItemSearch>(search)}
        />
        
         <Column key="code"
                dataIndex="code"
                title={translate('itemMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<Item>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('itemMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Item>('name', sorter)}
        />
         <Column key="sKU"
                dataIndex="sKU"
                title={translate('itemMaster.sKU')}
                sorter
                sortOrder={getColumnSortOrder<Item>('sKU', sorter)}
        />
         <Column key="description"
                dataIndex="description"
                title={translate('itemMaster.description')}
                sorter
                sortOrder={getColumnSortOrder<Item>('description', sorter)}
        />
         <Column key="brand"
                dataIndex="brand"
                title={translate('itemMaster.brand')}
                sorter
                sortOrder={getColumnSortOrder<Item>('brand', sorter)}
                render={(brand: Brand) => {
                       return (
                         <>
                           {brand && brand.id}
                         </>
                       );
                     }}
        />
         <Column key="category"
                dataIndex="category"
                title={translate('itemMaster.category')}
                sorter
                sortOrder={getColumnSortOrder<Item>('category', sorter)}
                render={(category: Category) => {
                       return (
                         <>
                           {category && category.id}
                         </>
                       );
                     }}
        />
         <Column key="partner"
                dataIndex="partner"
                title={translate('itemMaster.partner')}
                sorter
                sortOrder={getColumnSortOrder<Item>('partner', sorter)}
                render={(partner: Partner) => {
                       return (
                         <>
                           {partner && partner.id}
                         </>
                       );
                     }}
        />
         <Column key="status"
                dataIndex="status"
                title={translate('itemMaster.status')}
                sorter
                sortOrder={getColumnSortOrder<Item>('status', sorter)}
                render={(status: ItemStatus) => {
                       return (
                         <>
                           {status && status.id}
                         </>
                       );
                     }}
        />
         <Column key="type"
                dataIndex="type"
                title={translate('itemMaster.type')}
                sorter
                sortOrder={getColumnSortOrder<Item>('type', sorter)}
                render={(type: ItemType) => {
                       return (
                         <>
                           {type && type.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, item: Item) => {
                  return (
                    <>
                      <Link to={path.join(ITEM_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(item)}>
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

export default withRouter(ItemMaster);
