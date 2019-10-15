import {Filter, IFilterType} from 'core';
import {Equals, GreaterThan, GreaterThanOrEqualTo, LessThan, LessThanOrEqualTo, NotEqualsTo} from 'core/filters/types';

export class NumberFilter extends Filter {

  public static readonly types: IFilterType[] = [
    Equals,
    NotEqualsTo,
    GreaterThanOrEqualTo,
    GreaterThan,
    LessThanOrEqualTo,
    LessThan,
  ];

  public equals: number = null;

  public notEqualTo: number = null;

  public greaterThan: number = null;

  public lessThan: number = null;

  public greaterThanOrEqualTo: number = null;

  public lessThanOrEqualTo: number = null;

  public constructor(numberFilter?: NumberFilter) {
    super(numberFilter);
  }
}
