import {Filter} from 'core';

export class ListFilter<T> extends Filter {
  protected data: T;

  public constructor(listFilter?: ListFilter<T>, data?: T) {
    super(listFilter);
    this.setData(data);
  }

  public setData(data: T) {
    this.data = data;
  }
}
