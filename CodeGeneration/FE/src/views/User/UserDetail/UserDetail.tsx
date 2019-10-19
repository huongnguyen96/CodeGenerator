
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

import {USER_ROUTE} from 'config/route-consts';
import {User} from 'models/User';
import './UserDetail.scss';
import userDetailRepository from './UserDetailRepository';

function UserDetail(props) {
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
  const [user, loading] = useDetail<User>(id, userDetailRepository.get, new User());

  function handleSubmit() {
    form.validateFields((validationError: Error, user: User) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      userDetailRepository.save(user)
        .subscribe(
          () => {
            notification.success({
              message: translate('userDetail.update.success'),
            });
            props.history.push(path.join(USER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('userDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(USER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('userDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: user.id,
        })(
          <Input type="hidden"/>,
        )}

        <Form.Item label={translate('userDetail.username')}>
          {form.getFieldDecorator('code', {
            initialValue: user.username,
            rules: [
              {
                required: true,
                message: translate('userDetail.errors.username.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('userDetail.password')}>
          {form.getFieldDecorator('code', {
            initialValue: user.password,
            rules: [
              {
                required: true,
                message: translate('userDetail.errors.password.required'),
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

export default Form.create()(withRouter(UserDetail));
