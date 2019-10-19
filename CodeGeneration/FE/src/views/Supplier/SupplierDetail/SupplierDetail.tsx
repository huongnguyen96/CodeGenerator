
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

import {SUPPLIER_ROUTE} from 'config/route-consts';
import {Supplier} from 'models/Supplier';
import './SupplierDetail.scss';
import supplierDetailRepository from './SupplierDetailRepository';

function SupplierDetail(props) {
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
  const [supplier, loading] = useDetail<Supplier>(id, supplierDetailRepository.get, new Supplier());

  function handleSubmit() {
    form.validateFields((validationError: Error, supplier: Supplier) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      supplierDetailRepository.save(supplier)
        .subscribe(
          () => {
            notification.success({
              message: translate('supplierDetail.update.success'),
            });
            props.history.push(path.join(SUPPLIER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('supplierDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(SUPPLIER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('supplierDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: supplier.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('supplierDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: supplier.name,
            rules: [
              {
                required: true,
                message: translate('supplierDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('supplierDetail.phone')}>
          {form.getFieldDecorator('code', {
            initialValue: supplier.phone,
            rules: [
              {
                required: true,
                message: translate('supplierDetail.errors.phone.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('supplierDetail.contactPerson')}>
          {form.getFieldDecorator('code', {
            initialValue: supplier.contactPerson,
            rules: [
              {
                required: true,
                message: translate('supplierDetail.errors.contactPerson.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('supplierDetail.address')}>
          {form.getFieldDecorator('code', {
            initialValue: supplier.address,
            rules: [
              {
                required: true,
                message: translate('supplierDetail.errors.address.required'),
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

export default Form.create()(withRouter(SupplierDetail));
