import {PaginationProps} from 'antd/lib/pagination';
import {Pagination} from 'core/entities/Pagination';
import {useState} from 'react';

export function useLocalPagination(): Pagination {
  const [pagination, setPagination] = useState<Pagination>(new Pagination());

  pagination.onChange = (current: number, pageSize: number) => {
    Object.assign<Pagination, PaginationProps>(pagination, {
      current,
      pageSize,
    });
    setPagination(new Pagination(pagination));
  };

  return pagination;
}
