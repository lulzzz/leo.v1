import template from './navbar.html';

export const NavbarComponent = {
  template,
  bindings : {
    onLogOut: '&',
    states: '<',
    signedIn: '<'
  },
  controller : class NavbarComponent{
    constructor(){
      'ngInject';
    }

    $onInit(){
      var navItems = [];
      
      this.states.forEach(function(state) {
          if(state.data && state.data.nav){
              navItems.push({
                  sref: state.name,
                  name: state.data.nav.text || state.name,
                  data : state.data
              });
          }            
      });

      this.navItems = navItems;
    }

    $onChange(){      
    }
  }
};