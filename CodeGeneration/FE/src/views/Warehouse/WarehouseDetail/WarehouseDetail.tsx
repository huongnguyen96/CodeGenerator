
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

import {WAREHOUSE_ROUTE} from 'config/route-consts';
import {Warehouse} from 'models/Warehouse';
import './WarehouseDetail.scss';
import warehouseDetailRepository from './WarehouseDetailRepository';

import {PartnerSearch} from 'models/PartnerSearch';

function WarehouseDetail(props) {
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
  const [warehouse, loading] = useDetail<Warehouse>(id, warehouseDetailRepository.get, new Warehouse());
  
  const [partnerSearch, setPartnerSearch] = useState<PartnerSearch>(new PartnerSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, warehouse: Warehouse) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      warehouseDetailRepository.save(warehouse)
        .subscribe(
          () => {
            notification.success({
              message: translate('warehouseDetail.update.success'),
            });
            props.history.push(path.join(WAREHOUSE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('warehouseDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(WAREHOUSE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('warehouseDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: warehouse.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('warehouseDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: warehouse.name,
            rules: [
              {
                required: true,
                message: translate('warehouseDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('warehouseDetail.phone')}>
          {form.getFieldDecorator('code', {
            initialValue: warehouse.phone,
            rules: [
              {
                required: true,
                message: translate('warehouseDetail.errors.phone.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('warehouseDetail.email')}>
          {form.getFieldDecorator('code', {
            initialValue: warehouse.email,
            rules: [
              {
                required: true,
                message: translate('warehouseDetail.errors.email.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('warehouseDetail.address')}>
          {form.getFieldDecorator('code', {
            initialValue: warehouse.address,
            rules: [
              {
                required: true,
                message: translate('warehouseDetail.errors.address.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('warehouseDetail.partnerId')}>
          {form.getFieldDecorator('code', {
            initialValue: warehouse.partnerId,
            rules: [
              {
                required: true,
                message: translate('warehouseDetail.errors.partnerId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('warehouseDetail.partner')}>
            {
                form.getFieldDecorator(
                    'partnerId', 
                    {
                        initialValue: warehouse.partner 
                            ? warehouse.partner.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={warehouseDetailRepository.singleListPartner}
                                  search={partnerSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setPartnerSearch}>
                      {warehouse.partner && (
                        <Option value={warehouse.partner.id}>
                          {warehouse.partner.name}
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

export default Form.create()(withRouter(WarehouseDetail));