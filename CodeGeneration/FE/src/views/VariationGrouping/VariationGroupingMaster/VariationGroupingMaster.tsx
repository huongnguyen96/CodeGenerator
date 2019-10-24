
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

import './VariationGroupingMaster.scss';
import variationGroupingMasterRepository from './VariationGroupingMasterRepository';
import { VARIATION_GROUPING_ROUTE } from 'config/route-consts';
import { VariationGrouping } from 'models/VariationGrouping';
import { VariationGroupingSearch } from 'models/VariationGroupingSearch';

import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

const {Column} = Table;

function VariationGroupingMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(VARIATION_GROUPING_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new VariationGroupingSearch());
  }

  function reloadList() {
    setSearch(new VariationGroupingSearch(search));
  }

  function handleDelete(variationGrouping: VariationGrouping) {
    return () => {
      confirm({
        title: translate('variationGroupingMaster.deletion.title'),
        content: translate('variationGroupingMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          variationGroupingMasterRepository.delete(variationGrouping)
            .subscribe(
              () => {
                notification.success({
                  message: translate('variationGroupingMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('variationGroupingMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<VariationGroupingSearch>(new VariationGroupingSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<VariationGrouping, VariationGroupingSearch>(
    search,
    setSearch,
    variationGroupingMasterRepository.list,
    variationGroupingMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('variationGroupingMaster.title')}
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
                title={translate('variationGroupingMaster.index')}
                render={renderIndex<VariationGrouping, VariationGroupingSearch>(search)}
        />
        
         <Column key="name"
                dataIndex="name"
                title={translate('variationGroupingMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<VariationGrouping>('name', sorter)}
        />
         <Column key="product"
                dataIndex="product"
                title={translate('variationGroupingMaster.product')}
                sorter
                sortOrder={getColumnSortOrder<VariationGrouping>('product', sorter)}
                render={(product: Product) => {
                       return (
                         <>
                           {product && product.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, variationGrouping: VariationGrouping) => {
                  return (
                    <>
                      <Link to={path.join(VARIATION_GROUPING_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(variationGrouping)}>
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

export default withRouter(VariationGroupingMaster);
