
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

import {PAYMENT_METHOD_ROUTE} from 'config/route-consts';
import {PaymentMethod} from 'models/PaymentMethod';
import {PaymentMethodSearch} from 'models/PaymentMethodSearch';
import './PaymentMethodDetail.scss';
import paymentMethodDetailRepository from './PaymentMethodDetailRepository';


function PaymentMethodDetail(props) {
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
  const [paymentMethod, loading] = useDetail<PaymentMethod>(id, paymentMethodDetailRepository.get, new PaymentMethod());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, paymentMethod: PaymentMethod) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      paymentMethodDetailRepository.save(paymentMethod)
        .subscribe(
          () => {
            notification.success({
              message: translate('paymentMethodDetail.update.success'),
            });
            props.history.push(path.join(PAYMENT_METHOD_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('paymentMethodDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PAYMENT_METHOD_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('paymentMethodDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: paymentMethod.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('paymentMethodDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: paymentMethod.code,
            rules: [
              {
                required: true,
                message: translate('paymentMethodDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('paymentMethodDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: paymentMethod.name,
            rules: [
              {
                required: true,
                message: translate('paymentMethodDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('paymentMethodDetail.description')}>
          {form.getFieldDecorator('description', {
            initialValue: paymentMethod.description,
            rules: [
              {
                required: true,
                message: translate('paymentMethodDetail.errors.description.required'),
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

export default Form.create()(withRouter(PaymentMethodDetail));