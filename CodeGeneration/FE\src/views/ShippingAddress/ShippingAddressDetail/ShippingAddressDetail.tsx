
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

import {SHIPPING_ADDRESS_ROUTE} from 'config/route-consts';
import {ShippingAddress} from 'models/ShippingAddress';
import {ShippingAddressSearch} from 'models/ShippingAddressSearch';
import './ShippingAddressDetail.scss';
import shippingAddressDetailRepository from './ShippingAddressDetailRepository';

import {Customer} from 'models/Customer';
import {CustomerSearch} from 'models/CustomerSearch';
import {District} from 'models/District';
import {DistrictSearch} from 'models/DistrictSearch';
import {Province} from 'models/Province';
import {ProvinceSearch} from 'models/ProvinceSearch';
import {Ward} from 'models/Ward';
import {WardSearch} from 'models/WardSearch';

function ShippingAddressDetail(props) {
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
  const [shippingAddress, loading] = useDetail<ShippingAddress>(id, shippingAddressDetailRepository.get, new ShippingAddress());
  
  const [customerSearch, setCustomerSearch] = useState<CustomerSearch>(new CustomerSearch());
  const [districtSearch, setDistrictSearch] = useState<DistrictSearch>(new DistrictSearch());
  const [provinceSearch, setProvinceSearch] = useState<ProvinceSearch>(new ProvinceSearch());
  const [wardSearch, setWardSearch] = useState<WardSearch>(new WardSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, shippingAddress: ShippingAddress) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      shippingAddressDetailRepository.save(shippingAddress)
        .subscribe(
          () => {
            notification.success({
              message: translate('shippingAddressDetail.update.success'),
            });
            props.history.push(path.join(SHIPPING_ADDRESS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('shippingAddressDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(SHIPPING_ADDRESS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('shippingAddressDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: shippingAddress.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('shippingAddressDetail.fullName')}>
          {form.getFieldDecorator('fullName', {
            initialValue: shippingAddress.fullName,
            rules: [
              {
                required: true,
                message: translate('shippingAddressDetail.errors.fullName.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('shippingAddressDetail.companyName')}>
          {form.getFieldDecorator('companyName', {
            initialValue: shippingAddress.companyName,
            rules: [
              {
                required: true,
                message: translate('shippingAddressDetail.errors.companyName.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('shippingAddressDetail.phoneNumber')}>
          {form.getFieldDecorator('phoneNumber', {
            initialValue: shippingAddress.phoneNumber,
            rules: [
              {
                required: true,
                message: translate('shippingAddressDetail.errors.phoneNumber.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('shippingAddressDetail.address')}>
          {form.getFieldDecorator('address', {
            initialValue: shippingAddress.address,
            rules: [
              {
                required: true,
                message: translate('shippingAddressDetail.errors.address.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('shippingAddressDetail.isDefault')}>
          {form.getFieldDecorator('isDefault', {
            initialValue: shippingAddress.isDefault,
            rules: [
              {
                required: true,
                message: translate('shippingAddressDetail.errors.isDefault.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('shippingAddressDetail.customer')}>
            {
                form.getFieldDecorator(
                    'customerId', 
                    {
                        initialValue: shippingAddress.customer 
                            ? shippingAddress.customer.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={shippingAddressDetailRepository.singleListCustomer}
                                  search={customerSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCustomerSearch}>
                      {shippingAddress.customer && (
                        <Option value={shippingAddress.customer.id}>
                          {shippingAddress.customer.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('shippingAddressDetail.district')}>
            {
                form.getFieldDecorator(
                    'districtId', 
                    {
                        initialValue: shippingAddress.district 
                            ? shippingAddress.district.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={shippingAddressDetailRepository.singleListDistrict}
                                  search={districtSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setDistrictSearch}>
                      {shippingAddress.district && (
                        <Option value={shippingAddress.district.id}>
                          {shippingAddress.district.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('shippingAddressDetail.province')}>
            {
                form.getFieldDecorator(
                    'provinceId', 
                    {
                        initialValue: shippingAddress.province 
                            ? shippingAddress.province.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={shippingAddressDetailRepository.singleListProvince}
                                  search={provinceSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProvinceSearch}>
                      {shippingAddress.province && (
                        <Option value={shippingAddress.province.id}>
                          {shippingAddress.province.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('shippingAddressDetail.ward')}>
            {
                form.getFieldDecorator(
                    'wardId', 
                    {
                        initialValue: shippingAddress.ward 
                            ? shippingAddress.ward.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={shippingAddressDetailRepository.singleListWard}
                                  search={wardSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setWardSearch}>
                      {shippingAddress.ward && (
                        <Option value={shippingAddress.ward.id}>
                          {shippingAddress.ward.id}
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

export default Form.create()(withRouter(ShippingAddressDetail));