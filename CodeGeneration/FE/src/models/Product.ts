import {Model} from 'core';

import {Brand} from 'models/Brand';
import {Category} from 'models/Category';
import {Merchant} from 'models/Merchant';
import {ProductStatus} from 'models/ProductStatus';
import {ProductType} from 'models/ProductType';
import {EVoucher} from 'models/EVoucher';
import {Item} from 'models/Item';
import {VariationGrouping} from 'models/VariationGrouping';

export class Product extends Model {
   
  public id?: number;
 
  public code?: string;
 
  public name?: string;
 
  public description?: string;
 
  public typeId?: number;
 
  public statusId?: number;
 
  public merchantId?: number;
 
  public categoryId?: number;
 
  public brandId?: number;
 
  public warrantyPolicy?: string;
 
  public returnPolicy?: string;
 
  public expiredDate?: string;
 
  public conditionOfUse?: string;
 
  public maximumPurchaseQuantity?: number;

  public brand?: Brand;

  public category?: Category;

  public merchant?: Merchant;

  public status?: ProductStatus;

  public type?: ProductType;
  
  public eVouchers?: EVoucher[];
  
  public items?: Item[];
  
  public variationGroupings?: VariationGrouping[];

  public constructor(product?: Product) {
    super(product);
  }
}
