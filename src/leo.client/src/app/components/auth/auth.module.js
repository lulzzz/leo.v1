import angular from 'angular';
import uiRouter from 'angular-ui-router';
import angularLocker from 'angular-locker';

import { AuthService } from './auth.service';
import { SessionService } from './session.service';
import { AuthInterceptorFactory } from './auth.interceptor';
import { LoginModule } from './login/login.module';
import { EventsModule } from './../events/events.module';

export const AuthModule = angular
    .module('auth', [
        uiRouter,
        angularLocker,
        LoginModule,
        EventsModule
    ])
    .config((lockerProvider, $httpProvider) => {
        'ngInject';
        lockerProvider.defaults({
            namespace: 'leo',
            separator: '.',
            eventsEnabled: true,
            extend: {}
        });
        
        $httpProvider.interceptors.push('AuthInterceptor');
    })
    .config((eventsProvider) => {
        "ngInject";
        eventsProvider.publisher('user', {
            url : 'http://localhost/hubs',
            hubs : {
                user : {
                    name : 'userHub',
                    events : {
                        boardAdded : 'BoardAdded'
                    }
                }
            }
        });
    })
    .factory('AuthInterceptor', AuthInterceptorFactory)
    .factory('OnSignedIn', ($rootScope, rx) => {
        'ngInject';
        var rvalue = new rx.Subject();
        $rootScope.$on('components.auth.onsignedin', (event, args) => {
            rvalue.onNext(args);
        });
        return rvalue;
    })
    .run(($log, $transitions, $state, $rootScope, AuthService) => {
        'ngInject';
        $transitions.onStart({
            to: (state) => {
                return !!(state.data && state.data.secured);
            }
        }, () => {
            $log.debug('secured state transition detected, authenticating...')
            return AuthService.authenticate()
                .then((response) => {
                    $rootScope.$broadcast('components.auth.onsignedin', response);
                })
                .catch((response) => {
                    $log.debug('unable to authenticate, returning to login...')
                    return $state.target('login');
                })
        });
    })
    .service('AuthService', AuthService)
    .service('SessionService', SessionService)
    .name;