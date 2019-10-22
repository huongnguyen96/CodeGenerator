
import {IMAGE_FILE_ROUTE} from 'config/route-consts';
import path from 'path';
import React from 'react';
import {Route,Switch, withRouter} from 'react-router-dom';
import ImageFileDetail from './ImageFileDetail';
import ImageFileMaster from './ImageFileMaster';
import './ImageFileView.scss';

function ImageFileView() {
  return (
    <Switch>
      <Route path={path.join(IMAGE_FILE_ROUTE)} exact component={ImageFileMaster}/>
      <Route path={path.join(IMAGE_FILE_ROUTE, ':id')} component={ImageFileDetail}/>
    </Switch>
  );
}

export default withRouter(ImageFileView);
