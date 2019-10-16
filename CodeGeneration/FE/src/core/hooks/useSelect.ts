import {Model, Search} from 'core';
import {useEffect, useState} from 'react';
import {Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';

export function useSelect<T extends Model, TSearch extends Search = null>(
  getList: (search?: TSearch) => Observable<T[]>,
  search?: TSearch,
): [T[], boolean] {
  const [loading, setLoading] = useState<boolean>(false);
  const [list, setList] = useState<T[]>([]);

  useEffect(
    () => {
      setLoading(true);
      getList(search)
        .pipe(
          finalize(() => {
            setLoading(false);
          }),
        )
        .subscribe((newList: T[]) => {
          setList(newList);
        });
    },
    [search, getList],
  );

  return [list, loading];
}
