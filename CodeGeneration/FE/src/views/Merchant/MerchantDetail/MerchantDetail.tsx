
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

import {MERCHANT_ROUTE} from 'config/route-consts';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import './MerchantDetail.scss';
import merchantDetailRepository from './MerchantDetailRepository';

function MerchantDetail(props) {
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
  const [merchant, loading] = useDetail<Merchant>(id, merchantDetailRepository.get, new Merchant());

  function handleSubmit() {
    form.validateFields((validationError: Error, merchant: Merchant) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      merchantDetailRepository.save(merchant)
        .subscribe(
          () => {
            notification.success({
              message: translate('merchantDetail.update.success'),
            });
            props.history.push(path.join(MERCHANT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('merchantDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(MERCHANT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('merchantDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: merchant.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('merchantDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: merchant.name,
            rules: [
              {
                required: true,
                message: translate('merchantDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantDetail.phone')}>
          {form.getFieldDecorator('phone', {
            initialValue: merchant.phone,
            rules: [
              {
                required: true,
                message: translate('merchantDetail.errors.phone.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantDetail.contactPerson')}>
          {form.getFieldDecorator('contactPerson', {
            initialValue: merchant.contactPerson,
            rules: [
              {
                required: true,
                message: translate('merchantDetail.errors.contactPerson.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantDetail.address')}>
          {form.getFieldDecorator('address', {
            initialValue: merchant.address,
            rules: [
              {
                required: true,
                message: translate('merchantDetail.errors.address.required'),
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

export default Form.create()(withRouter(MerchantDetail));
