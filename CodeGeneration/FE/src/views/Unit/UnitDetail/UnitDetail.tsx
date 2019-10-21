
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
import './UnitDetail.scss';
import unitDetailRepository from './UnitDetailRepository';

import {VariationSearch} from 'models/VariationSearch';
import {VariationSearch} from 'models/VariationSearch';
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
  const [variationSearch, setVariationSearch] = useState<VariationSearch>(new VariationSearch());
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
        
        <Form.Item label={translate('unitDetail.firstVariationId')}>
          {form.getFieldDecorator('code', {
            initialValue: unit.firstVariationId,
            rules: [
              {
                required: true,
                message: translate('unitDetail.errors.firstVariationId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('unitDetail.secondVariationId')}>
          {form.getFieldDecorator('code', {
            initialValue: unit.secondVariationId,
            rules: [
              {
                required: true,
                message: translate('unitDetail.errors.secondVariationId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('unitDetail.thirdVariationId')}>
          {form.getFieldDecorator('code', {
            initialValue: unit.thirdVariationId,
            rules: [
              {
                required: true,
                message: translate('unitDetail.errors.thirdVariationId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('unitDetail.sKU')}>
          {form.getFieldDecorator('code', {
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
          {form.getFieldDecorator('code', {
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
                          {unit.firstVariation.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('unitDetail.secondVariation')}>
            {
                form.getFieldDecorator(
                    'secondVariationId', 
                    {
                        initialValue: unit.secondVariation 
                            ? unit.secondVariation.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={unitDetailRepository.singleListVariation}
                                  search={variationSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVariationSearch}>
                      {unit.secondVariation && (
                        <Option value={unit.secondVariation.id}>
                          {unit.secondVariation.name}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('unitDetail.thirdVariation')}>
            {
                form.getFieldDecorator(
                    'thirdVariationId', 
                    {
                        initialValue: unit.thirdVariation 
                            ? unit.thirdVariation.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={unitDetailRepository.singleListVariation}
                                  search={variationSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVariationSearch}>
                      {unit.thirdVariation && (
                        <Option value={unit.thirdVariation.id}>
                          {unit.thirdVariation.name}
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