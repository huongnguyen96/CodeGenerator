import Button from 'antd/lib/button';
import Checkbox from 'antd/lib/checkbox';
import Form from 'antd/lib/form';
import Icon from 'antd/lib/icon';
import Input from 'antd/lib/input';
import React from 'react';
import {useTranslation} from 'react-i18next';
import './Login.scss';

const {Item: FormItem} = Form;

interface IProps {
  form: any;
}

function Login(props: IProps) {
  const {getFieldDecorator} = props.form;

  const [translate] = useTranslation();

  return (
    <Form className="login-form">
      <FormItem>
        {getFieldDecorator('username', {
          rules: [{required: true, message: translate('login.username.errors.required')}],
        })(
          <Input
            prefix={<Icon type="user" style={{color: 'rgba(0,0,0,.25)'}}/>}
            placeholder={translate('login.username.placeholder')}
          />,
        )}
      </FormItem>
      <FormItem>
        {getFieldDecorator('password', {
          rules: [{required: true, message: translate('login.password.errors.required')}],
        })(
          <Input
            prefix={<Icon type="lock" style={{color: 'rgba(0,0,0,.25)'}}/>}
            type="password"
            placeholder={translate('login.password.placeholder')}
          />,
        )}
      </FormItem>
      <FormItem className="row">
        <div className="form-group">
          {getFieldDecorator('remember', {
            valuePropName: 'checked',
            initialValue: true,
          })(<Checkbox>
            {translate('login.remember')}
          </Checkbox>)}
        </div>

        <div className="form-group d-flex justify-content-between">
          <a className="login-form-forgot" href="/">
            {translate('login.forgotPassword')}
          </a>
          <Button type="primary" htmlType="submit" className="login-form-button">
            {translate('login.login')}
          </Button>
        </div>
        <div className="form-group">
          {translate('login.or')} <a href="/">
          {translate('login.registerNow')}
        </a>
        </div>
      </FormItem>
    </Form>
  );
}

export default Form.create({
  name: 'login',
})(Login);
