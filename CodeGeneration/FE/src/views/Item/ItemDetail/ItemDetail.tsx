
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

import {ITEM_ROUTE} from 'config/route-consts';
import {Item} from 'models/Item';
import './ItemDetail.scss';
import itemDetailRepository from './ItemDetailRepository';

import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {ItemTypeSearch} from 'models/ItemTypeSearch';
import {ItemUnitOfMeasureSearch} from 'models/ItemUnitOfMeasureSearch';
import {SupplierSearch} from 'models/SupplierSearch';

function ItemDetail(props) {
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
  const [item, loading] = useDetail<Item>(id, itemDetailRepository.get, new Item());

  const [itemStatusSearch, setItemStatusSearch] = useState<ItemStatusSearch>(new ItemStatusSearch());
  const [supplierSearch, setSupplierSearch] = useState<SupplierSearch>(new SupplierSearch());
  const [itemTypeSearch, setItemTypeSearch] = useState<ItemTypeSearch>(new ItemTypeSearch());
  const [itemUnitOfMeasureSearch, setItemUnitOfMeasureSearch] = useState<ItemUnitOfMeasureSearch>(new ItemUnitOfMeasureSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, item: Item) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemDetailRepository.save(item)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemDetail.update.success'),
            });
            props.history.push(path.join(ITEM_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: item.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('itemDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: item.code,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: item.name,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.sKU')}>
          {form.getFieldDecorator('code', {
            initialValue: item.sKU,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.sKU.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.typeId')}>
          {form.getFieldDecorator('code', {
            initialValue: item.typeId,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.typeId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.purchasePrice')}>
          {form.getFieldDecorator('code', {
            initialValue: item.purchasePrice,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.purchasePrice.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.salePrice')}>
          {form.getFieldDecorator('code', {
            initialValue: item.salePrice,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.salePrice.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.description')}>
          {form.getFieldDecorator('code', {
            initialValue: item.description,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.description.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.statusId')}>
          {form.getFieldDecorator('code', {
            initialValue: item.statusId,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.statusId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.unitOfMeasureId')}>
          {form.getFieldDecorator('code', {
            initialValue: item.unitOfMeasureId,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.unitOfMeasureId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.supplierId')}>
          {form.getFieldDecorator('code', {
            initialValue: item.supplierId,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.supplierId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.status')}>
            {
                form.getFieldDecorator(
                    'statusId',
                    {
                        initialValue: item.status
                            ? item.status.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListItemStatus}
                                  search={itemStatusSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemStatusSearch}>
                      {item.status && (
                        <Option value={item.status.id}>
                          {item.status.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.supplier')}>
            {
                form.getFieldDecorator(
                    'supplierId',
                    {
                        initialValue: item.supplier
                            ? item.supplier.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListSupplier}
                                  search={supplierSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setSupplierSearch}>
                      {item.supplier && (
                        <Option value={item.supplier.id}>
                          {item.supplier.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.type')}>
            {
                form.getFieldDecorator(
                    'typeId',
                    {
                        initialValue: item.type
                            ? item.type.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListItemType}
                                  search={itemTypeSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemTypeSearch}>
                      {item.type && (
                        <Option value={item.type.id}>
                          {item.type.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.unitOfMeasure')}>
            {
                form.getFieldDecorator(
                    'unitOfMeasureId',
                    {
                        initialValue: item.unitOfMeasure
                            ? item.unitOfMeasure.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListItemUnitOfMeasure}
                                  search={itemUnitOfMeasureSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemUnitOfMeasureSearch}>
                      {item.unitOfMeasure && (
                        <Option value={item.unitOfMeasure.id}>
                          {item.unitOfMeasure.name}
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

export default Form.create()(withRouter(ItemDetail));
