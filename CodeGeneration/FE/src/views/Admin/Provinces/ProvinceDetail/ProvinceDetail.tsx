import {Input} from 'antd';
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Form from 'antd/lib/form';
import Spin from 'antd/lib/spin';
import Table from 'antd/lib/table';
import {Pagination} from 'core/entities/Pagination';
import {useDetail} from 'core/hooks/useDetail';
import {useLocalPagination} from 'core/hooks/useLocalPagination';
import {notification} from 'helpers';
import {Province} from 'models/Province';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {withRouter} from 'react-router-dom';
import {finalize} from 'rxjs/operators';
import './ProvinceDetail.scss';
import provinceDetailRepository from './ProvinceDetailRepository';

const {Item} = Form;
const {Column} = Table;

function ProvinceDetail(props) {
  const {
    form,
    match: {
      params: {
        id,
      },
    },
  } = props;

  const [translate] = useTranslation();

  const [province, loading] = useDetail<Province>(id, provinceDetailRepository.get);
  const [pageSpinning, setPageSpinning] = useState<boolean>(false);
  const pagination: Pagination = useLocalPagination();

  function handleSubmit() {
    form.validateFields((validationError: Error, validatedProvince: Province) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      provinceDetailRepository.save(validatedProvince)
        .pipe(
          finalize(() => {
            setPageSpinning(false);
          }),
        )
        .subscribe(
          () => {
            notification.success({
              message: translate('provinces.update.success'),
            });
            props.history.push('/admin/provinces');
          },
          (error: Error) => {
            notification.error({
              message: translate('provinces.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card title={(
        <div className="d-flex">
          {translate('provinces.detail.title')}
          <div className="flex-grow-1 d-flex justify-content-end">
            <Button type="primary" loading={pageSpinning} onClick={handleSubmit}>
              {translate('buttons.save')}
            </Button>
          </div>
        </div>
      )}
            loading={loading}>
        {form.getFieldDecorator('id', {
          initialValue: province ? province.id : null,
        })(
          <Input type="hidden"/>,
        )}
        <Item label={translate('provinces.code')}>
          {form.getFieldDecorator('code', {
            initialValue: province ? province.code : null,
            rules: [
              {
                required: true,
                message: translate('provinces.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Item>
        <Item label={translate('provinces.name')}>
          {form.getFieldDecorator('name', {
            initialValue: province ? province.name : null,
            rules: [
              {
                required: true,
                message: translate('provinces.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Item>
      </Card>

      <Card title={translate('provinces.detail.districts.title')}>
        <Table dataSource={province ? province.districts : []}
               rowKey="id"
               pagination={pagination}
               loading={loading}>
          <Column key="code" dataIndex="code" title={translate('provinces.detail.districts.code')}/>
          <Column key="name" dataIndex="name" title={translate('provinces.detail.districts.name')}/>
        </Table>
      </Card>
    </Spin>
  );
}

export default Form.create()(withRouter(ProvinceDetail));
