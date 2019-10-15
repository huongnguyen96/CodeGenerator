import Spin from 'antd/lib/spin';
import {IGlobalData} from 'config/global';
import React, {ReactElement, ReactNode} from 'react';
import {useTranslation} from 'react-i18next';
import {useGlobal} from 'reactn';
import './AppLoading.scss';

interface IAppLoadingProps {
  children?: ReactNode[] | Element[] | Element | ReactNode | ReactElement;
}

function AppLoading(props: IAppLoadingProps) {
  const {
    children = [],
  } = props;

  const [translate] = useTranslation();

  const [spinning] = useGlobal<IGlobalData>('spinning');

  return (
    <Spin tip={translate('general.loading')} spinning={!!spinning}>
      {children}
    </Spin>
  );
}

export default AppLoading;
