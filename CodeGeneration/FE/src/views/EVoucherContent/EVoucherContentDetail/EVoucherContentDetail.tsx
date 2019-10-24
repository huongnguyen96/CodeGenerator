
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

import {E_VOUCHER_CONTENT_ROUTE} from 'config/route-consts';
import {EVoucherContent} from 'models/EVoucherContent';
import {EVoucherContentSearch} from 'models/EVoucherContentSearch';
import './EVoucherContentDetail.scss';
import eVoucherContentDetailRepository from './EVoucherContentDetailRepository';

import {EVoucher} from 'models/EVoucher';
import {EVoucherSearch} from 'models/EVoucherSearch';

function EVoucherContentDetail(props) {
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
  const [eVoucherContent, loading] = useDetail<EVoucherContent>(id, eVoucherContentDetailRepository.get, new EVoucherContent());
  
  const [eVoucherSearch, setEVoucherSearch] = useState<EVoucherSearch>(new EVoucherSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, eVoucherContent: EVoucherContent) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      eVoucherContentDetailRepository.save(eVoucherContent)
        .subscribe(
          () => {
            notification.success({
              message: translate('eVoucherContentDetail.update.success'),
            });
            props.history.push(path.join(E_VOUCHER_CONTENT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('eVoucherContentDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(E_VOUCHER_CONTENT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('eVoucherContentDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: eVoucherContent.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('eVoucherContentDetail.usedCode')}>
          {form.getFieldDecorator('usedCode', {
            initialValue: eVoucherContent.usedCode,
            rules: [
              {
                required: true,
                message: translate('eVoucherContentDetail.errors.usedCode.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('eVoucherContentDetail.merchantCode')}>
          {form.getFieldDecorator('merchantCode', {
            initialValue: eVoucherContent.merchantCode,
            rules: [
              {
                required: true,
                message: translate('eVoucherContentDetail.errors.merchantCode.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('eVoucherContentDetail.usedDate')}>
          {
            form.getFieldDecorator(
                'usedDate', 
                {
                    initialValue: eVoucherContent.usedDate,
                    rules: [
                    ],
                }
            )
            (<DatePicker/>)
          }
        </Form.Item>

        
        <Form.Item label={translate('eVoucherContentDetail.eVourcher')}>
            {
                form.getFieldDecorator(
                    'eVourcherId', 
                    {
                        initialValue: eVoucherContent.eVourcher 
                            ? eVoucherContent.eVourcher.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={eVoucherContentDetailRepository.singleListEVoucher}
                                  search={eVoucherSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setEVoucherSearch}>
                      {eVoucherContent.eVourcher && (
                        <Option value={eVoucherContent.eVourcher.id}>
                          {eVoucherContent.eVourcher.id}
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

export default Form.create()(withRouter(EVoucherContentDetail));