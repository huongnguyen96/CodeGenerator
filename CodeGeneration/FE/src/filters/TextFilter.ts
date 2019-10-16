import {Filter, IFilterType} from 'core';
import {Contains, EndsWith, Equals, NotContains, NotEndsWith, NotEqualTo, NotStartsWith, StartsWith} from 'core/filters/types';

export class TextFilter extends Filter {

  public static readonly types: IFilterType[] = [
    StartsWith,
    NotStartsWith,
    EndsWith,
    NotEndsWith,
    Contains,
    NotContains,
    Equals,
    NotEqualTo,
  ];

  public equal: string = null;

  public notEqual: string = null;

  public startsWith: string = null;

  public notStartsWith: string = null;

  public endsWith: string = null;

  public notEndsWith: string = null;

  public contains: string = null;

  public notContains: string = null;

  public constructor(textFilter?: TextFilter) {
    super(textFilter);
  }
}
