import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import {useList} from 'core/hooks/useList';
import {renderIndex} from 'helpers/renderIndex';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Link} from 'react-router-dom';
import './ProvinceList.scss';
import provinceListRepository from './ProvinceListRepository';

const {Column} = Table;

function ProvinceList() {
  const [translate] = useTranslation();
  const [search, setSearch] = useState<ProvinceSearch>(new ProvinceSearch());
  const [provinces, total, loading] = useList<Province, ProvinceSearch>(
    [],
    search,
    provinceListRepository.list,
    provinceListRepository.count,
  );

  return (
    <Card title={translate('provinces.title')}>
      <Table dataSource={provinces}
             rowKey="id"
             loading={loading}
             pagination={{
               total,
               onChange(page: number, pageSize: number) {
                 const skip: number = (page - 1) * pageSize;
                 setSearch(new ProvinceSearch({
                   ...search,
                   skip,
                   take: pageSize,
                 }));
               },
             }}>
        <Column key="index"
                title={translate('provinces.index')}
                render={renderIndex<Province, ProvinceSearch>(search)}
                sorter
        />
        <Column key="code"
                dataIndex="code"
                title={translate('provinces.code')}
        />
        <Column key="name"
                dataIndex="name"
                title={translate('provinces.name')}
        />
        <Column key="id"
                dataIndex="id"
                title={translate('provinces.actions')}
                render={(id: string) => (
                  <Link to={`/admin/provinces/${id}`}>
                    {translate('general.actions.edit')}
                  </Link>
                )}
        />
      </Table>
    </Card>
  );
}

export default ProvinceList;
