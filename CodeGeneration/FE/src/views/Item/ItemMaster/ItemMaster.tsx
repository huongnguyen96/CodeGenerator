
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

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

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
               pageSize: search.take,
             }}
      >
        <Column key="index"
                title={translate('itemMaster.index')}
                render={renderIndex<Item, ItemSearch>(search)}
        />
        
         <Column key="sKU"
                dataIndex="sKU"
                title={translate('itemMaster.sKU')}
                sorter
                sortOrder={getColumnSortOrder<Item>('sKU', sorter)}
        />
         <Column key="price"
                dataIndex="price"
                title={translate('itemMaster.price')}
                sorter
                sortOrder={getColumnSortOrder<Item>('price', sorter)}
        />
         <Column key="minPrice"
                dataIndex="minPrice"
                title={translate('itemMaster.minPrice')}
                sorter
                sortOrder={getColumnSortOrder<Item>('minPrice', sorter)}
        />
         <Column key="firstVariation"
                dataIndex="firstVariation"
                title={translate('itemMaster.firstVariation')}
                sorter
                sortOrder={getColumnSortOrder<Item>('firstVariation', sorter)}
                render={(firstVariation: Variation) => {
                       return (
                         <>
                           {firstVariation && firstVariation.id}
                         </>
                       );
                     }}
        />
         <Column key="product"
                dataIndex="product"
                title={translate('itemMaster.product')}
                sorter
                sortOrder={getColumnSortOrder<Item>('product', sorter)}
                render={(product: Product) => {
                       return (
                         <>
                           {product && product.id}
                         </>
                       );
                     }}
        />
         <Column key="secondVariation"
                dataIndex="secondVariation"
                title={translate('itemMaster.secondVariation')}
                sorter
                sortOrder={getColumnSortOrder<Item>('secondVariation', sorter)}
                render={(secondVariation: Variation) => {
                       return (
                         <>
                           {secondVariation && secondVariation.id}
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
