import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import {useList} from 'core/hooks/useList';
import {renderIndex} from 'helpers/renderIndex';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Link} from 'react-router-dom';
import './DistrictList.scss';
import districtListRepository from './DistrictListRepository';

const {Column} = Table;

function DistrictList() {
  const [translate] = useTranslation();
  const [search, setSearch] = useState<DistrictSearch>(new DistrictSearch());
  const [districts, total, loading] = useList<District, DistrictSearch>(
    [],
    search,
    districtListRepository.list,
    districtListRepository.count,
  );

  return (
    <Card title={translate('districts.title')}>
      <Table dataSource={districts}
             rowKey="id"
             loading={loading}
             pagination={{
               total,
               onChange(page: number, pageSize: number) {
                 const skip: number = (page - 1) * pageSize;
                 setSearch(new DistrictSearch({
                   ...search,
                   skip,
                   take: pageSize,
                 }));
               },
             }}>
        <Column key="index"
                title={translate('districts.index')}
                render={renderIndex<District, DistrictSearch>(search)}
                sorter
        />
        <Column key="code"
                dataIndex="code"
                title={translate('districts.code')}
        />
        <Column key="name"
                dataIndex="name"
                title={translate('districts.name')}
        />
        <Column key="id"
                dataIndex="id"
                title={translate('districts.actions')}
                render={(id: string) => (
                  <Link to={`/admin/districts/${id}`}>
                    {translate('general.actions.edit')}
                  </Link>
                )}
        />
      </Table>
    </Card>
  );
}

export default DistrictList;
