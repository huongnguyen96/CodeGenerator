
import Card from 'antd/lib/card';
import Form from 'antd/lib/form';
import Input from 'antd/lib/input';
import DatePicker from 'antd/lib/date-picker';
import Spin from 'antd/lib/spin';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle';
import SingleSelect, {Option} from 'components/SingleSelect';
import {useDetail} from 'core/hooks/useDetail';
import {usePagination} from 'core/hooks/usePagination';
import {notification} from 'helpers';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {withRouter} from 'react-router-dom';

import {DISTRICT_ROUTE} from 'config/route-consts';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import './DistrictDetail.scss';
import districtDetailRepository from './DistrictDetailRepository';

import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';

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
              message: translate('districtDetail.update.success'),
            });
            props.history.push(path.join(DISTRICT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('districtDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(DISTRICT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('districtDetail.detail.title', {
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
        
        <Form.Item label={translate('districtDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: district.name,
            rules: [
              {
                required: true,
                message: translate('districtDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('districtDetail.orderNumber')}>
          {form.getFieldDecorator('orderNumber', {
            initialValue: district.orderNumber,
            rules: [
              {
                required: true,
                message: translate('districtDetail.errors.orderNumber.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('districtDetail.province')}>
            {
                form.getFieldDecorator(
                    'provinceId', 
                    {
                        initialValue: district.province 
                            ? district.province.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={districtDetailRepository.singleListProvince}
                                  search={provinceSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProvinceSearch}>
                      {district.province && (
                        <Option value={district.province.id}>
                          {district.province.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(DistrictDetail));