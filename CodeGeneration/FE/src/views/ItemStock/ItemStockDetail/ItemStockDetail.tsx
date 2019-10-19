
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

import {ITEM_STOCK_ROUTE} from 'config/route-consts';
import {ItemStock} from 'models/ItemStock';
import './ItemStockDetail.scss';
import itemStockDetailRepository from './ItemStockDetailRepository';

import {ItemSearch} from 'models/ItemSearch';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {WarehouseSearch} from 'models/WarehouseSearch';

function ItemStockDetail(props) {
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
  const [itemStock, loading] = useDetail<ItemStock>(id, itemStockDetailRepository.get, new ItemStock());

  const [itemSearch, setItemSearch] = useState<ItemSearch>(new ItemSearch());
  const [itemUnitOfMeasureSearch, setItemUnitOfMeasureSearch] = useState<ItemUnitOfMeasureSearch>(new ItemUnitOfMeasureSearch());
  const [warehouseSearch, setWarehouseSearch] = useState<WarehouseSearch>(new WarehouseSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, itemStock: ItemStock) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemStockDetailRepository.save(itemStock)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemStockDetail.update.success'),
            });
            props.history.push(path.join(ITEM_STOCK_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemStockDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_STOCK_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemStockDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: itemStock.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('itemStockDetail.itemId')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStock.itemId,
            rules: [
              {
                required: true,
                message: translate('itemStockDetail.errors.itemId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemStockDetail.warehouseId')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStock.warehouseId,
            rules: [
              {
                required: true,
                message: translate('itemStockDetail.errors.warehouseId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemStockDetail.unitOfMeasureId')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStock.unitOfMeasureId,
            rules: [
              {
                required: true,
                message: translate('itemStockDetail.errors.unitOfMeasureId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemStockDetail.quantity')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStock.quantity,
            rules: [
              {
                required: true,
                message: translate('itemStockDetail.errors.quantity.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemStockDetail.item')}>
            {
                form.getFieldDecorator(
                    'itemId',
                    {
                        initialValue: itemStock.item
                            ? itemStock.item.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemStockDetailRepository.singleListItem}
                                  search={itemSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemSearch}>
                      {itemStock.item && (
                        <Option value={itemStock.item.id}>
                          {itemStock.item.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemStockDetail.unitOfMeasure')}>
            {
                form.getFieldDecorator(
                    'unitOfMeasureId',
                    {
                        initialValue: itemStock.unitOfMeasure
                            ? itemStock.unitOfMeasure.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemStockDetailRepository.singleListItemUnitOfMeasure}
                                  search={itemUnitOfMeasureSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemUnitOfMeasureSearch}>
                      {itemStock.unitOfMeasure && (
                        <Option value={itemStock.unitOfMeasure.id}>
                          {itemStock.unitOfMeasure.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemStockDetail.warehouse')}>
            {
                form.getFieldDecorator(
                    'warehouseId',
                    {
                        initialValue: itemStock.warehouse
                            ? itemStock.warehouse.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemStockDetailRepository.singleListWarehouse}
                                  search={warehouseSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setWarehouseSearch}>
                      {itemStock.warehouse && (
                        <Option value={itemStock.warehouse.id}>
                          {itemStock.warehouse.name}
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

export default Form.create()(withRouter(ItemStockDetail));
