
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

import {IMAGE_FILE_ROUTE} from 'config/route-consts';
import {ImageFile} from 'models/ImageFile';
import {ImageFileSearch} from 'models/ImageFileSearch';
import './ImageFileDetail.scss';
import imageFileDetailRepository from './ImageFileDetailRepository';


function ImageFileDetail(props) {
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
  const [imageFile, loading] = useDetail<ImageFile>(id, imageFileDetailRepository.get, new ImageFile());
  

  function handleSubmit() {
    form.validateFields((validationError: Error, imageFile: ImageFile) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      imageFileDetailRepository.save(imageFile)
        .subscribe(
          () => {
            notification.success({
              message: translate('imageFileDetail.update.success'),
            });
            props.history.push(path.join(IMAGE_FILE_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('imageFileDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(IMAGE_FILE_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('imageFileDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: imageFile.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('imageFileDetail.path')}>
          {form.getFieldDecorator('path', {
            initialValue: imageFile.path,
            rules: [
              {
                required: true,
                message: translate('imageFileDetail.errors.path.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('imageFileDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: imageFile.name,
            rules: [
              {
                required: true,
                message: translate('imageFileDetail.errors.name.required'),
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

export default Form.create()(withRouter(ImageFileDetail));