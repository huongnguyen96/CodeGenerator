
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

import {CUSTOMER_GROUPING_ROUTE} from 'config/route-consts';
import {CustomerGrouping} from 'models/CustomerGrouping';
import './CustomerGroupingDetail.scss';
import customerGroupingDetailRepository from './CustomerGroupingDetailRepository';


function CustomerGroupingDetail(props) {
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
  const [customerGrouping, loading] = useDetail<CustomerGrouping>(id, customerGroupingDetailRepository.get, new CustomerGrouping());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, customerGrouping: CustomerGrouping) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      customerGroupingDetailRepository.save(customerGrouping)
        .subscribe(
          () => {
            notification.success({
              message: translate('customerGroupingDetail.update.success'),
            });
            props.history.push(path.join(CUSTOMER_GROUPING_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('customerGroupingDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(CUSTOMER_GROUPING_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('customerGroupingDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: customerGrouping.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('customerGroupingDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: customerGrouping.name,
            rules: [
              {
                required: true,
                message: translate('customerGroupingDetail.errors.name.required'),
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

export default Form.create()(withRouter(CustomerGroupingDetail));