import Card from 'antd/lib/card';
import Form from 'antd/lib/form';
import Input from 'antd/lib/input';
import Spin from 'antd/lib/spin';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle';
import SingleSelect, {Option} from 'components/SingleSelect';
import {ADMIN_DISTRICTS_ROUTE} from 'config/route-consts';
import {useDetail} from 'core/hooks/useDetail';
import {usePagination} from 'core/hooks/usePagination';
import {notification} from 'helpers';
import {District} from 'models/District';
import {ProvinceSearch} from 'models/ProvinceSearch';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {withRouter} from 'react-router-dom';
import './DistrictDetail.scss';
import districtDetailRepository from './DistrictDetailRepository';

const {Item} = Form;
const {Column} = Table;

function DistrictDetail(props) {
  const {
    form,
    match: {
      params: {
        id,
      },
    },
  } = props;

  const [translate] = useTranslation();
  const [pageSpinning, setPageSpinning] = useState<boolean>(false);
  const [district, loading] = useDetail<District>(id, districtDetailRepository.get, new District());
  const [provinceSearch, setProvinceSearch] = useState<ProvinceSearch>(new ProvinceSearch());

  const [pagination] = usePagination();

  function handleSubmit() {
    form.validateFields((validationError: Error, district: District) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      districtDetailRepository.save(district)
        .subscribe(
          () => {
            notification.success({
              message: translate('districts.update.success'),
            });
            props.history.push(path.join(ADMIN_DISTRICTS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('districts.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ADMIN_DISTRICTS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('districts.detail.title', {
              name: district.name,
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: district.id,
        })(
          <Input type="hidden"/>,
        )}
        <Item label={translate('districts.code')}>
          {form.getFieldDecorator('code', {
            initialValue: district.code,
            rules: [
              {
                required: true,
                message: translate('districts.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Item>
        <Item label={translate('districts.name')}>
          {form.getFieldDecorator('name', {
            initialValue: district.name,
            rules: [
              {
                required: true,
                message: translate('districts.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Item>
        <Item label={translate('districts.province')} required>
          {form.getFieldDecorator('provinceId', {
            initialValue: district.province ? district.province.id : null,
          })(
            <SingleSelect getList={districtDetailRepository.listProvince}
                          search={provinceSearch}
                          searchField="name"
                          showSearch
                          setSearch={setProvinceSearch}>
              {district.province && (
                <Option value={district.province.id}>
                  {district.province.name}
                </Option>
              )}
            </SingleSelect>,
          )}
        </Item>
      </Card>

      <Card title={translate('districts.detail.wards.title')}>
        <Table dataSource={district.wards}
               rowKey="id"
               pagination={pagination}
               loading={loading}>
          <Column key="code"
                  dataIndex="code"
                  title={translate('districts.detail.wards.code')}
          />
          <Column key="name"
                  dataIndex="name"
                  title={translate('districts.detail.wards.name')}
          />
        </Table>
      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(DistrictDetail));
