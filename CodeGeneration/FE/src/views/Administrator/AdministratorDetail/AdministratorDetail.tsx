
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

import {ADMINISTRATOR_ROUTE} from 'config/route-consts';
import {Administrator} from 'models/Administrator';
import './AdministratorDetail.scss';
import administratorDetailRepository from './AdministratorDetailRepository';


function AdministratorDetail(props) {
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
  const [administrator, loading] = useDetail<Administrator>(id, administratorDetailRepository.get, new Administrator());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, administrator: Administrator) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      administratorDetailRepository.save(administrator)
        .subscribe(
          () => {
            notification.success({
              message: translate('administratorDetail.update.success'),
            });
            props.history.push(path.join(ADMINISTRATOR_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('administratorDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ADMINISTRATOR_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('administratorDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: administrator.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('administratorDetail.username')}>
          {form.getFieldDecorator('code', {
            initialValue: administrator.username,
            rules: [
              {
                required: true,
                message: translate('administratorDetail.errors.username.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('administratorDetail.displayName')}>
          {form.getFieldDecorator('code', {
            initialValue: administrator.displayName,
            rules: [
              {
                required: true,
                message: translate('administratorDetail.errors.displayName.required'),
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

export default Form.create()(withRouter(AdministratorDetail));