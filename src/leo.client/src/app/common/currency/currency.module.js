import angular from 'angular';
import { CurrencyDirective } from './currency.directive';

export const CurrencyModule = angular
  .module('app.common.currency', [
  ])
  .directive('currency', () => new CurrencyDirective())
  .name;