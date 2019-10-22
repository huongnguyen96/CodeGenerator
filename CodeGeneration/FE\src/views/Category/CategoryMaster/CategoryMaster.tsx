
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

import './CategoryMaster.scss';
import categoryMasterRepository from './CategoryMasterRepository';
import { CATEGORY_ROUTE } from 'config/route-consts';
import { Category } from 'models/Category';
import { CategorySearch } from 'models/CategorySearch';


const {Column} = Table;

function CategoryMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(CATEGORY_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new CategorySearch());
  }

  function reloadList() {
    setSearch(new CategorySearch(search));
  }

  function handleDelete(category: Category) {
    return () => {
      confirm({
        title: translate('categoryMaster.deletion.title'),
        content: translate('categoryMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          categoryMasterRepository.delete(category)
            .subscribe(
              () => {
                notification.success({
                  message: translate('categoryMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('categoryMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<CategorySearch>(new CategorySearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Category, CategorySearch>(
    search,
    setSearch,
    categoryMasterRepository.list,
    categoryMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('categoryMaster.title')}
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
                title={translate('categoryMaster.index')}
                render={renderIndex<Category, CategorySearch>(search)}
        />
        
         <Column key="code"
                dataIndex="code"
                title={translate('categoryMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<Category>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('categoryMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Category>('name', sorter)}
        />
         <Column key="icon"
                dataIndex="icon"
                title={translate('categoryMaster.icon')}
                sorter
                sortOrder={getColumnSortOrder<Category>('icon', sorter)}
        />
         <Column key="parent"
                dataIndex="parent"
                title={translate('categoryMaster.parent')}
                sorter
                sortOrder={getColumnSortOrder<Category>('parent', sorter)}
                render={(parent: Category) => {
                       return (
                         <>
                           {parent && parent.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, category: Category) => {
                  return (
                    <>
                      <Link to={path.join(CATEGORY_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(category)}>
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

export default withRouter(CategoryMaster);
