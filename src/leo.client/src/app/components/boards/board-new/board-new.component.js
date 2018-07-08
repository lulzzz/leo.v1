import template from './board-new.html';

export const BoardNewComponent = {
  template,
  bindings : {
    onAdd : '&',
    board : '<'
  },
  controller : class BoardsComponent{
    constructor($log){
      'ngInject';
      this.$log = $log;
    }
    
    $onInit(){
        this.master = {};
    }

    cancel(){
        this.board = angular.copy(this.master);
    }

    add(board){
        this.onAdd({ board : board });
        this.board = angular.copy(this.master);
    }
  }
};