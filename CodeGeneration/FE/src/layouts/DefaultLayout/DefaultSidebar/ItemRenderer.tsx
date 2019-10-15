import Icon from 'antd/lib/icon';
import React from 'react';
import {NavLink, withRouter} from 'react-router-dom';
import {IDefaultSidebarProps} from './SidebarMenu';

function ItemRenderer(props: IDefaultSidebarProps) {
  const {item} = props;
  const propsForChild = {
    ...props,
  };
  delete propsForChild.staticContext;
  return (
    <NavLink role="menuitem"
             className="ant-menu-item"
             to={item.path}
             exact={item.exact}
             activeClassName="ant-menu-item-selected">
      <Icon type={item.icon}/>
      <span>
        {item.title}
      </span>
    </NavLink>
  );
}

export default withRouter(ItemRenderer);
