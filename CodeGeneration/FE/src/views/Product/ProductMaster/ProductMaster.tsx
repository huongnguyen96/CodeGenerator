
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

import { PRODUCT_ROUTE } from 'config/route-consts';
import { Product } from 'models/Product';
import { ProductSearch } from 'models/ProductSearch';
import './ProductMaster.scss';
import productMasterRepository from './ProductMasterRepository';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';

const {Column} = Table;

function ProductMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PRODUCT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ProductSearch());
  }

  function reloadList() {
    setSearch(new ProductSearch(search));
  }

  function handleDelete(product: Product) {
    return () => {
      confirm({
        title: translate('productMaster.deletion.title'),
        content: translate('productMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          productMasterRepository.delete(product)
            .subscribe(
              () => {
                notification.success({
                  message: translate('productMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('productMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ProductSearch>(new ProductSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Product, ProductSearch>(
    search,
    setSearch,
    productMasterRepository.list,
    productMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('productMaster.title')}
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
                title={translate('productMaster.index')}
                render={renderIndex<Product, ProductSearch>(search)}
        />

         <Column key="code"
                dataIndex="code"
                title={translate('productMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<Product>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('productMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Product>('name', sorter)}
        />
         <Column key="description"
                dataIndex="description"
                title={translate('productMaster.description')}
                sorter
                sortOrder={getColumnSortOrder<Product>('description', sorter)}
        />
         <Column key="warrantyPolicy"
                dataIndex="warrantyPolicy"
                title={translate('productMaster.warrantyPolicy')}
                sorter
                sortOrder={getColumnSortOrder<Product>('warrantyPolicy', sorter)}
        />
         <Column key="returnPolicy"
                dataIndex="returnPolicy"
                title={translate('productMaster.returnPolicy')}
                sorter
                sortOrder={getColumnSortOrder<Product>('returnPolicy', sorter)}
        />
         <Column key="expiredDate"
                dataIndex="expiredDate"
                title={translate('productMaster.expiredDate')}
                sorter
                sortOrder={getColumnSortOrder<Product>('expiredDate', sorter)}
        />
         <Column key="conditionOfUse"
                dataIndex="conditionOfUse"
                title={translate('productMaster.conditionOfUse')}
                sorter
                sortOrder={getColumnSortOrder<Product>('conditionOfUse', sorter)}
        />
         <Column key="maximumPurchaseQuantity"
                dataIndex="maximumPurchaseQuantity"
                title={translate('productMaster.maximumPurchaseQuantity')}
                sorter
                sortOrder={getColumnSortOrder<Product>('maximumPurchaseQuantity', sorter)}
        />
         <Column key="brand"
                dataIndex="brand"
                title={translate('productMaster.brand')}
                sorter
                sortOrder={getColumnSortOrder<Product>('brand', sorter)}
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
                title={translate('productMaster.category')}
                sorter
                sortOrder={getColumnSortOrder<Product>('category', sorter)}
                render={(category: Category) => {
                       return (
                         <>
                           {category && category.id}
                         </>
                       );
                     }}
        />
         <Column key="merchant"
                dataIndex="merchant"
                title={translate('productMaster.merchant')}
                sorter
                sortOrder={getColumnSortOrder<Product>('merchant', sorter)}
                render={(merchant: Merchant) => {
                       return (
                         <>
                           {merchant && merchant.id}
                         </>
                       );
                     }}
        />
         <Column key="status"
                dataIndex="status"
                title={translate('productMaster.status')}
                sorter
                sortOrder={getColumnSortOrder<Product>('status', sorter)}
                render={(status: ProductStatus) => {
                       return (
                         <>
                           {status && status.id}
                         </>
                       );
                     }}
        />
         <Column key="type"
                dataIndex="type"
                title={translate('productMaster.type')}
                sorter
                sortOrder={getColumnSortOrder<Product>('type', sorter)}
                render={(type: ProductType) => {
                       return (
                         <>
                           {type && type.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, product: Product) => {
                  return (
                    <>
                      <Link to={path.join(PRODUCT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(product)}>
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

export default withRouter(ProductMaster);
