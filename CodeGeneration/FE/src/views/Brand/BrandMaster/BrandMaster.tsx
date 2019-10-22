
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

import './BrandMaster.scss';
import brandMasterRepository from './BrandMasterRepository';
import { BRAND_ROUTE } from 'config/route-consts';
import { Brand } from 'models/Brand';
import { BrandSearch } from 'models/BrandSearch';

import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';

const {Column} = Table;

function BrandMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(BRAND_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new BrandSearch());
  }

  function reloadList() {
    setSearch(new BrandSearch(search));
  }

  function handleDelete(brand: Brand) {
    return () => {
      confirm({
        title: translate('brandMaster.deletion.title'),
        content: translate('brandMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          brandMasterRepository.delete(brand)
            .subscribe(
              () => {
                notification.success({
                  message: translate('brandMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('brandMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<BrandSearch>(new BrandSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Brand, BrandSearch>(
    search,
    setSearch,
    brandMasterRepository.list,
    brandMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('brandMaster.title')}
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
                title={translate('brandMaster.index')}
                render={renderIndex<Brand, BrandSearch>(search)}
        />
        
         <Column key="name"
                dataIndex="name"
                title={translate('brandMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Brand>('name', sorter)}
        />
         <Column key="category"
                dataIndex="category"
                title={translate('brandMaster.category')}
                sorter
                sortOrder={getColumnSortOrder<Brand>('category', sorter)}
                render={(category: Category) => {
                       return (
                         <>
                           {category && category.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, brand: Brand) => {
                  return (
                    <>
                      <Link to={path.join(BRAND_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(brand)}>
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

export default withRouter(BrandMaster);
