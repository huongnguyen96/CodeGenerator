
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

import {DISCOUNT_CONTENT_ROUTE} from 'config/route-consts';
import {DiscountContent} from 'models/DiscountContent';
import {DiscountContentSearch} from 'models/DiscountContentSearch';
import './DiscountContentDetail.scss';
import discountContentDetailRepository from './DiscountContentDetailRepository';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';

function DiscountContentDetail(props) {
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
  const [discountContent, loading] = useDetail<DiscountContent>(id, discountContentDetailRepository.get, new DiscountContent());

  const [discountSearch, setDiscountSearch] = useState<DiscountSearch>(new DiscountSearch());
  const [itemSearch, setItemSearch] = useState<ItemSearch>(new ItemSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, discountContent: DiscountContent) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      discountContentDetailRepository.save(discountContent)
        .subscribe(
          () => {
            notification.success({
              message: translate('discountContentDetail.update.success'),
            });
            props.history.push(path.join(DISCOUNT_CONTENT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('discountContentDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(DISCOUNT_CONTENT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('discountContentDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: discountContent.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('discountContentDetail.discountValue')}>
          {form.getFieldDecorator('discountValue', {
            initialValue: discountContent.discountValue,
            rules: [
              {
                required: true,
                message: translate('discountContentDetail.errors.discountValue.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('discountContentDetail.discount')}>
            {
                form.getFieldDecorator(
                    'discountId',
                    {
                        initialValue: discountContent.discount
                            ? discountContent.discount.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={discountContentDetailRepository.singleListDiscount}
                                  search={discountSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setDiscountSearch}>
                      {discountContent.discount && (
                        <Option value={discountContent.discount.id}>
                          {discountContent.discount.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('discountContentDetail.item')}>
            {
                form.getFieldDecorator(
                    'itemId',
                    {
                        initialValue: discountContent.item
                            ? discountContent.item.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={discountContentDetailRepository.singleListItem}
                                  search={itemSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemSearch}>
                      {discountContent.item && (
                        <Option value={discountContent.item.id}>
                          {discountContent.item.id}
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

export default Form.create()(withRouter(DiscountContentDetail));
