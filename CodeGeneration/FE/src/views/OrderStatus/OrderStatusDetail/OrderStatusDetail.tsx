
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

import {ORDER_STATUS_ROUTE} from 'config/route-consts';
import {OrderStatus} from 'models/OrderStatus';
import {OrderStatusSearch} from 'models/OrderStatusSearch';
import './OrderStatusDetail.scss';
import orderStatusDetailRepository from './OrderStatusDetailRepository';

function OrderStatusDetail(props) {
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
  const [orderStatus, loading] = useDetail<OrderStatus>(id, orderStatusDetailRepository.get, new OrderStatus());

  function handleSubmit() {
    form.validateFields((validationError: Error, orderStatus: OrderStatus) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      orderStatusDetailRepository.save(orderStatus)
        .subscribe(
          () => {
            notification.success({
              message: translate('orderStatusDetail.update.success'),
            });
            props.history.push(path.join(ORDER_STATUS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('orderStatusDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ORDER_STATUS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('orderStatusDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: orderStatus.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('orderStatusDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: orderStatus.code,
            rules: [
              {
                required: true,
                message: translate('orderStatusDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderStatusDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: orderStatus.name,
            rules: [
              {
                required: true,
                message: translate('orderStatusDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderStatusDetail.description')}>
          {form.getFieldDecorator('description', {
            initialValue: orderStatus.description,
            rules: [
              {
                required: true,
                message: translate('orderStatusDetail.errors.description.required'),
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

export default Form.create()(withRouter(OrderStatusDetail));
