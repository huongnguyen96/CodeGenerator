import {useEffect, useState} from 'react';
import {forkJoin, Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';
import {Model, Search} from '..';

export function useList<T extends Model, TSearch extends Search>(
  defaultList: T[],
  search: TSearch,
  getList: (search: TSearch) => Observable<T[]>,
  count: (search: TSearch) => Observable<number>,
): [T[], number, boolean] {
  const [list, setList] = useState<T[]>(defaultList);
  const [loading, setLoading] = useState<boolean>(false);
  const [total, setTotal] = useState<number>(0);

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
          ([newList, newCount]) => {
            setList(newList);
            setTotal(newCount);
          },
        );
    },
    [getList, search, count, setLoading],
  );

  return [list, total, loading];
}
