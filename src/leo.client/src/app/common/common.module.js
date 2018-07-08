import angular from 'angular';
import { NavbarModule } from './navbar/navbar.module';
import { CurrencyModule } from './currency/currency.module';

export const CommonModule = angular
  .module('app.common', [
    NavbarModule,
    CurrencyModule
  ])
  .name;