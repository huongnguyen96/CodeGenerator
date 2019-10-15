import Layout from 'antd/lib/layout';
import {routes} from 'config/routes';
import RouterRenderer from 'core/components/RouterRender';
import React from 'react';
import DefaultFooter from './DefaultFooter';
import DefaultHeader from './DefaultHeader';
import './DefaultLayout.scss';
import DefaultSidebar from './DefaultSidebar';

const {
  Content,
} = Layout;

function DefaultLayout() {
  return (
    <Layout>
      <DefaultHeader/>
      <Layout>
        <DefaultSidebar/>
        <Layout>
          <Content>
            <RouterRenderer routes={routes}/>
          </Content>
          <DefaultFooter/>
        </Layout>
      </Layout>
    </Layout>
  );
}

export default DefaultLayout;
