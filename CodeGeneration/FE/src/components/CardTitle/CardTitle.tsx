import Button from 'antd/lib/button';
import React from 'react';
import {useTranslation} from 'react-i18next';
import './CardTitle.scss';

interface ICardTitleProps {
  title: string;
  allowAdd?: boolean;
  onAdd?: (event) => void;
  allowSave?: boolean;
  onSave?: (event) => void;
  allowCancel?: boolean;
  onCancel?: (event) => void;
  allowClear?: boolean;
  onClear?: (event) => void;
}

function CardTitle(props: ICardTitleProps) {
  const [translate] = useTranslation();

  const {
    title,
  } = props;

  return (
    <div className="ant-card-title">
      {title}
      <div className="ant-card-title-actions">
        {props.allowAdd && (
          <Button htmlType="button" type="primary" onClick={props.onAdd}>
            {translate('buttons.add')}
          </Button>
        )}
        {props.allowSave && (
          <Button htmlType="button" type="primary" onClick={props.onSave}>
            {translate('buttons.save')}
          </Button>
        )}
        {props.allowCancel && (
          <Button htmlType="button" type="default" onClick={props.onCancel}>
            {translate('buttons.cancel')}
          </Button>
        )}
        {props.allowClear && (
          <Button htmlType="button" type="default" onClick={props.onClear}>
            {translate('buttons.clear')}
          </Button>
        )}
      </div>
    </div>
  );
}

export default CardTitle;
