import {Input} from 'antd';
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Form from 'antd/lib/form';
import Spin from 'antd/lib/spin';
import Table from 'antd/lib/table';
import {Pagination} from 'core/entities/Pagination';
import {useDetail} from 'core/hooks/useDetail';
import {useLocalPagination} from 'core/hooks/useLocalPagination';
import {notification} from 'helpers';
import {District} from 'models/District';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {withRouter} from 'react-router-dom';
import {finalize} from 'rxjs/operators';
import SingleSelect from '../../../../components/SingleSelect/SingleSelect';
import {ProvinceSearch} from '../../../../models/ProvinceSearch';
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
  const [provinceSearch, setProvinceSearch] = useState<ProvinceSearch>(new ProvinceSearch());

  const [district, loading] = useDetail<District>(id, districtDetailRepository.get);
  const [pageSpinning, setPageSpinning] = useState<boolean>(false);
  const pagination: Pagination = useLocalPagination();

  function handleSubmit() {
    form.validateFields((validationError: Error, validatedDistrict: District) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      districtDetailRepository.save(validatedDistrict)
        .pipe(
          finalize(() => {
            setPageSpinning(false);
          }),
        )
        .subscribe(
          () => {
            notification.success({
              message: translate('districts.update.success'),
            });
            props.history.push('/admin/districts');
          },
          (error: Error) => {
            notification.error({
              message: translate('districts.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card title={(
        <div className="d-flex">
          {translate('districts.detail.title')}
          <div className="flex-grow-1 d-flex justify-content-end">
            <Button type="primary" loading={pageSpinning} onClick={handleSubmit}>
              {translate('buttons.save')}
            </Button>
          </div>
        </div>
      )}
            loading={loading}>
        {form.getFieldDecorator('id', {
          initialValue: district ? district.id : null,
        })(
          <Input type="hidden"/>,
        )}
        <Item label={translate('districts.code')}>
          {form.getFieldDecorator('code', {
            initialValue: district ? district.code : null,
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
            initialValue: district ? district.name : null,
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
        <Item label={translate('districts.province')}>
          {form.getFieldDecorator('province', {
            initialValue: district ? district.province.id : null,
            rules: [],
          })(
            <SingleSelect getList={districtDetailRepository.listProvince}
                          search={provinceSearch}/>,
          )}
        </Item>
      </Card>

      <Card title={translate('districts.detail.districts.title')}>
        <Table dataSource={district ? district.wards : []}
               rowKey="id"
               pagination={pagination}
               loading={loading}>
          <Column key="code" dataIndex="code" title={translate('districts.detail.districts.code')}/>
          <Column key="name" dataIndex="name" title={translate('districts.detail.districts.name')}/>
        </Table>
      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(DistrictDetail));
