
import Card from 'antd/lib/card';
import DatePicker from 'antd/lib/date-picker';
import Form from 'antd/lib/form';
import Input from 'antd/lib/input';
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

import {PROVINCE_ROUTE} from 'config/route-consts';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import './ProvinceDetail.scss';
import provinceDetailRepository from './ProvinceDetailRepository';

function ProvinceDetail(props) {
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
  const [province, loading] = useDetail<Province>(id, provinceDetailRepository.get, new Province());

  function handleSubmit() {
    form.validateFields((validationError: Error, province: Province) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      provinceDetailRepository.save(province)
        .subscribe(
          () => {
            notification.success({
              message: translate('provinceDetail.update.success'),
            });
            props.history.push(path.join(PROVINCE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('provinceDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PROVINCE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('provinceDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: province.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('provinceDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: province.name,
            rules: [
              {
                required: true,
                message: translate('provinceDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('provinceDetail.orderNumber')}>
          {form.getFieldDecorator('orderNumber', {
            initialValue: province.orderNumber,
            rules: [
              {
                required: true,
                message: translate('provinceDetail.errors.orderNumber.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(ProvinceDetail));
