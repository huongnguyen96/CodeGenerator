import React, {FC} from 'react';
import './ErrorPage.scss';

interface IProps {
  code?: number | string;
  message?: string;
}

const ErrorPage: FC<IProps> = (props: IProps) => {
  const {
    code,
  } = props;
  return (
    <div>
      {code}
    </div>
  );
};

export default ErrorPage;
