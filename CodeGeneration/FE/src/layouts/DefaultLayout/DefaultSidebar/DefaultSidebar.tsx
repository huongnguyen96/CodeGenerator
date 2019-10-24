import Layout from 'antd/lib/layout';
import Menu from 'antd/lib/menu';
import {menu} from 'config/menu';
import {IRoute} from 'core/IRoute';
import React, {useEffect, useState} from 'react';
import {RouteComponentProps, withRouter} from 'react-router-dom';
import './DefaultSidebar.scss';
import SidebarMenu from './SidebarMenu';

const {Sider} = Layout;

interface IDefaultSidebarProps extends RouteComponentProps {
  routes?: IRoute[];
}

function DefaultSidebar(props: IDefaultSidebarProps) {
  const {routes} = props;

  const [collapsed, setCollapsed] = useState(false);

  function toggleSider(event: boolean) {
    setCollapsed(event);
  }

  function toggleSiderOnWindowResize() {
    setCollapsed(window.innerWidth <= 680);
  }

  useEffect(
    () => {
      window.addEventListener('resize', toggleSiderOnWindowResize);

      return () => {
        window.removeEventListener('resize', toggleSiderOnWindowResize);
      };
    },
    [],
  );

  return (
    <Sider collapsible={true} collapsed={collapsed} onCollapse={toggleSider}>
      <Menu theme="dark"
            defaultSelectedKeys={['1']}
            mode="inline"
            className="default-sidebar">
        {routes.map((route: IRoute) => (
          <SidebarMenu {...props} key={route.path} item={route}/>
        ))}
      </Menu>
    </Sider>
  );
}

DefaultSidebar.defaultProps = {
  routes: menu,
};

export default withRouter(DefaultSidebar);
