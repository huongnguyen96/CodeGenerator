
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

import {BRAND_ROUTE} from 'config/route-consts';
import {Brand} from 'models/Brand';
import './BrandDetail.scss';
import brandDetailRepository from './BrandDetailRepository';

import {CategorySearch} from 'models/CategorySearch';

function BrandDetail(props) {
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
  const [brand, loading] = useDetail<Brand>(id, brandDetailRepository.get, new Brand());
  
  const [categorySearch, setCategorySearch] = useState<CategorySearch>(new CategorySearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, brand: Brand) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      brandDetailRepository.save(brand)
        .subscribe(
          () => {
            notification.success({
              message: translate('brandDetail.update.success'),
            });
            props.history.push(path.join(BRAND_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('brandDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(BRAND_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('brandDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: brand.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('brandDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: brand.name,
            rules: [
              {
                required: true,
                message: translate('brandDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('brandDetail.categoryId')}>
          {form.getFieldDecorator('code', {
            initialValue: brand.categoryId,
            rules: [
              {
                required: true,
                message: translate('brandDetail.errors.categoryId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('brandDetail.category')}>
            {
                form.getFieldDecorator(
                    'categoryId', 
                    {
                        initialValue: brand.category 
                            ? brand.category.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={brandDetailRepository.singleListCategory}
                                  search={categorySearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCategorySearch}>
                      {brand.category && (
                        <Option value={brand.category.id}>
                          {brand.category.name}
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

export default Form.create()(withRouter(BrandDetail));