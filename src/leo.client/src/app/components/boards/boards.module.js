import angular from 'angular';
import uiRouter from 'angular-ui-router';
import { BoardsComponent } from './boards.component';
import { BoardNewComponent } from './board-new/board-new.component';
import { BoardsService } from './boards.service';

export const BoardsModule = angular
  .module('boards', [
    uiRouter
  ])
  .component('boards', BoardsComponent)
  .component('boardNew', BoardNewComponent)
  .service('BoardsService', BoardsService)
  .config(($stateProvider) => {
    "ngInject";
    $stateProvider
      .state('boards', {
        url: '/boards',
        component: 'boards',
        data : {
          secured: true,
          nav : {
            text : 'Boards'
          }
        },
        resolve : {
          boards : (BoardsService) => {
            'ngInject';
            return BoardsService.getBoards();
          }
        }
      });
  })
  .name;