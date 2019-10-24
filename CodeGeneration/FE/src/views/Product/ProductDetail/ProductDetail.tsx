
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

import {PRODUCT_ROUTE} from 'config/route-consts';
import {Product} from 'models/Product';
import {ProductSearch} from 'models/ProductSearch';
import './ProductDetail.scss';
import productDetailRepository from './ProductDetailRepository';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Merchant} from 'models/Merchant';
import {MerchantSearch} from 'models/MerchantSearch';
import {ProductStatus} from 'models/ProductStatus';
import {ProductStatusSearch} from 'models/ProductStatusSearch';
import {ProductType} from 'models/ProductType';
import {ProductTypeSearch} from 'models/ProductTypeSearch';

function ProductDetail(props) {
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
  const [product, loading] = useDetail<Product>(id, productDetailRepository.get, new Product());
  
  const [brandSearch, setBrandSearch] = useState<BrandSearch>(new BrandSearch());
  const [categorySearch, setCategorySearch] = useState<CategorySearch>(new CategorySearch());
  const [merchantSearch, setMerchantSearch] = useState<MerchantSearch>(new MerchantSearch());
  const [productStatusSearch, setProductStatusSearch] = useState<ProductStatusSearch>(new ProductStatusSearch());
  const [productTypeSearch, setProductTypeSearch] = useState<ProductTypeSearch>(new ProductTypeSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, product: Product) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      productDetailRepository.save(product)
        .subscribe(
          () => {
            notification.success({
              message: translate('productDetail.update.success'),
            });
            props.history.push(path.join(PRODUCT_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('productDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(PRODUCT_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('productDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: product.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('productDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: product.code,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: product.name,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.description')}>
          {form.getFieldDecorator('description', {
            initialValue: product.description,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.description.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.warrantyPolicy')}>
          {form.getFieldDecorator('warrantyPolicy', {
            initialValue: product.warrantyPolicy,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.warrantyPolicy.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.returnPolicy')}>
          {form.getFieldDecorator('returnPolicy', {
            initialValue: product.returnPolicy,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.returnPolicy.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.expiredDate')}>
          {form.getFieldDecorator('expiredDate', {
            initialValue: product.expiredDate,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.expiredDate.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.conditionOfUse')}>
          {form.getFieldDecorator('conditionOfUse', {
            initialValue: product.conditionOfUse,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.conditionOfUse.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('productDetail.maximumPurchaseQuantity')}>
          {form.getFieldDecorator('maximumPurchaseQuantity', {
            initialValue: product.maximumPurchaseQuantity,
            rules: [
              {
                required: true,
                message: translate('productDetail.errors.maximumPurchaseQuantity.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('productDetail.brand')}>
            {
                form.getFieldDecorator(
                    'brandId', 
                    {
                        initialValue: product.brand 
                            ? product.brand.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={productDetailRepository.singleListBrand}
                                  search={brandSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setBrandSearch}>
                      {product.brand && (
                        <Option value={product.brand.id}>
                          {product.brand.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('productDetail.category')}>
            {
                form.getFieldDecorator(
                    'categoryId', 
                    {
                        initialValue: product.category 
                            ? product.category.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={productDetailRepository.singleListCategory}
                                  search={categorySearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCategorySearch}>
                      {product.category && (
                        <Option value={product.category.id}>
                          {product.category.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('productDetail.merchant')}>
            {
                form.getFieldDecorator(
                    'merchantId', 
                    {
                        initialValue: product.merchant 
                            ? product.merchant.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={productDetailRepository.singleListMerchant}
                                  search={merchantSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setMerchantSearch}>
                      {product.merchant && (
                        <Option value={product.merchant.id}>
                          {product.merchant.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('productDetail.status')}>
            {
                form.getFieldDecorator(
                    'statusId', 
                    {
                        initialValue: product.status 
                            ? product.status.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={productDetailRepository.singleListProductStatus}
                                  search={productStatusSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProductStatusSearch}>
                      {product.status && (
                        <Option value={product.status.id}>
                          {product.status.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('productDetail.type')}>
            {
                form.getFieldDecorator(
                    'typeId', 
                    {
                        initialValue: product.type 
                            ? product.type.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={productDetailRepository.singleListProductType}
                                  search={productTypeSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setProductTypeSearch}>
                      {product.type && (
                        <Option value={product.type.id}>
                          {product.type.id}
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

export default Form.create()(withRouter(ProductDetail));