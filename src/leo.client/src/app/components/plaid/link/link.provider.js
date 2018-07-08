import { LinkService } from './link.service';
export const LinkProvider = function(){
    var _linkHandlerArgs = {};
    this.link = (args) => {
        _linkHandlerArgs = {
            env : args.env || 'development',
            clientName : args.clientName || 'Leo Dev',
            key : args.publicKey || '1860746acb49e3756cfec151bbb1fc',
            product : args.product || ['auth', 'transactions', 'balance'],
            apiVersion : args.apiVersion || 'v2',
            selectAccount : args.selectAccount || false
        }
    };
    this.$get = ($log, $q) => {
        "ngInject";
        return new LinkService($log, $q, _linkHandlerArgs);
    };
}
