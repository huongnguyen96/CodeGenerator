
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

import {ORDER_ROUTE} from 'config/route-consts';
import {Order} from 'models/Order';
import './OrderDetail.scss';
import orderDetailRepository from './OrderDetailRepository';

import {CustomerSearch} from 'models/CustomerSearch';

function OrderDetail(props) {
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
  const [order, loading] = useDetail<Order>(id, orderDetailRepository.get, new Order());
  
  const [customerSearch, setCustomerSearch] = useState<CustomerSearch>(new CustomerSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, order: Order) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      orderDetailRepository.save(order)
        .subscribe(
          () => {
            notification.success({
              message: translate('orderDetail.update.success'),
            });
            props.history.push(path.join(ORDER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('orderDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ORDER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('orderDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: order.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('orderDetail.customerId')}>
          {form.getFieldDecorator('code', {
            initialValue: order.customerId,
            rules: [
              {
                required: true,
                message: translate('orderDetail.errors.customerId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderDetail.createdDate')}>
          {
            form.getFieldDecorator(
                'name', 
                {
                    initialValue: order.createdDate,
                    rules: [
                    ],
                }
            )
            (<DatePicker/>)
          }
        </Form.Item>

        <Form.Item label={translate('orderDetail.voucherCode')}>
          {form.getFieldDecorator('code', {
            initialValue: order.voucherCode,
            rules: [
              {
                required: true,
                message: translate('orderDetail.errors.voucherCode.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderDetail.total')}>
          {form.getFieldDecorator('code', {
            initialValue: order.total,
            rules: [
              {
                required: true,
                message: translate('orderDetail.errors.total.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderDetail.voucherDiscount')}>
          {form.getFieldDecorator('code', {
            initialValue: order.voucherDiscount,
            rules: [
              {
                required: true,
                message: translate('orderDetail.errors.voucherDiscount.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderDetail.campaignDiscount')}>
          {form.getFieldDecorator('code', {
            initialValue: order.campaignDiscount,
            rules: [
              {
                required: true,
                message: translate('orderDetail.errors.campaignDiscount.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('orderDetail.customer')}>
            {
                form.getFieldDecorator(
                    'customerId', 
                    {
                        initialValue: order.customer 
                            ? order.customer.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={orderDetailRepository.singleListCustomer}
                                  search={customerSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCustomerSearch}>
                      {order.customer && (
                        <Option value={order.customer.id}>
                          {order.customer.name}
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

export default Form.create()(withRouter(OrderDetail));