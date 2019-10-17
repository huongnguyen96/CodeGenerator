import {SorterResult, SortOrder} from 'antd/lib/table';

export function getAntSortOrder(sortOrder: SortOrder): 'ASC' | 'DESC' {
  switch (sortOrder) {
    case 'ascend':
      return 'ASC';

    case 'descend':
      return 'DESC';

    default:
      return null;
  }
}

export function getColumnSortOrder<T>(field: string, sorter: SorterResult<T>) {
  return field === sorter.field ? sorter.order : undefined;
}
