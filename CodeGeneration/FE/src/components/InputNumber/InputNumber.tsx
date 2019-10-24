import AntInput, {InputProps} from 'antd/lib/input';
import {formatNumber, parseNumber} from 'helpers/number-format';
import React, {LegacyRef, useState} from 'react';
import './InputNumber.scss';

interface IInputNumberProps extends InputProps {
  allowNegative?: boolean;
  onlyInteger?: boolean;
  formatter?: (x: number | string) => string;
  parser?: (x: string) => number;
  max?: number;
  min?: number;
  onChange?: (value) => void;

  value?: number;
}

const numberRegex: RegExp = new RegExp(`/[0-9]/`);

const InputNumber = React.forwardRef((props: IInputNumberProps, ref: LegacyRef<AntInput>) => {
  const [checkControl, setControl] = useState<boolean>(false);

  function handleKeyDown(event) {
    if (event.key === 'Control') {
      setControl(true);
    }
    if (event.key.length === 1 && !numberRegex.test(event.key) && !checkControl) {
      event.preventDefault();
    }
  }

  function handleKeyUp(event) {
    if (event.key === 'Control') {
      setControl(false);
    }
  }

  function handleChange(event) {
    props.onChange(parseNumber(event.target.value));
  }

  return (
    <AntInput type="number"
              onChange={handleChange}
              defaultValue={formatNumber(props.value)}
              onKeyDown={handleKeyDown}
              onKeyUp={handleKeyUp}
              ref={ref}/>
  );
});

InputNumber.defaultProps = {
  parser: parseNumber,
  formatter: formatNumber,
  allowNegative: true,
  onlyInteger: false,
};

export default InputNumber;
