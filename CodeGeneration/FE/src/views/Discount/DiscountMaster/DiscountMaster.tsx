
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

import './DiscountMaster.scss';
import discountMasterRepository from './DiscountMasterRepository';
import { DISCOUNT_ROUTE } from 'config/route-consts';
import { Discount } from 'models/Discount';
import { DiscountSearch } from 'models/DiscountSearch';


const {Column} = Table;

function DiscountMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(DISCOUNT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new DiscountSearch());
  }

  function reloadList() {
    setSearch(new DiscountSearch(search));
  }

  function handleDelete(discount: Discount) {
    return () => {
      confirm({
        title: translate('discountMaster.deletion.title'),
        content: translate('discountMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          discountMasterRepository.delete(discount)
            .subscribe(
              () => {
                notification.success({
                  message: translate('discountMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('discountMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<DiscountSearch>(new DiscountSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Discount, DiscountSearch>(
    search,
    setSearch,
    discountMasterRepository.list,
    discountMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('discountMaster.title')}
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
                title={translate('discountMaster.index')}
                render={renderIndex<Discount, DiscountSearch>(search)}
        />
        
         <Column key="name"
                dataIndex="name"
                title={translate('discountMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Discount>('name', sorter)}
        />
         <Column key="start"
                dataIndex="start"
                title={translate('discountMaster.start')}
                sorter
                sortOrder={getColumnSortOrder<Discount>('start', sorter)}
        />
         <Column key="end"
                dataIndex="end"
                title={translate('discountMaster.end')}
                sorter
                sortOrder={getColumnSortOrder<Discount>('end', sorter)}
        />
         <Column key="type"
                dataIndex="type"
                title={translate('discountMaster.type')}
                sorter
                sortOrder={getColumnSortOrder<Discount>('type', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, discount: Discount) => {
                  return (
                    <>
                      <Link to={path.join(DISCOUNT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(discount)}>
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

export default withRouter(DiscountMaster);
