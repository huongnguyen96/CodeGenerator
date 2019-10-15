import {IFilterType} from 'core/filters/IFilterType';

export abstract class Filter {
  public static readonly types?: IFilterType[];

  protected constructor(filter?: Filter) {
    Object.assign(this, filter);
  }

  public setField(field: string, value: any) {
    Object
      .keys(this)
      .forEach((key: string) => {
        if (key !== field) {
          this[key] = null;
        } else {
          this[key] = value;
        }
      });
  }
}
