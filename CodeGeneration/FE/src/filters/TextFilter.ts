import {Filter, IFilterType} from 'core';
import {Contains, EndsWith, Equals, NotEqualsTo, StartsWith} from 'core/filters/types';

export class TextFilter extends Filter {

  public static readonly types: IFilterType[] = [
    StartsWith,
    EndsWith,
    Contains,
    Equals,
    NotEqualsTo,
  ];

  public equals: string = null;

  public notEqualTo: string = null;

  public startsWith: string = null;

  public endsWith: string = null;

  public contains: string = null;

  public constructor(textFilter?: TextFilter) {
    super(textFilter);
  }
}
