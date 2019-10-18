import {Model} from 'core/entities/Model';
import {isGuid} from 'helpers/string';
import {useEffect, useState} from 'react';
import {Observable} from 'rxjs';
import {finalize} from 'rxjs/operators';

export function useDetail<T extends Model>(
  id: string,
  getter: (id: any) => Observable<T>,
  defaultValue: T = null,
): [T, boolean, (t: T) => void] {
  const [loading, setLoading] = useState<boolean>(false);
  const [t, setT] = useState<T>(defaultValue);

  useEffect(
    () => {
      if (isGuid(id)) {
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
      }
    },
    [getter, id],
  );

  return [t, loading, setT];
}
