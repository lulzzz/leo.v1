import angular from 'angular';
import uiRouter from 'angular-ui-router';

import { EnvelopesComponent } from './envelopes.component';
import { EnvelopesService } from './envelopes.service';

export const EnvelopesModule = angular
  .module('envelopes', [
  ])
  .service('EnvelopesService', EnvelopesService)
  .component('envelopes', EnvelopesComponent) 
  .config(($stateProvider) => {
    "ngInject";
    $stateProvider
      .state('envelopes', {
        url: '/envelopes',
        parent: 'board',
        component: 'envelopes',
        resolve:{
          boardId : ($stateParams) => {
            'ngInject';
            return $stateParams.boardId;
          },
          envelopes : ($stateParams, EnvelopesService) => {
              'ngInject';
              return EnvelopesService.getEnvelopes($stateParams.boardId);
          }
        }
      });
  })
  .name;