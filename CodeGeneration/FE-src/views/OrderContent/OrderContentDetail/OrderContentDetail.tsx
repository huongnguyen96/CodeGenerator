
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

import {ORDER_CONTENT_ROUTE} from 'config/route-consts';
import {OrderContent} from 'models/OrderContent';
import {OrderContentSearch} from 'models/OrderContentSearch';
import './OrderContentDetail.scss';
import orderContentDetailRepository from './OrderContentDetailRepository';

import {Order} from 'models/Order';
import {OrderSearch} from 'models/OrderSearch';

function OrderContentDetail(props) {
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
  const [orderContent, loading] = useDetail<OrderContent>(id, orderContentDetailRepository.get, new OrderContent());
  
  const [orderSearch, setOrderSearch] = useState<OrderSearch>(new OrderSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, orderContent: OrderContent) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      orderContentDetailRepository.save(orderContent)
        .subscribe(
          () => {
            notification.success({
              message: translate('orderContentDetail.update.success'),
            });
            props.history.push(path.join(ORDER_CONTENT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('orderContentDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ORDER_CONTENT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('orderContentDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: orderContent.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('orderContentDetail.itemName')}>
          {form.getFieldDecorator('itemName', {
            initialValue: orderContent.itemName,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.itemName.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderContentDetail.firstVersion')}>
          {form.getFieldDecorator('firstVersion', {
            initialValue: orderContent.firstVersion,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.firstVersion.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderContentDetail.secondVersion')}>
          {form.getFieldDecorator('secondVersion', {
            initialValue: orderContent.secondVersion,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.secondVersion.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderContentDetail.thirdVersion')}>
          {form.getFieldDecorator('thirdVersion', {
            initialValue: orderContent.thirdVersion,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.thirdVersion.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderContentDetail.price')}>
          {form.getFieldDecorator('price', {
            initialValue: orderContent.price,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.price.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('orderContentDetail.discountPrice')}>
          {form.getFieldDecorator('discountPrice', {
            initialValue: orderContent.discountPrice,
            rules: [
              {
                required: true,
                message: translate('orderContentDetail.errors.discountPrice.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('orderContentDetail.order')}>
            {
                form.getFieldDecorator(
                    'orderId', 
                    {
                        initialValue: orderContent.order 
                            ? orderContent.order.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={orderContentDetailRepository.singleListOrder}
                                  search={orderSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setOrderSearch}>
                      {orderContent.order && (
                        <Option value={orderContent.order.id}>
                          {orderContent.order.id}
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

export default Form.create()(withRouter(OrderContentDetail));