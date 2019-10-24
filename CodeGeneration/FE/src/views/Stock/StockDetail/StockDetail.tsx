
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
import {StockSearch} from 'models/StockSearch';
import './StockDetail.scss';
import stockDetailRepository from './StockDetailRepository';

import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import {Warehouse} from 'models/Warehouse';
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
  
  const [itemSearch, setItemSearch] = useState<ItemSearch>(new ItemSearch());
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
        
        <Form.Item label={translate('stockDetail.quantity')}>
          {form.getFieldDecorator('quantity', {
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

        
        <Form.Item label={translate('stockDetail.item')}>
            {
                form.getFieldDecorator(
                    'itemId', 
                    {
                        initialValue: stock.item 
                            ? stock.item.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={stockDetailRepository.singleListItem}
                                  search={itemSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemSearch}>
                      {stock.item && (
                        <Option value={stock.item.id}>
                          {stock.item.id}
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
                          {stock.warehouse.id}
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