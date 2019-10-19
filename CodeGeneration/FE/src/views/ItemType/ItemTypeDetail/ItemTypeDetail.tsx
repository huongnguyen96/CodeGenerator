
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

import {ITEM_TYPE_ROUTE} from 'config/route-consts';
import {ItemType} from 'models/ItemType';
import './ItemTypeDetail.scss';
import itemTypeDetailRepository from './ItemTypeDetailRepository';

function ItemTypeDetail(props) {
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
  const [itemType, loading] = useDetail<ItemType>(id, itemTypeDetailRepository.get, new ItemType());

  function handleSubmit() {
    form.validateFields((validationError: Error, itemType: ItemType) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemTypeDetailRepository.save(itemType)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemTypeDetail.update.success'),
            });
            props.history.push(path.join(ITEM_TYPE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemTypeDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_TYPE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemTypeDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: itemType.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('itemTypeDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: itemType.code,
            rules: [
              {
                required: true,
                message: translate('itemTypeDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemTypeDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: itemType.name,
            rules: [
              {
                required: true,
                message: translate('itemTypeDetail.errors.name.required'),
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

export default Form.create()(withRouter(ItemTypeDetail));
