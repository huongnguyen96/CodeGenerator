
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

import './ProductTypeMaster.scss';
import productTypeMasterRepository from './ProductTypeMasterRepository';
import { PRODUCT_TYPE_ROUTE } from 'config/route-consts';
import { ProductType } from 'models/ProductType';
import { ProductTypeSearch } from 'models/ProductTypeSearch';


const {Column} = Table;

function ProductTypeMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PRODUCT_TYPE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ProductTypeSearch());
  }

  function reloadList() {
    setSearch(new ProductTypeSearch(search));
  }

  function handleDelete(productType: ProductType) {
    return () => {
      confirm({
        title: translate('productTypeMaster.deletion.title'),
        content: translate('productTypeMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          productTypeMasterRepository.delete(productType)
            .subscribe(
              () => {
                notification.success({
                  message: translate('productTypeMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('productTypeMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ProductTypeSearch>(new ProductTypeSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ProductType, ProductTypeSearch>(
    search,
    setSearch,
    productTypeMasterRepository.list,
    productTypeMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('productTypeMaster.title')}
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
                title={translate('productTypeMaster.index')}
                render={renderIndex<ProductType, ProductTypeSearch>(search)}
        />
        
         <Column key="code"
                dataIndex="code"
                title={translate('productTypeMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<ProductType>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('productTypeMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ProductType>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, productType: ProductType) => {
                  return (
                    <>
                      <Link to={path.join(PRODUCT_TYPE_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(productType)}>
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

export default withRouter(ProductTypeMaster);
