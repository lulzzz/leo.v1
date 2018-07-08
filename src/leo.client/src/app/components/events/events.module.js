import angular from 'angular';
import rx from 'rx-angular';
import $ from 'jquery';
import './../../../../node_modules/signalr/jquery.signalR.js';

import { EventsProvider } from './events.provider'

export const EventsModule = angular
    .module('core.events', ['rx'])
    .value('rx', rx)
    .provider('events', EventsProvider)
    .name;