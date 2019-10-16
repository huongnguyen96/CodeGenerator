import {Filter, IFilterType} from 'core';
import {Equals, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo, NotEqualTo} from 'core/filters/types';

export type DateType = string | Date;

export class DateFilter extends Filter {

  public static readonly types: IFilterType[] = [
    Equals,
    NotEqualTo,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo,
  ];

  public equal: DateType = null;

  public notEqual: DateType = null;

  public greater: number = null;

  public less: number = null;

  public greaterEqual: number = null;

  public lessEqual: number = null;

  public constructor(dateFilter?: DateFilter) {
    super(dateFilter);
  }
}
