import angular from 'angular';
import uiRouter from 'angular-ui-router';
import './app.scss';
import 'bootstrap';


import { AppComponent } from './app.component';
import { CommonModule } from './common/common.module';
import { ComponentsModule } from './components/components.module';

export const AppModule = angular
    .module('app', [
        CommonModule,
        ComponentsModule,
        uiRouter
    ])
    .config(($urlRouterProvider, $locationProvider, $logProvider) => {   
        "ngInject";
        $urlRouterProvider.otherwise('/login');
        $locationProvider.html5Mode(true);
        $logProvider.debugEnabled(true);
    })
    .component('app', AppComponent)
    .name;