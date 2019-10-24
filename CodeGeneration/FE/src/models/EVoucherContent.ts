import {Model} from 'core';

import {EVoucher} from 'models/EVoucher';

export class EVoucherContent extends Model {
   
  public id?: number;
 
  public eVourcherId?: number;
 
  public usedCode?: string;
 
  public merchantCode?: string;
 
  public usedDate?: string | Date;

  public eVourcher?: EVoucher;

  public constructor(eVoucherContent?: EVoucherContent) {
    super(eVoucherContent);
  }
}
