
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

import './EVoucherContentMaster.scss';
import eVoucherContentMasterRepository from './EVoucherContentMasterRepository';
import { E_VOUCHER_CONTENT_ROUTE } from 'config/route-consts';
import { EVoucherContent } from 'models/EVoucherContent';
import { EVoucherContentSearch } from 'models/EVoucherContentSearch';

import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';

const {Column} = Table;

function EVoucherContentMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(E_VOUCHER_CONTENT_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new EVoucherContentSearch());
  }

  function reloadList() {
    setSearch(new EVoucherContentSearch(search));
  }

  function handleDelete(eVoucherContent: EVoucherContent) {
    return () => {
      confirm({
        title: translate('eVoucherContentMaster.deletion.title'),
        content: translate('eVoucherContentMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          eVoucherContentMasterRepository.delete(eVoucherContent)
            .subscribe(
              () => {
                notification.success({
                  message: translate('eVoucherContentMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('eVoucherContentMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<EVoucherContentSearch>(new EVoucherContentSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<EVoucherContent, EVoucherContentSearch>(
    search,
    setSearch,
    eVoucherContentMasterRepository.list,
    eVoucherContentMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('eVoucherContentMaster.title')}
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
                title={translate('eVoucherContentMaster.index')}
                render={renderIndex<EVoucherContent, EVoucherContentSearch>(search)}
        />
        
         <Column key="usedCode"
                dataIndex="usedCode"
                title={translate('eVoucherContentMaster.usedCode')}
                sorter
                sortOrder={getColumnSortOrder<EVoucherContent>('usedCode', sorter)}
        />
         <Column key="merchantCode"
                dataIndex="merchantCode"
                title={translate('eVoucherContentMaster.merchantCode')}
                sorter
                sortOrder={getColumnSortOrder<EVoucherContent>('merchantCode', sorter)}
        />
         <Column key="usedDate"
                dataIndex="usedDate"
                title={translate('eVoucherContentMaster.usedDate')}
                sorter
                sortOrder={getColumnSortOrder<EVoucherContent>('usedDate', sorter)}
        />
         <Column key="eVourcher"
                dataIndex="eVourcher"
                title={translate('eVoucherContentMaster.eVourcher')}
                sorter
                sortOrder={getColumnSortOrder<EVoucherContent>('eVourcher', sorter)}
                render={(eVourcher: EVoucher) => {
                       return (
                         <>
                           {eVourcher && eVourcher.id}
                         </>
                       );
                     }}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, eVoucherContent: EVoucherContent) => {
                  return (
                    <>
                      <Link to={path.join(E_VOUCHER_CONTENT_ROUTE, id.toString())}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(eVoucherContent)}>
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

export default withRouter(EVoucherContentMaster);
