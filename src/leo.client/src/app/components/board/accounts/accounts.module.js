import angular from 'angular';
import uiRouter from 'angular-ui-router';

import { AccountsComponent } from './accounts.component';
import { AccountsService } from './accounts.service';
import { LinkModule } from './../../plaid/link/link.module';
import { EventsModule } from './../../events/events.module';
import './accounts.scss';

export const AccountsModule = angular
  .module('accounts', [
    LinkModule,
    EventsModule
  ])
  .service('AccountsService', AccountsService)
  .component('accounts', AccountsComponent) 
  .config((plaidLinkProvider, $stateProvider) => {
    "ngInject";
    plaidLinkProvider.link({});
    $stateProvider
      .state('accounts', {
        url: '/accounts',
        parent: 'board',
        component: 'accounts',
        resolve:{
          boardId : ($stateParams) => {
            'ngInject';
            return $stateParams.boardId;
          }
        }
      });
  })
  .name;