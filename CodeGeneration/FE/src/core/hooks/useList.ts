import {PaginationConfig} from 'antd/lib/pagination';
import {SorterResult} from 'antd/lib/table';
import {Model, Search} from 'core';
import {getAntSortOrder} from 'helpers/ant-design';
import {useEffect, useState} from 'react';
import {forkJoin, Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';

export function useList<T extends Model, TSearch extends Search>(
  search: TSearch,
  setSearch: (tSearch?: TSearch) => void,
  getList: (search: TSearch) => Observable<T[]>,
  count: (search: TSearch) => Observable<number>,
  defaultList: T[] = [],
): [
  T[],
  number,
  boolean,
  SorterResult<T>,
  (pagination: PaginationConfig, filter: Record<string, any>, sorter: SorterResult<T>) => void,
  () => void,
] {
  const [list, setList] = useState<T[]>(defaultList);
  const [loading, setLoading] = useState<boolean>(false);
  const [total, setTotal] = useState<number>(0);
  const [sorter, setSorter] = useState<SorterResult<T>>({} as SorterResult<T>);

  function handleChange(pagination: PaginationConfig, filter: Record<string, any>, sorter: SorterResult<T>) {
    setSorter(sorter);
    setSearch({
      ...search,
      ...filter,
      skip: (pagination.current - 1) * pagination.pageSize,
      take: pagination.size,
      orderBy: sorter.field,
      orderType: getAntSortOrder(sorter.order),
    });
  }

  function handleClear() {
    setSorter({
      order: undefined,
      field: null,
    } as SorterResult<T>);
  }

  useEffect(
    () => {
      setLoading(true);
      forkJoin(
        getList(search),
        count(search),
      )
        .pipe(
          finalize(() => {
            setLoading(false);
          }),
        )
        .subscribe(
          ([newList, newTotal]) => {
            setList(Object.values(newList));
            setTotal(newTotal);
          },
        );
    },
    [search, sorter, count, getList],
  );

  return [list, total, loading, sorter, handleChange, handleClear];
}
