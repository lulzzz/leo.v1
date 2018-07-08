import angular from 'angular';

import { LinkProvider } from './link.provider'

export const LinkModule = angular
    .module('vendors.plaid.link', [])
    .provider('plaidLink', LinkProvider)
    .name;