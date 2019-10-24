import React from 'react';
import InputNumber from './InputNumber';

export default {title: 'InputNumber'};

export const defaultInput = () => (
  <InputNumber className="w-100" onChange={(event) => {
    // tslint:disable-next-line:no-console
    console.log(event);
  }}/>
);
