import i18nextConfig from 'config/i18next';
import {InitOptions as I18nOptions} from 'i18next';

/**
 * React global hooks
 */
export interface IGlobalData {
  /**
   * Sidebar status (visible or invisible)
   */
  sidebar?: boolean;
  /**
   * App spinning
   */
  spinning?: boolean;

  /**
   * i18next config
   */
  i18next?: I18nOptions;

  /**
   * Current language
   */
  lang?: string;
}

/**
 * Data interface of React global hooks.
 */
export const globalData: IGlobalData = {
  sidebar: true,
  spinning: false,
  i18next: i18nextConfig,
  lang: 'en',
};
