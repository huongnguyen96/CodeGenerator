
import Button from 'antd/lib/button';
import Card from 'antd/lib/card';
import Table from 'antd/lib/table';
import CardTitle from 'components/CardTitle/CardTitle';
import {useList} from 'core/hooks/useList';
import {confirm, getColumnSortOrder, notification, renderIndex } from 'helpers';
import path from 'path';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import { Link,RouteComponentProps, withRouter } from 'react-router-dom';

import './ImageFileMaster.scss';
import imageFileMasterRepository from './ImageFileMasterRepository';
import { IMAGE_FILE_ROUTE } from 'config/route-consts';
import { ImageFile } from 'models/ImageFile';
import { ImageFileSearch } from 'models/ImageFileSearch';


const {Column} = Table;

function ImageFileMaster(props: RouteComponentProps) {
  function handleAdd() {
    props.history.push(path.join(IMAGE_FILE_ROUTE, 'add'));
  }

  function handleClear() {
    clearFiltersAndSorters();
    setSearch(new ImageFileSearch());
  }

  function reloadList() {
    setSearch(new ImageFileSearch(search));
  }

  function handleDelete(imageFile: ImageFile) {
    return () => {
      confirm({
        title: translate('imageFileMaster.deletion.title'),
        content: translate('imageFileMaster.deletion.content'),
        okType: 'danger',
        onOk: () => {
          imageFileMasterRepository.delete(imageFile)
            .subscribe(
              () => {
                notification.success({
                  message: translate('imageFileMaster.deletion.success'),
                });
                reloadList();
              },
              (error: Error) => {
                notification.error({
                  message: translate('imageFileMaster.deletion.error'),
                  description: error.message,
                });
              },
            );
        },
      });
    };
  }

  const [translate] = useTranslation();
  const [search, setSearch] = useState<ImageFileSearch>(new ImageFileSearch());

  const [
    list,
    total,
    loading,
    sorter,
    handleChange,
    clearFiltersAndSorters,
  ] = useList<ImageFile, ImageFileSearch>(
    search,
    setSearch,
    imageFileMasterRepository.list,
    imageFileMasterRepository.count,
  );

  return (
    <Card title={
      <CardTitle title={translate('imageFileMaster.title')}
                 allowAdd
                 onAdd={handleAdd}
                 allowClear
                 onClear={handleClear}
      />
    }>
      <Table dataSource={list}
             rowKey="id"
             loading={loading}
             onChange={handleChange}
             pagination={{
               total,
             }}
      >
        <Column key="index"
                title={translate('imageFileMaster.index')}
                render={renderIndex<ImageFile, ImageFileSearch>(search)}
        />
        
         <Column key="id"
                dataIndex="id"
                title={translate('imageFileMaster.id')}
                sorter
                sortOrder={getColumnSortOrder<ImageFile>('id', sorter)}
        />
         <Column key="path"
                dataIndex="path"
                title={translate('imageFileMaster.path')}
                sorter
                sortOrder={getColumnSortOrder<ImageFile>('path', sorter)}
        />
         <Column key="name"
                dataIndex="name"
                title={translate('imageFileMaster.name')}
                sorter
                sortOrder={getColumnSortOrder<ImageFile>('name', sorter)}
        />
        <Column key="actions"
                dataIndex="id"
                render={(id: string, imageFile: ImageFile) => {
                  return (
                    <>
                      <Link to={path.join(IMAGE_FILE_ROUTE, id)}>
                        {translate('general.actions.edit')}
                      </Link>
                      <Button htmlType="button" type="link" onClick={handleDelete(imageFile)}>
                        {translate('general.actions.delete')}
                      </Button>
                    </>
                  );
                }}
        />
      </Table>
    </Card>
  );
}

export default withRouter(ImageFileMaster);
