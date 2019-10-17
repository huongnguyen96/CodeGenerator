import Select, {SelectProps} from 'antd/lib/select';
import {INPUT_DEBOUNCE_TIME} from 'config/consts';
import {Model, Search} from 'core';
import debounce from 'lodash/debounce';
import React, {Dispatch, ReactNode, SetStateAction, useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';
import './SingleSelect.scss';

const {Option} = Select;

interface ISingleSelectProps<TSearch extends Search = {}> extends SelectProps {
  list?: any[];
  search?: TSearch;
  setSearch?: Dispatch<SetStateAction<any>>;
  searchField?: string;
  displayField?: string;
  getList?: (...params) => Observable<any[]>;
  onChange?: (event) => void;
  onSearch?: (value: string) => void;
  onError?: (error: Error) => void;
  render?: (element: any) => ReactNode;
  children?: ReactNode[];

  [key: string]: any;
}

export const SingleSelect = React.forwardRef(
  <T extends Model, TSearch extends Search = {}>(props: ISingleSelectProps<TSearch>, ref) => {
    const [list, setList] = useState<T[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [translate] = useTranslation();

    const handleSearch = debounce((value: string) => {
      props.setSearch({
        ...props.search,
        [props.searchField]: value,
      });
      reloadList();
    }, INPUT_DEBOUNCE_TIME);

    function reloadList() {
      setLoading(true);
      props.getList(props.search)
        .pipe(
          finalize(() => {
            setLoading(false);
          }),
        )
        .subscribe(
          (newList: T[]) => {
            setList(newList);
          },
          (error: Error) => {
            if (props.onError) {
              props.onError(error);
            }
          },
        );
    }

    function handleToggle(state: boolean) {
      if (state) {
        props.setSearch({
          ...props.search,
          [props.searchField]: null,
        });
        reloadList();
      }
    }

    if (props.search) {
      if (!props.searchField) {
        throw new Error(translate('components.singleSelect.errors.searchFieldRequired'));
      }
    }

    return (
      <Select
        ref={ref}
        {...props}
        mode="single"
        loading={loading}
        onSearch={handleSearch}
        onDropdownVisibleChange={handleToggle}>
        {list.length === 0 && props.children}
        {list.map(props.render)}
      </Select>
    );
  },
);

SingleSelect.defaultProps = {
  list: [],
  displayField: 'name',
  searchField: 'name',
  filterOption: false,
  allowClear: true,
  allowClearSearchValue: false,
  render: (element) => (
    <Option value={element.id} key={element.id}>
      {element.name}
    </Option>
  ),
};

export default SingleSelect;

export {Option};
