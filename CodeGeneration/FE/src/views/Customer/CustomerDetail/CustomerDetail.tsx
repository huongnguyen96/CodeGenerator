
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

import {CUSTOMER_ROUTE} from 'config/route-consts';
import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import './CustomerDetail.scss';
import customerDetailRepository from './CustomerDetailRepository';

function CustomerDetail(props) {
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
  const [customer, loading] = useDetail<Customer>(id, customerDetailRepository.get, new Customer());

  function handleSubmit() {
    form.validateFields((validationError: Error, customer: Customer) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      customerDetailRepository.save(customer)
        .subscribe(
          () => {
            notification.success({
              message: translate('customerDetail.update.success'),
            });
            props.history.push(path.join(CUSTOMER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('customerDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(CUSTOMER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('customerDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: customer.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('customerDetail.username')}>
          {form.getFieldDecorator('username', {
            initialValue: customer.username,
            rules: [
              {
                required: true,
                message: translate('customerDetail.errors.username.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('customerDetail.displayName')}>
          {form.getFieldDecorator('displayName', {
            initialValue: customer.displayName,
            rules: [
              {
                required: true,
                message: translate('customerDetail.errors.displayName.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('customerDetail.phoneNumber')}>
          {form.getFieldDecorator('phoneNumber', {
            initialValue: customer.phoneNumber,
            rules: [
              {
                required: true,
                message: translate('customerDetail.errors.phoneNumber.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('customerDetail.email')}>
          {form.getFieldDecorator('email', {
            initialValue: customer.email,
            rules: [
              {
                required: true,
                message: translate('customerDetail.errors.email.required'),
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

export default Form.create()(withRouter(CustomerDetail));
