
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

import './ItemUnitOfMeasureMaster.scss';
import itemUnitOfMeasureMasterRepository from './ItemUnitOfMeasureMasterRepository';
import { ITEM_UNIT_OF_MEASURE_ROUTE } from 'config/route-consts';
import { ItemUnitOfMeasure } from 'models/ItemUnitOfMeasure';
import { ItemUnitOfMeasureSearch } from 'models/ItemUnitOfMeasureSearch';

const {Column} = Table;

function ItemUnitOfMeasureMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(ITEM_UNIT_OF_MEASURE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ItemUnitOfMeasureSearch());
  }

  function reloadList() {
    setSearch(new ItemUnitOfMeasureSearch(search));
  }

  function handleDelete(id: string) {
    return () => {
      confirm({
        title: translate('itemUnitOfMeasureMaster.deletion.title'),
        content: translate('itemUnitOfMeasureMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          ItemUnitOfMeasureMasterRepository.delete(id)
            .subscribe(
              () => {
                notification.success({
                  message: translate('itemUnitOfMeasureMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('itemUnitOfMeasureMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ItemUnitOfMeasureSearch>(new ItemUnitOfMeasureSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ItemUnitOfMeasure, ItemUnitOfMeasureSearch>(
    search,
    setSearch,
    itemUnitOfMeasureMasterRepository.list,
    itemUnitOfMeasureRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('itemUnitOfMeasureMaster.title')}
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
                title={translate('itemUnitOfMeasureMaster.index')}
                render={renderIndex<ItemUnitOfMeasure, ItemUnitOfMeasureSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('itemUnitOfMeasureMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<ItemUnitOfMeasure>('id', sorter)}
        />
         <Column key="code"
                dataIndex="code"
                title={translate('itemUnitOfMeasureMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<ItemUnitOfMeasure>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('itemUnitOfMeasureMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ItemUnitOfMeasure>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string) => {
                  return (
                    <>
                      <Link to={path.join(ITEM_UNIT_OF_MEASURE_ROUTE, id)}>
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

export default withRouter(ItemUnitOfMeasureMaster);
