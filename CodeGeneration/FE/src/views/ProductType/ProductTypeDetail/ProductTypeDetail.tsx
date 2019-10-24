
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

import {PRODUCT_TYPE_ROUTE} from 'config/route-consts';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';
import './ProductTypeDetail.scss';
import productTypeDetailRepository from './ProductTypeDetailRepository';


function ProductTypeDetail(props) {
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
  const [productType, loading] = useDetail<ProductType>(id, productTypeDetailRepository.get, new ProductType());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, productType: ProductType) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      productTypeDetailRepository.save(productType)
        .subscribe(
          () => {
            notification.success({
              message: translate('productTypeDetail.update.success'),
            });
            props.history.push(path.join(PRODUCT_TYPE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('productTypeDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PRODUCT_TYPE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('productTypeDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: productType.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('productTypeDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: productType.code,
            rules: [
              {
                required: true,
                message: translate('productTypeDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productTypeDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: productType.name,
            rules: [
              {
                required: true,
                message: translate('productTypeDetail.errors.name.required'),
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

export default Form.create()(withRouter(ProductTypeDetail));