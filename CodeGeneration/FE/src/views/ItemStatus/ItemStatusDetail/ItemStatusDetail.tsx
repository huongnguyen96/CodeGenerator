
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

import {ITEM_STATUS_ROUTE} from 'config/route-consts';
import {ItemStatus} from 'models/ItemStatus';
import './ItemStatusDetail.scss';
import itemStatusDetailRepository from './ItemStatusDetailRepository';


function ItemStatusDetail(props) {
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
  const [itemStatus, loading] = useDetail<ItemStatus>(id, itemStatusDetailRepository.get, new ItemStatus());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, itemStatus: ItemStatus) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemStatusDetailRepository.save(itemStatus)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemStatusDetail.update.success'),
            });
            props.history.push(path.join(ITEM_STATUS_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemStatusDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_STATUS_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemStatusDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: itemStatus.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('itemStatusDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStatus.code,
            rules: [
              {
                required: true,
                message: translate('itemStatusDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemStatusDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: itemStatus.name,
            rules: [
              {
                required: true,
                message: translate('itemStatusDetail.errors.name.required'),
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

export default Form.create()(withRouter(ItemStatusDetail));