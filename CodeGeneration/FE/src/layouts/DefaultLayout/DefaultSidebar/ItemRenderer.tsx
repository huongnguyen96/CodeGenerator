import Icon from 'antd/lib/icon';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { NavLink, withRouter } from 'react-router-dom';
import { IDefaultSidebarProps } from './SidebarMenu';

function ItemRenderer(props: IDefaultSidebarProps) {
  const { item } = props;
  const propsForChild = {
    ...props,
  };
  delete propsForChild.staticContext;
  const [translate] = useTranslation();
  return (
    <NavLink role="menuitem"
      className="ant-menu-item"
      to={item.path}
      exact={item.exact}
      activeClassName="ant-menu-item-selected">
      <Icon type={item.icon} />
      <span>
        {translate(item.title)}
      </span>
    </NavLink>
  );
}

export default withRouter(ItemRenderer);
