
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

import {PRODUCT_STATUS_ROUTE} from 'config/route-consts';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import './ProductStatusDetail.scss';
import productStatusDetailRepository from './ProductStatusDetailRepository';


function ProductStatusDetail(props) {
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
  const [productStatus, loading] = useDetail<ProductStatus>(id, productStatusDetailRepository.get, new ProductStatus());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, productStatus: ProductStatus) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      productStatusDetailRepository.save(productStatus)
        .subscribe(
          () => {
            notification.success({
              message: translate('productStatusDetail.update.success'),
            });
            props.history.push(path.join(PRODUCT_STATUS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('productStatusDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PRODUCT_STATUS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('productStatusDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: productStatus.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('productStatusDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: productStatus.code,
            rules: [
              {
                required: true,
                message: translate('productStatusDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productStatusDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: productStatus.name,
            rules: [
              {
                required: true,
                message: translate('productStatusDetail.errors.name.required'),
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

export default Form.create()(withRouter(ProductStatusDetail));