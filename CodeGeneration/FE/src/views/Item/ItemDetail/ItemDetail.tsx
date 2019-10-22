
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

import {ITEM_ROUTE} from 'config/route-consts';
import {Item} from 'models/Item';
import {ItemSearch} from 'models/ItemSearch';
import './ItemDetail.scss';
import itemDetailRepository from './ItemDetailRepository';

import {Brand} from 'models/Brand';
import {BrandSearch} from 'models/BrandSearch';
import {Category} from 'models/Category';
import {CategorySearch} from 'models/CategorySearch';
import {Partner} from 'models/Partner';
import {PartnerSearch} from 'models/PartnerSearch';
import {ItemStatus} from 'models/ItemStatus';
import {ItemStatusSearch} from 'models/ItemStatusSearch';
import {ItemType} from 'models/ItemType';
import {ItemTypeSearch} from 'models/ItemTypeSearch';

function ItemDetail(props) {
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
  const [item, loading] = useDetail<Item>(id, itemDetailRepository.get, new Item());
  
  const [brandSearch, setBrandSearch] = useState<BrandSearch>(new BrandSearch());
  const [categorySearch, setCategorySearch] = useState<CategorySearch>(new CategorySearch());
  const [partnerSearch, setPartnerSearch] = useState<PartnerSearch>(new PartnerSearch());
  const [itemStatusSearch, setItemStatusSearch] = useState<ItemStatusSearch>(new ItemStatusSearch());
  const [itemTypeSearch, setItemTypeSearch] = useState<ItemTypeSearch>(new ItemTypeSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, item: Item) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      itemDetailRepository.save(item)
        .subscribe(
          () => {
            notification.success({
              message: translate('itemDetail.update.success'),
            });
            props.history.push(path.join(ITEM_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('itemDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(ITEM_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('itemDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: item.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('itemDetail.code')}>
          {form.getFieldDecorator('code', {
            initialValue: item.code,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.code.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.name')}>
          {form.getFieldDecorator('name', {
            initialValue: item.name,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.sKU')}>
          {form.getFieldDecorator('sKU', {
            initialValue: item.sKU,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.sKU.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('itemDetail.description')}>
          {form.getFieldDecorator('description', {
            initialValue: item.description,
            rules: [
              {
                required: true,
                message: translate('itemDetail.errors.description.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('itemDetail.brand')}>
            {
                form.getFieldDecorator(
                    'brandId', 
                    {
                        initialValue: item.brand 
                            ? item.brand.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListBrand}
                                  search={brandSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setBrandSearch}>
                      {item.brand && (
                        <Option value={item.brand.id}>
                          {item.brand.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.category')}>
            {
                form.getFieldDecorator(
                    'categoryId', 
                    {
                        initialValue: item.category 
                            ? item.category.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListCategory}
                                  search={categorySearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setCategorySearch}>
                      {item.category && (
                        <Option value={item.category.id}>
                          {item.category.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.partner')}>
            {
                form.getFieldDecorator(
                    'partnerId', 
                    {
                        initialValue: item.partner 
                            ? item.partner.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListPartner}
                                  search={partnerSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setPartnerSearch}>
                      {item.partner && (
                        <Option value={item.partner.id}>
                          {item.partner.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.status')}>
            {
                form.getFieldDecorator(
                    'statusId', 
                    {
                        initialValue: item.status 
                            ? item.status.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListItemStatus}
                                  search={itemStatusSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemStatusSearch}>
                      {item.status && (
                        <Option value={item.status.id}>
                          {item.status.id}
                        </Option>
                      )}
                    </SingleSelect>,
                )
            }
        </Form.Item>
        <Form.Item label={translate('itemDetail.type')}>
            {
                form.getFieldDecorator(
                    'typeId', 
                    {
                        initialValue: item.type 
                            ? item.type.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={itemDetailRepository.singleListItemType}
                                  search={itemTypeSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemTypeSearch}>
                      {item.type && (
                        <Option value={item.type.id}>
                          {item.type.id}
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

export default Form.create()(withRouter(ItemDetail));