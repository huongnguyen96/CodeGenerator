
import {Search} from 'core/entities/Search';

export class ItemSearch extends Search {
  
  public id?: number;

  public code?: string;

  public name?: string;

  public sKU?: string;

  public typeId?: number;

  public purchasePrice?: number;

  public salePrice?: number;

  public description?: string;

  public statusId?: number;

  public unitOfMeasureId?: number;

  public supplierId?: number;
;
}
