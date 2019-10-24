
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

import {MERCHANT_ADDRESS_ROUTE} from 'config/route-consts';
import {MerchantAddress} from 'models/MerchantAddress';
import {MerchantAddressSearch} from 'models/MerchantAddressSearch';
import './MerchantAddressDetail.scss';
import merchantAddressDetailRepository from './MerchantAddressDetailRepository';

import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';

function MerchantAddressDetail(props) {
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
  const [merchantAddress, loading] = useDetail<MerchantAddress>(id, merchantAddressDetailRepository.get, new MerchantAddress());

  const [merchantSearch, setMerchantSearch] = useState<MerchantSearch>(new MerchantSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, merchantAddress: MerchantAddress) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      merchantAddressDetailRepository.save(merchantAddress)
        .subscribe(
          () => {
            notification.success({
              message: translate('merchantAddressDetail.update.success'),
            });
            props.history.push(path.join(MERCHANT_ADDRESS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('merchantAddressDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(MERCHANT_ADDRESS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('merchantAddressDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: merchantAddress.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('merchantAddressDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: merchantAddress.code,
            rules: [
              {
                required: true,
                message: translate('merchantAddressDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantAddressDetail.address')}>
          {form.getFieldDecorator('address', {
            initialValue: merchantAddress.address,
            rules: [
              {
                required: true,
                message: translate('merchantAddressDetail.errors.address.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantAddressDetail.contact')}>
          {form.getFieldDecorator('contact', {
            initialValue: merchantAddress.contact,
            rules: [
              {
                required: true,
                message: translate('merchantAddressDetail.errors.contact.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantAddressDetail.phone')}>
          {form.getFieldDecorator('phone', {
            initialValue: merchantAddress.phone,
            rules: [
              {
                required: true,
                message: translate('merchantAddressDetail.errors.phone.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('merchantAddressDetail.merchant')}>
            {
                form.getFieldDecorator(
                    'merchantId',
                    {
                        initialValue: merchantAddress.merchant
                            ? merchantAddress.merchant.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={merchantAddressDetailRepository.singleListMerchant}
                                  search={merchantSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setMerchantSearch}>
                      {merchantAddress.merchant && (
                        <Option value={merchantAddress.merchant.id}>
                          {merchantAddress.merchant.id}
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

export default Form.create()(withRouter(MerchantAddressDetail));
