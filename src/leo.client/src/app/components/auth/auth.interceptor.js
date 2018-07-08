export function AuthInterceptorFactory($log, SessionService) {
    'ngInject';
    return {
        'request' : (config) => {
            
            if(SessionService.access_token){
                config.headers = config.headers || {};
                config.headers.Authorization = "Bearer " + SessionService.access_token;
                $log.debug('authinterceptor: attached auth header', config.headers.Authorization);
            }

            return config;
        }
    };
}
