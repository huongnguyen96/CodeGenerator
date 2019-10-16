import Select from 'antd/lib/select';
import {INPUT_DEBOUNCE_TIME} from 'config/consts';
import {Model, Search} from 'core';
import {useSelect} from 'core/hooks/useSelect';
import debounce from 'lodash/debounce';
import React, {ReactNode} from 'react';
import {Observable} from 'rxjs';
import './SingleSelect.scss';

const {Option} = Select;

interface ISingleSelectProps {
  list?: any[];
  search?: Search;
  getList?: (...params) => Observable<any[]>;
  onChange?: (event) => void;
  onSearch?: (value: string) => void;
  render?: (element: any) => ReactNode;

  [key: string]: any;
}

function SingleSelect<T extends Model, TSearch extends Search = null>(props: ISingleSelectProps) {
  const [list, loading] = useSelect<T, TSearch>(props.getList, props.search as TSearch);

  const handleSearch = debounce((value: string) => {
    props.onChange(value);
  }, INPUT_DEBOUNCE_TIME);

  return (
    <Select
      mode="single"
      loading={loading}
      showSearch
      onSearch={handleSearch}>
      {list.map(props.render)}
    </Select>
  );
}

SingleSelect.defaultProps = {
  list: [],
  render: (element) => (
    <Option value={element.value}>
      {element.label}
    </Option>
  ),
};

export default SingleSelect;
