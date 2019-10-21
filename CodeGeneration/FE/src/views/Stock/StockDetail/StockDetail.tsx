
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

import {STOCK_ROUTE} from 'config/route-consts';
import {Stock} from 'models/Stock';
import './StockDetail.scss';
import stockDetailRepository from './StockDetailRepository';

import {UnitSearch} from 'models/UnitSearch';
import {WarehouseSearch} from 'models/WarehouseSearch';

function StockDetail(props) {
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
  const [stock, loading] = useDetail<Stock>(id, stockDetailRepository.get, new Stock());
  
  const [unitSearch, setUnitSearch] = useState<UnitSearch>(new UnitSearch());
  const [warehouseSearch, setWarehouseSearch] = useState<WarehouseSearch>(new WarehouseSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, stock: Stock) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      stockDetailRepository.save(stock)
        .subscribe(
          () => {
            notification.success({
              message: translate('stockDetail.update.success'),
            });
            props.history.push(path.join(STOCK_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('stockDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(STOCK_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('stockDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: stock.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('stockDetail.unitId')}>
          {form.getFieldDecorator('code', {
            initialValue: stock.unitId,
            rules: [
              {
                required: true,
                message: translate('stockDetail.errors.unitId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('stockDetail.warehouseId')}>
          {form.getFieldDecorator('code', {
            initialValue: stock.warehouseId,
            rules: [
              {
                required: true,
                message: translate('stockDetail.errors.warehouseId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('stockDetail.quantity')}>
          {form.getFieldDecorator('code', {
            initialValue: stock.quantity,
            rules: [
              {
                required: true,
                message: translate('stockDetail.errors.quantity.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('stockDetail.unit')}>
            {
                form.getFieldDecorator(
                    'unitId', 
                    {
                        initialValue: stock.unit 
                            ? stock.unit.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={stockDetailRepository.singleListUnit}
                                  search={unitSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setUnitSearch}>
                      {stock.unit && (
                        <Option value={stock.unit.id}>
                          {stock.unit.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('stockDetail.warehouse')}>
            {
                form.getFieldDecorator(
                    'warehouseId', 
                    {
                        initialValue: stock.warehouse 
                            ? stock.warehouse.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={stockDetailRepository.singleListWarehouse}
                                  search={warehouseSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setWarehouseSearch}>
                      {stock.warehouse && (
                        <Option value={stock.warehouse.id}>
                          {stock.warehouse.name}
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

export default Form.create()(withRouter(StockDetail));