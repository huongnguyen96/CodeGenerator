
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

import './VariationMaster.scss';
import variationMasterRepository from './VariationMasterRepository';
import { VARIATION_ROUTE } from 'config/route-consts';
import { Variation } from 'models/Variation';
import { VariationSearch } from 'models/VariationSearch';

import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';

const {Column} = Table;

function VariationMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(VARIATION_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new VariationSearch());
  }

  function reloadList() {
    setSearch(new VariationSearch(search));
  }

  function handleDelete(variation: Variation) {
    return () => {
      confirm({
        title: translate('variationMaster.deletion.title'),
        content: translate('variationMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          variationMasterRepository.delete(variation)
            .subscribe(
              () => {
                notification.success({
                  message: translate('variationMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('variationMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<VariationSearch>(new VariationSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Variation, VariationSearch>(
    search,
    setSearch,
    variationMasterRepository.list,
    variationMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('variationMaster.title')}
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
                title={translate('variationMaster.index')}
                render={renderIndex<Variation, VariationSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('variationMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Variation>('id', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('variationMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<Variation>('name', sorter)}
        />
         <Column key="variationGroupingId"
                dataIndex="variationGroupingId"
                title={translate('variationMaster.variationGroupingId')}
                sorter
                sortOrder={getColumnSortOrder<Variation>('variationGroupingId', sorter)}
        />
         <Column key="variationGrouping"
                dataIndex="variationGrouping"
                title={translate('variationMaster.variationGrouping')}
                sorter
                sortOrder={getColumnSortOrder<Variation>('variationGrouping', sorter)}
                render={(variationGrouping: VariationGrouping) => {
                       return (
                         <>
                           {variationGrouping.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, variation: Variation) => {
                  return (
                    <>
                      <Link to={path.join(VARIATION_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(variation)}>
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

export default withRouter(VariationMaster);
