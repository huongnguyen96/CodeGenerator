
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

import {VARIATION_ROUTE} from 'config/route-consts';
import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';
import './VariationDetail.scss';
import variationDetailRepository from './VariationDetailRepository';

import {VariationGrouping} from 'models/VariationGrouping';
import {VariationGroupingSearch} from 'models/VariationGroupingSearch';

function VariationDetail(props) {
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
  const [variation, loading] = useDetail<Variation>(id, variationDetailRepository.get, new Variation());
  
  const [variationGroupingSearch, setVariationGroupingSearch] = useState<VariationGroupingSearch>(new VariationGroupingSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, variation: Variation) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      variationDetailRepository.save(variation)
        .subscribe(
          () => {
            notification.success({
              message: translate('variationDetail.update.success'),
            });
            props.history.push(path.join(VARIATION_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('variationDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(VARIATION_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('variationDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: variation.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('variationDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: variation.name,
            rules: [
              {
                required: true,
                message: translate('variationDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('variationDetail.variationGrouping')}>
            {
                form.getFieldDecorator(
                    'variationGroupingId', 
                    {
                        initialValue: variation.variationGrouping 
                            ? variation.variationGrouping.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={variationDetailRepository.singleListVariationGrouping}
                                  search={variationGroupingSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVariationGroupingSearch}>
                      {variation.variationGrouping && (
                        <Option value={variation.variationGrouping.id}>
                          {variation.variationGrouping.id}
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

export default Form.create()(withRouter(VariationDetail));