import {Filter, IFilterType} from 'core';
import {Equals, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo, NotEqualTo} from 'core/filters/types';

export class NumberFilter extends Filter {

  public static readonly types: IFilterType[] = [
    Equals,
    NotEqualTo,
    GreaterThanOrEqualTo,
    GreaterThan,
    LessThanOrEqualTo,
    LessThan,
  ];

  public equal: number = null;

  public notEqual: number = null;

  public greater: number = null;

  public less: number = null;

  public greaterEqual: number = null;

  public lessEqual: number = null;

  public constructor(numberFilter?: NumberFilter) {
    super(numberFilter);
  }
}
