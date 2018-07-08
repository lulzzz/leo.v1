import angular from 'angular';
import uiRouter from 'angular-ui-router';
import './board.scss';

import { AuthModule } from './../auth/auth.module';
import { EventsModule } from './../events/events.module';
import { BoardComponent } from './board.component';
import { BoardService } from './board.service';
import { AccountsModule } from './accounts/accounts.module';
import { EnvelopesModule } from './envelopes/envelopes.module';
import { EnvelopesNewModule } from './envelopes-new/envelopes-new.module';


export const BoardModule = angular
  .module('board', [
    uiRouter,
    AuthModule,
    EventsModule,
    AccountsModule,
    EnvelopesModule,
    EnvelopesNewModule
  ])
  .service('BoardService', BoardService)
  .component('board', BoardComponent)  
  .config(($stateProvider) => {
    "ngInject";
    $stateProvider
      .state('board', {
        url: '/boards/:boardId',
        component: 'board',
        data : {
          secured: true
        },
        resolve : {
          board : (BoardService, $stateParams) => {
            'ngInject';
            return BoardService.getBoard($stateParams.boardId);
          }
        }
      });
  })
  .config((eventsProvider) => {
      "ngInject"
      eventsProvider.publisher('board', {
          url : 'http://localhost/hubs',
          hubs : {
              board : {
                  name : 'boardHub',
                  events : {
                      accountAdded : 'AccountAdded'
                  }
              }
          }
      });
  })
  .name;