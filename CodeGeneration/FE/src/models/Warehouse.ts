import {Model} from 'core';

import {Merchant} from 'models/Merchant';
import {Stock} from 'models/Stock';

export class Warehouse extends Model {
   
  public id?: number;
 
  public name?: string;
 
  public phone?: string;
 
  public email?: string;
 
  public address?: string;
 
  public partnerId?: number;

  public partner?: Merchant;
  
  public stocks?: Stock[];

  public constructor(warehouse?: Warehouse) {
    super(warehouse);
  }
}
