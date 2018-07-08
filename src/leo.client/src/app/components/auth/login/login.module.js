import angular from 'angular';
import uiRouter from 'angular-ui-router';
import { LoginComponent } from './login.component';
import './login.scss';

export const LoginModule = angular
  .module('login', [
    uiRouter
  ])
  .component('login', LoginComponent)
  .config(($stateProvider) => {
    "ngInject";
    $stateProvider
      .state('login', {
        url: '/login',
        template: '<login on-signin-succeeded="$ctrl.onSigninSucceeded()" on-signin-failed="$ctrl.onSigninFailed()"></login>'
      });
  })
  .name;