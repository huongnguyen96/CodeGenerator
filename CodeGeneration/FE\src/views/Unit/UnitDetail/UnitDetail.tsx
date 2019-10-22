
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

import {UNIT_ROUTE} from 'config/route-consts';
import {Unit} from 'models/Unit';
import {UnitSearch} from 'models/UnitSearch';
import './UnitDetail.scss';
import unitDetailRepository from './UnitDetailRepository';

import {Variation} from 'models/Variation';
import {VariationSearch} from 'models/VariationSearch';

function UnitDetail(props) {
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
  const [unit, loading] = useDetail<Unit>(id, unitDetailRepository.get, new Unit());
  
  const [variationSearch, setVariationSearch] = useState<VariationSearch>(new VariationSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, unit: Unit) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      unitDetailRepository.save(unit)
        .subscribe(
          () => {
            notification.success({
              message: translate('unitDetail.update.success'),
            });
            props.history.push(path.join(UNIT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('unitDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(UNIT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('unitDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: unit.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('unitDetail.sKU')}>
          {form.getFieldDecorator('sKU', {
            initialValue: unit.sKU,
            rules: [
              {
                required: true,
                message: translate('unitDetail.errors.sKU.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('unitDetail.price')}>
          {form.getFieldDecorator('price', {
            initialValue: unit.price,
            rules: [
              {
                required: true,
                message: translate('unitDetail.errors.price.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('unitDetail.firstVariation')}>
            {
                form.getFieldDecorator(
                    'firstVariationId', 
                    {
                        initialValue: unit.firstVariation 
                            ? unit.firstVariation.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={unitDetailRepository.singleListVariation}
                                  search={variationSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVariationSearch}>
                      {unit.firstVariation && (
                        <Option value={unit.firstVariation.id}>
                          {unit.firstVariation.id}
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

export default Form.create()(withRouter(UnitDetail));