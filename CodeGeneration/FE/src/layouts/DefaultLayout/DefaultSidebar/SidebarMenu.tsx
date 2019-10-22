import Menu from 'antd/lib/menu';
import { IRoute } from 'core/IRoute';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { RouteComponentProps } from 'react-router-dom';
import ItemRenderer from './ItemRenderer';

const { SubMenu } = Menu;

export interface IDefaultSidebarProps extends RouteComponentProps {
  item: IRoute;
}

function SidebarMenu(props: IDefaultSidebarProps) {
  const { item } = props;

  if (item.children) {
    return (
      <SubMenu key={item.key} title={<ItemRenderer item={item} />} {...props}>
        {item.children.map((subItem: IRoute) => (
          <SidebarMenu {...props} key={subItem.key} item={subItem} />
        ))}
      </SubMenu>
    );
  }

  return (
    <ItemRenderer item={item} {...props} />
  );
}

export default SidebarMenu;
