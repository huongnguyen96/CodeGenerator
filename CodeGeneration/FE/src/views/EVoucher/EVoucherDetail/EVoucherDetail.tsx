
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

import {E_VOUCHER_ROUTE} from 'config/route-consts';
import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';
import './EVoucherDetail.scss';
import eVoucherDetailRepository from './EVoucherDetailRepository';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

function EVoucherDetail(props) {
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
  const [eVoucher, loading] = useDetail<EVoucher>(id, eVoucherDetailRepository.get, new EVoucher());

  const [customerSearch, setCustomerSearch] = useState<CustomerSearch>(new CustomerSearch());
  const [productSearch, setProductSearch] = useState<ProductSearch>(new ProductSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, eVoucher: EVoucher) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      eVoucherDetailRepository.save(eVoucher)
        .subscribe(
          () => {
            notification.success({
              message: translate('eVoucherDetail.update.success'),
            });
            props.history.push(path.join(E_VOUCHER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('eVoucherDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(E_VOUCHER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('eVoucherDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: eVoucher.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('eVoucherDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: eVoucher.name,
            rules: [
              {
                required: true,
                message: translate('eVoucherDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('eVoucherDetail.start')}>
          {
            form.getFieldDecorator(
                'start',
                {
                    initialValue: eVoucher.start,
                    rules: [
                    ],
                },
            )
            (<DatePicker/>)
          }
        </Form.Item>

        <Form.Item label={translate('eVoucherDetail.end')}>
          {
            form.getFieldDecorator(
                'end',
                {
                    initialValue: eVoucher.end,
                    rules: [
                    ],
                },
            )
            (<DatePicker/>)
          }
        </Form.Item>

        <Form.Item label={translate('eVoucherDetail.quantity')}>
          {form.getFieldDecorator('quantity', {
            initialValue: eVoucher.quantity,
            rules: [
              {
                required: true,
                message: translate('eVoucherDetail.errors.quantity.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('eVoucherDetail.customer')}>
            {
                form.getFieldDecorator(
                    'customerId',
                    {
                        initialValue: eVoucher.customer
                            ? eVoucher.customer.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={eVoucherDetailRepository.singleListCustomer}
                                  search={customerSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCustomerSearch}>
                      {eVoucher.customer && (
                        <Option value={eVoucher.customer.id}>
                          {eVoucher.customer.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('eVoucherDetail.product')}>
            {
                form.getFieldDecorator(
                    'productId',
                    {
                        initialValue: eVoucher.product
                            ? eVoucher.product.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={eVoucherDetailRepository.singleListProduct}
                                  search={productSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProductSearch}>
                      {eVoucher.product && (
                        <Option value={eVoucher.product.id}>
                          {eVoucher.product.id}
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

export default Form.create()(withRouter(EVoucherDetail));
