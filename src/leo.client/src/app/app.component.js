import template from './app.html';

export const AppComponent = {
  template,
  controller : class AppComponent{
    constructor($log, $state, $window, $scope, OnSignedIn, AuthService, SessionService){
      'ngInject';
      this.$state = $state;
      this.$log = $log;
      this.$window = $window;
      this.auth = AuthService;
      this.session = SessionService;
      this.$scope = $scope;
      this.OnSignedIn = OnSignedIn;
    }
    $onInit(){
      this.states = this.$state.get();

      this.onSignedInSub = this.OnSignedIn
        .safeApply(this.$scope, (args) => {
          this.signedIn = true;
        })
        .subscribe();
    }

    $onDestroy(){
      this.onSignedInSub.dispose();
    }

    onSigninSucceeded(){
      this.$log.debug('appcomponent: signin succeeded');
      this.signedIn = true;
      this.$state.go('boards');
    }

    onSigninFailed(){
      this.$log.debug('signin failed');
      if(this.signedIn)
        delete this.signedIn;
      
      this.$state.go('login');
    }

    onLogout(){
      this.$log.debug('logging out'); 
      
      this.auth.logout()
        .then(response => {
          delete this.signedIn;
          this.$state.go('login');
        });
    }
  }
};
