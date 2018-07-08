import template from './login.html';

export const LoginComponent = {
  template,
  bindings : {
    onSigninSucceeded : '&',
    onSigninFailed : '&'
  },
  controller : class LoginComponent{
    constructor($log, $window, $location, AuthService){
      'ngInject';
      this.$window = $window;
      this.$location = $location;
      this.$log = $log;
      this.AuthService = AuthService;
    }

    $onInit(){
      var self = this;
      var params = this.$location.search();
      this.$log.debug('login params', params);
      if(params.access_token){
          this.$log.debug('authenticating access token', params.access_token);
          this.AuthService.authenticate(params.access_token)
              .then((validatedToken) => {
                  this.$log.debug('logincomponent: authenticated access token', validatedToken);
                  this.onSigninSucceeded();
              })            
              .catch((response) => {
                  this.$log.debug('failed to authenticate access token', response);
                  this.onSigninFailed();
              });
      }
    }

    login(authType){
        var url = this.AuthService.getLogin(authType);
        this.$log.debug('redirecting to url for sign in', url);
        this.$window.location = url;
    }
  }
};