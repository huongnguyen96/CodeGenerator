import {Model} from 'core/entities/Model';
import {useEffect, useState} from 'react';
import {Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';

export function useDetail<T extends Model>(id: string, getter: (id: string) => Observable<T>): [T, boolean] {
  const [loading, setLoading] = useState<boolean>(false);
  const [t, setT] = useState<T>(null);

  useEffect(
    () => {
      setLoading(true);
      getter(id)
        .pipe(
          finalize(() => {
            setLoading(false);
          }),
        )
        .subscribe((newT: T) => {
          setT(newT);
        });
    },
    [getter, id],
  );

  return [t, loading];
}
