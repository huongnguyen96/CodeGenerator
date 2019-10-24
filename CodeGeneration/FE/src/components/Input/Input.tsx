import Checkbox from 'antd/lib/checkbox';
import DatePicker from 'antd/lib/date-picker';
import AntInput from 'antd/lib/input';
import Radio from 'antd/lib/radio';
import TimePicker from 'antd/lib/time-picker';
import SingleSelect from 'components/SingleSelect';
import React, {Ref} from 'react';
import './Input.scss';

const {RangePicker} = DatePicker;

interface IInputProps {
  type: 'text' | 'textarea' | 'password' | 'hidden' | 'select' | 'checkbox' | 'radio' | 'date' | 'time' | 'date-range';

  [key: string]: any;
}

const Input = React.forwardRef((props: IInputProps, ref: Ref<any>) => {
  const {type, ...restProps} = props;

  switch (type) {
    case 'date':
      return (
        <DatePicker ref={ref} {...restProps}/>
      );

    case 'date-range':
      return (
        <RangePicker ref={ref} {...restProps}/>
      );

    case 'checkbox':
      return (
        <Checkbox ref={ref} {...restProps}/>
      );

    case 'select':
      return (
        <SingleSelect ref={ref} {...restProps}/>
      );

    case 'time':
      return (
        <TimePicker ref={ref} {...restProps}/>
      );

    case 'radio':
      return (
        <Radio ref={ref} {...restProps}/>
      );

    default:
      return (
        <AntInput ref={ref} type={type} {...restProps}/>
      );
  }
});

Input.defaultProps = {
  type: 'text',
};

export default Input;
