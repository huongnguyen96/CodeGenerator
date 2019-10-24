
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

import {DISCOUNT_ITEM_ROUTE} from 'config/route-consts';
import {DiscountItem} from 'models/DiscountItem';
import {DiscountItemSearch} from 'models/DiscountItemSearch';
import './DiscountItemDetail.scss';
import discountItemDetailRepository from './DiscountItemDetailRepository';

import {Discount} from 'models/Discount';
import {DiscountSearch} from 'models/DiscountSearch';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';

function DiscountItemDetail(props) {
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
  const [discountItem, loading] = useDetail<DiscountItem>(id, discountItemDetailRepository.get, new DiscountItem());
  
  const [discountSearch, setDiscountSearch] = useState<DiscountSearch>(new DiscountSearch());
  const [unitSearch, setUnitSearch] = useState<UnitSearch>(new UnitSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, discountItem: DiscountItem) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      discountItemDetailRepository.save(discountItem)
        .subscribe(
          () => {
            notification.success({
              message: translate('discountItemDetail.update.success'),
            });
            props.history.push(path.join(DISCOUNT_ITEM_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('discountItemDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(DISCOUNT_ITEM_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('discountItemDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: discountItem.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('discountItemDetail.discountValue')}>
          {form.getFieldDecorator('discountValue', {
            initialValue: discountItem.discountValue,
            rules: [
              {
                required: true,
                message: translate('discountItemDetail.errors.discountValue.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('discountItemDetail.discount')}>
            {
                form.getFieldDecorator(
                    'discountId', 
                    {
                        initialValue: discountItem.discount 
                            ? discountItem.discount.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={discountItemDetailRepository.singleListDiscount}
                                  search={discountSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setDiscountSearch}>
                      {discountItem.discount && (
                        <Option value={discountItem.discount.id}>
                          {discountItem.discount.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('discountItemDetail.unit')}>
            {
                form.getFieldDecorator(
                    'unitId', 
                    {
                        initialValue: discountItem.unit 
                            ? discountItem.unit.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={discountItemDetailRepository.singleListUnit}
                                  search={unitSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setUnitSearch}>
                      {discountItem.unit && (
                        <Option value={discountItem.unit.id}>
                          {discountItem.unit.id}
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

export default Form.create()(withRouter(DiscountItemDetail));