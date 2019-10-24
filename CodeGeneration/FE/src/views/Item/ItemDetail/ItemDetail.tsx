
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
import {ItemSearch} from 'models/ItemSearch';
import './ItemDetail.scss';
import itemDetailRepository from './ItemDetailRepository';

import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';
import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

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

  const [variationSearch, setVariationSearch] = useState<VariationSearch>(new VariationSearch());
  const [productSearch, setProductSearch] = useState<ProductSearch>(new ProductSearch());

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

        <Form.Item label={translate('itemDetail.sKU')}>
          {form.getFieldDecorator('sKU', {
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

        <Form.Item label={translate('itemDetail.price')}>
          {form.getFieldDecorator('price', {
            initialValue: item.price,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.price.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.minPrice')}>
          {form.getFieldDecorator('minPrice', {
            initialValue: item.minPrice,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.minPrice.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.firstVariation')}>
            {
                form.getFieldDecorator(
                    'firstVariationId',
                    {
                        initialValue: item.firstVariation
                            ? item.firstVariation.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListVariation}
                                  search={variationSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVariationSearch}>
                      {item.firstVariation && (
                        <Option value={item.firstVariation.id}>
                          {item.firstVariation.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.product')}>
            {
                form.getFieldDecorator(
                    'productId',
                    {
                        initialValue: item.product
                            ? item.product.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListProduct}
                                  search={productSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProductSearch}>
                      {item.product && (
                        <Option value={item.product.id}>
                          {item.product.id}
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
