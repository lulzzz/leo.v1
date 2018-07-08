export class AuthService {
    constructor($log, $state, $http, $httpParamSerializer, SessionService, events){
        'ngInject';
        this.$log = $log;
        this.$state = $state;
        this.$http = $http;
        this.$httpParamSerializer = $httpParamSerializer;
        this.session = SessionService;
        this.events = events;
    }

    getLogin(authType){
        return "http://localhost/api/login?" + this.$httpParamSerializer({ 
            authenticationType : authType, 
            redirectUrl : this.$state.href('login', {}, { absolute : true })
        });
    }

    logout(){
        return this.$http.get("http://localhost/api/logout", {
            headers : {
                Authorization : "Bearer " + this.session.access_token
            }
        })
        .then(response => {
            this.session.removeAccessToken();
            return this.events.disconnect(this.events.user.connection);
        })
    }  

    authenticate(access_token){
        access_token = access_token || this.session.access_token;
        return this.$http
            .get('http://localhost/api/auth', {
                headers : {
                    Authorization : "Bearer " + access_token
                }                
            })
            .then((response) => {
                this.$log.debug('authservice: authentication success', response);
                this.session.access_token = access_token;
                return this.events.connect({ access_token : access_token }, this.events.user.connection);
            })
            .catch((response) => {
                this.$log.debug('authentication failed', response);
                this.session.removeAccessToken();
                throw response;
            });
    }
}