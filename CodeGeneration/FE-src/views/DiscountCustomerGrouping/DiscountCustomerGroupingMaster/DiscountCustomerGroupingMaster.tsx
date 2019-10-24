
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

import './DiscountCustomerGroupingMaster.scss';
import discountCustomerGroupingMasterRepository from './DiscountCustomerGroupingMasterRepository';
import { DISCOUNT_CUSTOMER_GROUPING_ROUTE } from 'config/route-consts';
import { DiscountCustomerGrouping } from 'models/DiscountCustomerGrouping';
import { DiscountCustomerGroupingSearch } from 'models/DiscountCustomerGroupingSearch';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';

const {Column} = Table;

function DiscountCustomerGroupingMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DiscountCustomerGroupingSearch());
  }

  function reloadList() {
    setSearch(new DiscountCustomerGroupingSearch(search));
  }

  function handleDelete(discountCustomerGrouping: DiscountCustomerGrouping) {
    return () => {
      confirm({
        title: translate('discountCustomerGroupingMaster.deletion.title'),
        content: translate('discountCustomerGroupingMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          discountCustomerGroupingMasterRepository.delete(discountCustomerGrouping)
            .subscribe(
              () => {
                notification.success({
                  message: translate('discountCustomerGroupingMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('discountCustomerGroupingMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<DiscountCustomerGroupingSearch>(new DiscountCustomerGroupingSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<DiscountCustomerGrouping, DiscountCustomerGroupingSearch>(
    search,
    setSearch,
    discountCustomerGroupingMasterRepository.list,
    discountCustomerGroupingMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('discountCustomerGroupingMaster.title')}
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
                title={translate('discountCustomerGroupingMaster.index')}
                render={renderIndex<DiscountCustomerGrouping, DiscountCustomerGroupingSearch>(search)}
        />
        
         <Column key="customerGroupingCode"
                dataIndex="customerGroupingCode"
                title={translate('discountCustomerGroupingMaster.customerGroupingCode')}
                sorter
                sortOrder={getColumnSortOrder<DiscountCustomerGrouping>('customerGroupingCode', sorter)}
        />
         <Column key="discount"
                dataIndex="discount"
                title={translate('discountCustomerGroupingMaster.discount')}
                sorter
                sortOrder={getColumnSortOrder<DiscountCustomerGrouping>('discount', sorter)}
                render={(discount: Discount) => {
                       return (
                         <>
                           {discount && discount.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, discountCustomerGrouping: DiscountCustomerGrouping) => {
                  return (
                    <>
                      <Link to={path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(discountCustomerGrouping)}>
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

export default withRouter(DiscountCustomerGroupingMaster);
