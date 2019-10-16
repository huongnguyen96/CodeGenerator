import {Model, Search} from 'core';

export function renderIndex<T extends Model, TSearch extends Search>(search: TSearch) {
  return (...params: [any, T, number]) => params[2] + 1 + search.skip;
}
