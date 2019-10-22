
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

import './UnitMaster.scss';
import unitMasterRepository from './UnitMasterRepository';
import { UNIT_ROUTE } from 'config/route-consts';
import { Unit } from 'models/Unit';
import { UnitSearch } from 'models/UnitSearch';

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

const {Column} = Table;

function UnitMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(UNIT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new UnitSearch());
  }

  function reloadList() {
    setSearch(new UnitSearch(search));
  }

  function handleDelete(unit: Unit) {
    return () => {
      confirm({
        title: translate('unitMaster.deletion.title'),
        content: translate('unitMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          unitMasterRepository.delete(unit)
            .subscribe(
              () => {
                notification.success({
                  message: translate('unitMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('unitMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<UnitSearch>(new UnitSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<Unit, UnitSearch>(
    search,
    setSearch,
    unitMasterRepository.list,
    unitMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('unitMaster.title')}
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
                title={translate('unitMaster.index')}
                render={renderIndex<Unit, UnitSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('unitMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('id', sorter)}
        />
         <Column key="firstVariationId"
                dataIndex="firstVariationId"
                title={translate('unitMaster.firstVariationId')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('firstVariationId', sorter)}
        />
         <Column key="secondVariationId"
                dataIndex="secondVariationId"
                title={translate('unitMaster.secondVariationId')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('secondVariationId', sorter)}
        />
         <Column key="thirdVariationId"
                dataIndex="thirdVariationId"
                title={translate('unitMaster.thirdVariationId')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('thirdVariationId', sorter)}
        />
         <Column key="sKU"
                dataIndex="sKU"
                title={translate('unitMaster.sKU')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('sKU', sorter)}
        />
         <Column key="price"
                dataIndex="price"
                title={translate('unitMaster.price')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('price', sorter)}
        />
         <Column key="firstVariation"
                dataIndex="firstVariation"
                title={translate('unitMaster.firstVariation')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('firstVariation', sorter)}
                render={(firstVariation: Variation) => {
                       return (
                         <>
                           {firstVariation.id}
                         </>
                       );
                     }}
        />
         <Column key="secondVariation"
                dataIndex="secondVariation"
                title={translate('unitMaster.secondVariation')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('secondVariation', sorter)}
                render={(secondVariation: Variation) => {
                       return (
                         <>
                           {secondVariation.id}
                         </>
                       );
                     }}
        />
         <Column key="thirdVariation"
                dataIndex="thirdVariation"
                title={translate('unitMaster.thirdVariation')}
                sorter
                sortOrder={getColumnSortOrder<Unit>('thirdVariation', sorter)}
                render={(thirdVariation: Variation) => {
                       return (
                         <>
                           {thirdVariation.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, unit: Unit) => {
                  return (
                    <>
                      <Link to={path.join(UNIT_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(unit)}>
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

export default withRouter(UnitMaster);
