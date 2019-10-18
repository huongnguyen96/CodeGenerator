
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

import {ITEM_UNIT_OF_MEASURE_ROUTE} from 'config/route-consts';
import {ItemUnitOfMeasure} from 'models/ItemUnitOfMeasure';
import './ItemUnitOfMeasureDetail.scss';
import itemUnitOfMeasureDetailRepository from './ItemUnitOfMeasureDetailRepository';


const {Column} = Table;

function ItemUnitOfMeasureDetail(props) {
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
  const [itemUnitOfMeasure, loading] = useDetail<ItemUnitOfMeasure>(id, itemUnitOfMeasureDetailRepository.get, new ItemUnitOfMeasure());
  

  const [pagination] = usePagination();

  function handleSubmit() {
    form.validateFields((validationError: Error, district: District) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemUnitOfMeasureDetailRepository.save(itemUnitOfMeasure)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemUnitOfMeasureDetail.update.success'),
            });
            props.history.push(path.join(ITEM_UNIT_OF_MEASURE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemUnitOfMeasureDetail.update.error'),
              description: error.message,
            });
          },
        )};
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_UNIT_OF_MEASURE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemUnitOfMeasureDetail.detail.title', {
              name: itemUnitOfMeasure.name,
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: itemUnitOfMeasure.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('itemUnitOfMeasureDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: itemUnitOfMeasure.code,
            rules: [
              {
                required: true,
                message: translate('itemUnitOfMeasureDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemUnitOfMeasureDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: itemUnitOfMeasure.name,
            rules: [
              {
                required: true,
                message: translate('itemUnitOfMeasureDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(ItemUnitOfMeasureDetail));