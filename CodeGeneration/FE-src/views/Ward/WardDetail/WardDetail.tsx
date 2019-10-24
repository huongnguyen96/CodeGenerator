
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

import {WARD_ROUTE} from 'config/route-consts';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';
import './WardDetail.scss';
import wardDetailRepository from './WardDetailRepository';

import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';

function WardDetail(props) {
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
  const [ward, loading] = useDetail<Ward>(id, wardDetailRepository.get, new Ward());
  
  const [districtSearch, setDistrictSearch] = useState<DistrictSearch>(new DistrictSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, ward: Ward) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      wardDetailRepository.save(ward)
        .subscribe(
          () => {
            notification.success({
              message: translate('wardDetail.update.success'),
            });
            props.history.push(path.join(WARD_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('wardDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(WARD_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('wardDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: ward.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('wardDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: ward.name,
            rules: [
              {
                required: true,
                message: translate('wardDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('wardDetail.orderNumber')}>
          {form.getFieldDecorator('orderNumber', {
            initialValue: ward.orderNumber,
            rules: [
              {
                required: true,
                message: translate('wardDetail.errors.orderNumber.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('wardDetail.district')}>
            {
                form.getFieldDecorator(
                    'districtId', 
                    {
                        initialValue: ward.district 
                            ? ward.district.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={wardDetailRepository.singleListDistrict}
                                  search={districtSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setDistrictSearch}>
                      {ward.district && (
                        <Option value={ward.district.id}>
                          {ward.district.id}
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

export default Form.create()(withRouter(WardDetail));