import Layout from 'antd/lib/layout';
import React from 'react';

const {Header} = Layout;

function DefaultHeader() {
  return (
    <Header className="align-items-center">
      <div className="logo justify-content-end">
          <span className="h3">
            React
          </span>
      </div>
    </Header>
  );
}

export default DefaultHeader;
