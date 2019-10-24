
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

import { DISCOUNT_CONTENT_ROUTE } from 'config/route-consts';
import { DiscountContent } from 'models/DiscountContent';
import { DiscountContentSearch } from 'models/DiscountContentSearch';
import './DiscountContentMaster.scss';
import discountContentMasterRepository from './DiscountContentMasterRepository';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

const {Column} = Table;

function DiscountContentMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(DISCOUNT_CONTENT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DiscountContentSearch());
  }

  function reloadList() {
    setSearch(new DiscountContentSearch(search));
  }

  function handleDelete(discountContent: DiscountContent) {
    return () => {
      confirm({
        title: translate('discountContentMaster.deletion.title'),
        content: translate('discountContentMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          discountContentMasterRepository.delete(discountContent)
            .subscribe(
              () => {
                notification.success({
                  message: translate('discountContentMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('discountContentMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<DiscountContentSearch>(new DiscountContentSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<DiscountContent, DiscountContentSearch>(
    search,
    setSearch,
    discountContentMasterRepository.list,
    discountContentMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('discountContentMaster.title')}
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
                title={translate('discountContentMaster.index')}
                render={renderIndex<DiscountContent, DiscountContentSearch>(search)}
        />

         <Column key="discountValue"
                dataIndex="discountValue"
                title={translate('discountContentMaster.discountValue')}
                sorter
                sortOrder={getColumnSortOrder<DiscountContent>('discountValue', sorter)}
        />
         <Column key="discount"
                dataIndex="discount"
                title={translate('discountContentMaster.discount')}
                sorter
                sortOrder={getColumnSortOrder<DiscountContent>('discount', sorter)}
                render={(discount: Discount) => {
                       return (
                         <>
                           {discount && discount.id}
                         </>
                       );
                     }}
        />
         <Column key="item"
                dataIndex="item"
                title={translate('discountContentMaster.item')}
                sorter
                sortOrder={getColumnSortOrder<DiscountContent>('item', sorter)}
                render={(item: Item) => {
                       return (
                         <>
                           {item && item.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, discountContent: DiscountContent) => {
                  return (
                    <>
                      <Link to={path.join(DISCOUNT_CONTENT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(discountContent)}>
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

export default withRouter(DiscountContentMaster);
