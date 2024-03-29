
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

import {VARIATION_GROUPING_ROUTE} from 'config/route-consts';
import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';
import './VariationGroupingDetail.scss';
import variationGroupingDetailRepository from './VariationGroupingDetailRepository';

import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';

function VariationGroupingDetail(props) {
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
  const [variationGrouping, loading] = useDetail<VariationGrouping>(id, variationGroupingDetailRepository.get, new VariationGrouping());

  const [productSearch, setProductSearch] = useState<ProductSearch>(new ProductSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, variationGrouping: VariationGrouping) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      variationGroupingDetailRepository.save(variationGrouping)
        .subscribe(
          () => {
            notification.success({
              message: translate('variationGroupingDetail.update.success'),
            });
            props.history.push(path.join(VARIATION_GROUPING_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('variationGroupingDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(VARIATION_GROUPING_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('variationGroupingDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: variationGrouping.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('variationGroupingDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: variationGrouping.name,
            rules: [
              {
                required: true,
                message: translate('variationGroupingDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('variationGroupingDetail.product')}>
            {
                form.getFieldDecorator(
                    'productId',
                    {
                        initialValue: variationGrouping.product
                            ? variationGrouping.product.id
                            : null,
                    },
                )
                (
                    <SingleSelect getList={variationGroupingDetailRepository.singleListProduct}
                                  search={productSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProductSearch}>
                      {variationGrouping.product && (
                        <Option value={variationGrouping.product.id}>
                          {variationGrouping.product.id}
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

export default Form.create()(withRouter(VariationGroupingDetail));
