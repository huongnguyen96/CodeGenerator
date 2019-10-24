
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

import {DISCOUNT_ROUTE} from 'config/route-consts';
import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import './DiscountDetail.scss';
import discountDetailRepository from './DiscountDetailRepository';

function DiscountDetail(props) {
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
  const [discount, loading] = useDetail<Discount>(id, discountDetailRepository.get, new Discount());

  function handleSubmit() {
    form.validateFields((validationError: Error, discount: Discount) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      discountDetailRepository.save(discount)
        .subscribe(
          () => {
            notification.success({
              message: translate('discountDetail.update.success'),
            });
            props.history.push(path.join(DISCOUNT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('discountDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(DISCOUNT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('discountDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: discount.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('discountDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: discount.name,
            rules: [
              {
                required: true,
                message: translate('discountDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('discountDetail.start')}>
          {
            form.getFieldDecorator(
                'start',
                {
                    initialValue: discount.start,
                    rules: [
                    ],
                },
            )
            (<DatePicker/>)
          }
        </Form.Item>

        <Form.Item label={translate('discountDetail.end')}>
          {
            form.getFieldDecorator(
                'end',
                {
                    initialValue: discount.end,
                    rules: [
                    ],
                },
            )
            (<DatePicker/>)
          }
        </Form.Item>

        <Form.Item label={translate('discountDetail.type')}>
          {form.getFieldDecorator('type', {
            initialValue: discount.type,
            rules: [
              {
                required: true,
                message: translate('discountDetail.errors.type.required'),
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

export default Form.create()(withRouter(DiscountDetail));
