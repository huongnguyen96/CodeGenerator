
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

import {VERSION_ROUTE} from 'config/route-consts';
import {Version} from 'models/Version';
import './VersionDetail.scss';
import versionDetailRepository from './VersionDetailRepository';

import {VersionGroupingSearch} from 'models/VersionGroupingSearch';

function VersionDetail(props) {
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
  const [version, loading] = useDetail<Version>(id, versionDetailRepository.get, new Version());
  
  const [versionGroupingSearch, setVersionGroupingSearch] = useState<VersionGroupingSearch>(new VersionGroupingSearch());

  function handleSubmit() {
    form.validateFields((validationError: Error, version: Version) => {
      if (validationError) {
        return;
      }
      setPageSpinning(true);
      versionDetailRepository.save(version)
        .subscribe(
          () => {
            notification.success({
              message: translate('versionDetail.update.success'),
            });
            props.history.push(path.join(VERSION_ROUTE));
          },
          (error: Error) => {
            setPageSpinning(false);
            notification.error({
              message: translate('versionDetail.update.error'),
              description: error.message,
            });
          },
        );
    });
  }

  function backToList() {
    props.history.push(path.join(VERSION_ROUTE));
  }

  return (
    <Spin spinning={pageSpinning}>
      <Card
        loading={loading}
        title={
          <CardTitle
            title={translate('versionDetail.detail.title', {
            })}
            allowSave
            onSave={handleSubmit}
            allowCancel
            onCancel={backToList}
          />
        }>
        {form.getFieldDecorator('id', {
          initialValue: version.id,
        })(
          <Input type="hidden"/>,
        )}
        
        <Form.Item label={translate('versionDetail.name')}>
          {form.getFieldDecorator('code', {
            initialValue: version.name,
            rules: [
              {
                required: true,
                message: translate('versionDetail.errors.name.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        <Form.Item label={translate('versionDetail.versionGroupingId')}>
          {form.getFieldDecorator('code', {
            initialValue: version.versionGroupingId,
            rules: [
              {
                required: true,
                message: translate('versionDetail.errors.versionGroupingId.required'),
              },
            ],
          })(
            <Input type="text"/>,
          )}
        </Form.Item>

        
        <Form.Item label={translate('versionDetail.versionGrouping')}>
            {
                form.getFieldDecorator(
                    'versionGroupingId', 
                    {
                        initialValue: version.versionGrouping 
                            ? version.versionGrouping.id 
                            : null,
                    }
                )
                (
                    <SingleSelect getList={versionDetailRepository.singleListVersionGrouping}
                                  search={versionGroupingSearch}
                                  searchField="name"
                                  showSearch
                                  setSearch={setVersionGroupingSearch}>
                      {version.versionGrouping && (
                        <Option value={version.versionGrouping.id}>
                          {version.versionGrouping.name}
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

export default Form.create()(withRouter(VersionDetail));