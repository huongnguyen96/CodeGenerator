
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

import {PARTNER_ROUTE} from 'config/route-consts';
import {Partner} from 'models/Partner';
import './PartnerDetail.scss';
import partnerDetailRepository from './PartnerDetailRepository';


function PartnerDetail(props) {
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
  const [partner, loading] = useDetail<Partner>(id, partnerDetailRepository.get, new Partner());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, partner: Partner) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      partnerDetailRepository.save(partner)
        .subscribe(
          () => {
            notification.success({
              message: translate('partnerDetail.update.success'),
            });
            props.history.push(path.join(PARTNER_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('partnerDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PARTNER_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('partnerDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: partner.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('partnerDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: partner.name,
            rules: [
              {
                required: true,
                message: translate('partnerDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('partnerDetail.phone')}>
          {form.getFieldDecorator('code', {
            initialValue: partner.phone,
            rules: [
              {
                required: true,
                message: translate('partnerDetail.errors.phone.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('partnerDetail.contactPerson')}>
          {form.getFieldDecorator('code', {
            initialValue: partner.contactPerson,
            rules: [
              {
                required: true,
                message: translate('partnerDetail.errors.contactPerson.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('partnerDetail.address')}>
          {form.getFieldDecorator('code', {
            initialValue: partner.address,
            rules: [
              {
                required: true,
                message: translate('partnerDetail.errors.address.required'),
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

export default Form.create()(withRouter(PartnerDetail));