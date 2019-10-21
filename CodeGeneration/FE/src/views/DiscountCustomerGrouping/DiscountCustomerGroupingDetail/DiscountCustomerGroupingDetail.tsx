
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

import {DISCOUNT_CUSTOMER_GROUPING_ROUTE} from 'config/route-consts';
import {DiscountCustomerGrouping} from 'models/DiscountCustomerGrouping';
import './DiscountCustomerGroupingDetail.scss';
import discountCustomerGroupingDetailRepository from './DiscountCustomerGroupingDetailRepository';

import {DiscountSearch} from 'models/DiscountSearch';

function DiscountCustomerGroupingDetail(props) {
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
  const [discountCustomerGrouping, loading] = useDetail<DiscountCustomerGrouping>(id, discountCustomerGroupingDetailRepository.get, new DiscountCustomerGrouping());
  
  const [discountSearch, setDiscountSearch] = useState<DiscountSearch>(new DiscountSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, discountCustomerGrouping: DiscountCustomerGrouping) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      discountCustomerGroupingDetailRepository.save(discountCustomerGrouping)
        .subscribe(
          () => {
            notification.success({
              message: translate('discountCustomerGroupingDetail.update.success'),
            });
            props.history.push(path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('discountCustomerGroupingDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(DISCOUNT_CUSTOMER_GROUPING_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('discountCustomerGroupingDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: discountCustomerGrouping.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('discountCustomerGroupingDetail.discountId')}>
          {form.getFieldDecorator('code', {
            initialValue: discountCustomerGrouping.discountId,
            rules: [
              {
                required: true,
                message: translate('discountCustomerGroupingDetail.errors.discountId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('discountCustomerGroupingDetail.customerGroupingCode')}>
          {form.getFieldDecorator('code', {
            initialValue: discountCustomerGrouping.customerGroupingCode,
            rules: [
              {
                required: true,
                message: translate('discountCustomerGroupingDetail.errors.customerGroupingCode.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('discountCustomerGroupingDetail.discount')}>
            {
                form.getFieldDecorator(
                    'discountId', 
                    {
                        initialValue: discountCustomerGrouping.discount 
                            ? discountCustomerGrouping.discount.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={discountCustomerGroupingDetailRepository.singleListDiscount}
                                  search={discountSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setDiscountSearch}>
                      {discountCustomerGrouping.discount && (
                        <Option value={discountCustomerGrouping.discount.id}>
                          {discountCustomerGrouping.discount.name}
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

export default Form.create()(withRouter(DiscountCustomerGroupingDetail));