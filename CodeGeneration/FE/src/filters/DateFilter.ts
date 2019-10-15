import {Filter, IFilterType} from 'core';
import {After, Before, Equals, From, NotEqualsTo, To} from 'core/filters/types';

export type DateType = string | Date;

export class DateFilter extends Filter {

  public static readonly types: IFilterType[] = [
    Equals,
    NotEqualsTo,
    After,
    Before,
    From,
    To,
  ];

  public equals: DateType = null;

  public notEqualTo: DateType = null;

  public after: DateType = null;

  public before: DateType = null;

  public from: DateType = null;

  public to: DateType = null;

  public constructor(dateFilter?: DateFilter) {
    super(dateFilter);
  }
}
