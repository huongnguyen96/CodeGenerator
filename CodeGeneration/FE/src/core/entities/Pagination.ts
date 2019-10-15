import {PaginationProps} from 'antd/lib/pagination';
import * as React from 'react';

export class Pagination implements PaginationProps {
  public total?: number = 0;

  public current?: number = 1;

  public defaultCurrent?: number;

  public disabled?: boolean;

  public defaultPageSize?: number;

  public pageSize?: number;

  public hideOnSinglePage?: boolean;

  public showSizeChanger?: boolean;

  public pageSizeOptions?: string[];

  public onShowSizeChange?: (current: number, size: number) => void;

  public showQuickJumper?: boolean | {
    goButton?: React.ReactNode;
  };

  public showTotal?: (total: number, range: [number, number]) => React.ReactNode;

  public size?: string;

  public simple?: boolean;

  public style?: React.CSSProperties;

  public locale?: { [key: string]: any };

  public className?: string;

  public prefixCls?: string;

  public selectPrefixCls?: string;

  public itemRender?: (
    page: number,
    type: 'page' | 'prev' | 'next' | 'jump-prev' | 'jump-next',
    originalElement: React.ReactElement<HTMLElement>,
  ) => React.ReactNode;

  public role?: string;

  public showLessItems?: boolean;

  public constructor(pagination?: PaginationProps) {
    if (pagination) {
      Object.assign(this, pagination);
    }
  }

  public onChange(page: number, pageSize: number) {
    this.current = page;
    this.pageSize = pageSize;
  }
}
