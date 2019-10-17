import {PaginationProps} from 'antd/lib/pagination';
import {Pagination} from 'core/entities/Pagination';
import {useState} from 'react';

export function usePagination(
  paginationProps: PaginationProps = {},
): [Pagination, (paginationProps: PaginationProps) => void] {
  const [pagination, setPagination] = useState<Pagination>(new Pagination(paginationProps));

  pagination.onChange = (current: number, pageSize: number) => {
    Object.assign<Pagination, PaginationProps>(pagination, {
      current,
      pageSize,
    });
    setPagination(new Pagination(pagination));
  };

  function handleChange(paginationProps: PaginationProps) {
    setPagination(new Pagination({
      ...pagination,
      ...paginationProps,
    }));
  }

  return [pagination, handleChange];
}
