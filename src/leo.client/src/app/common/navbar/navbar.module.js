import angular from 'angular';
import { NavbarComponent } from './navbar.component';

export const NavbarModule = angular
  .module('navbar', [])
  .filter('navigable', () => {
      return (navItems) => {
        let rvalues = [];
        navItems.forEach(function(navItem) {
            if(navItem.data && navItem.data.nav){
                rvalues.push(navItem);
            }            
        });
        return rvalues;
      }
  })
  .filter('secured', () => {
      return (navItems, signedIn) => {
          let rvalues = [];
          if(!signedIn || signedIn == false){
              navItems.forEach((navItem) => {
                  if(!navItem.data || !navItem.data.secured || navItem.data.secured === false){
                      rvalues.push(navItem);
                  }
              });
          }
          else{
              rvalues = navItems;
          }
          return rvalues;
      }
  })
  .component('navbar', NavbarComponent)
  .name;