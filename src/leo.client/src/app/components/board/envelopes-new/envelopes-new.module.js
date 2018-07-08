import angular from 'angular';
import uiRouter from 'angular-ui-router';
import ngTagsInput from 'ng-tags-input';
import './envelopes-new.scss';

import { EnvelopesNewComponent } from './envelopes-new.component';
import { EnvelopesNewService } from './envelopes-new.service';

export const EnvelopesNewModule = angular
  .module('envelopes.new', [
    'ngTagsInput'
  ])
  .service('EnvelopesNewService', EnvelopesNewService)
  .component('envelopes.new', EnvelopesNewComponent) 
  .config(($stateProvider) => {
    "ngInject";
    $stateProvider
      .state('envelopes-new', {
        url: '/envelopes/new',
        component: 'envelopes.new',
        parent: 'board',
        resolve:{
          boardId : ($stateParams) => {
            'ngInject';
            return $stateParams.boardId;
          }
        }
      });
  })
  .name;