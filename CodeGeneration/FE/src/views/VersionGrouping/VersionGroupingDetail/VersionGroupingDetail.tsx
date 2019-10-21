
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

import {VERSION_GROUPING_ROUTE} from 'config/route-consts';
import {VersionGrouping} from 'models/VersionGrouping';
import './VersionGroupingDetail.scss';
import versionGroupingDetailRepository from './VersionGroupingDetailRepository';

import {ItemSearch} from 'models/ItemSearch';

function VersionGroupingDetail(props) {
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
  const [versionGrouping, loading] = useDetail<VersionGrouping>(id, versionGroupingDetailRepository.get, new VersionGrouping());
  
  const [itemSearch, setItemSearch] = useState<ItemSearch>(new ItemSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, versionGrouping: VersionGrouping) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      versionGroupingDetailRepository.save(versionGrouping)
        .subscribe(
          () => {
            notification.success({
              message: translate('versionGroupingDetail.update.success'),
            });
            props.history.push(path.join(VERSION_GROUPING_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('versionGroupingDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(VERSION_GROUPING_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('versionGroupingDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: versionGrouping.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('versionGroupingDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: versionGrouping.name,
            rules: [
              {
                required: true,
                message: translate('versionGroupingDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('versionGroupingDetail.itemId')}>
          {form.getFieldDecorator('code', {
            initialValue: versionGrouping.itemId,
            rules: [
              {
                required: true,
                message: translate('versionGroupingDetail.errors.itemId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('versionGroupingDetail.item')}>
            {
                form.getFieldDecorator(
                    'itemId', 
                    {
                        initialValue: versionGrouping.item 
                            ? versionGrouping.item.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={versionGroupingDetailRepository.singleListItem}
                                  search={itemSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setItemSearch}>
                      {versionGrouping.item && (
                        <Option value={versionGrouping.item.id}>
                          {versionGrouping.item.name}
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

export default Form.create()(withRouter(VersionGroupingDetail));