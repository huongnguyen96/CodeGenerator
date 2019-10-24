
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

import { PRODUCT_STATUS_ROUTE } from 'config/route-consts';
import { ProductStatus } from 'models/ProductStatus';
import { ProductStatusSearch } from 'models/ProductStatusSearch';
import './ProductStatusMaster.scss';
import productStatusMasterRepository from './ProductStatusMasterRepository';

const {Column} = Table;

function ProductStatusMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(PRODUCT_STATUS_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ProductStatusSearch());
  }

  function reloadList() {
    setSearch(new ProductStatusSearch(search));
  }

  function handleDelete(productStatus: ProductStatus) {
    return () => {
      confirm({
        title: translate('productStatusMaster.deletion.title'),
        content: translate('productStatusMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          productStatusMasterRepository.delete(productStatus)
            .subscribe(
              () => {
                notification.success({
                  message: translate('productStatusMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('productStatusMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ProductStatusSearch>(new ProductStatusSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ProductStatus, ProductStatusSearch>(
    search,
    setSearch,
    productStatusMasterRepository.list,
    productStatusMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('productStatusMaster.title')}
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
                title={translate('productStatusMaster.index')}
                render={renderIndex<ProductStatus, ProductStatusSearch>(search)}
        />

         <Column key="code"
                dataIndex="code"
                title={translate('productStatusMaster.code')}
                sorter
                sortOrder={getColumnSortOrder<ProductStatus>('code', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('productStatusMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ProductStatus>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, productStatus: ProductStatus) => {
                  return (
                    <>
                      <Link to={path.join(PRODUCT_STATUS_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(productStatus)}>
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

export default withRouter(ProductStatusMaster);
