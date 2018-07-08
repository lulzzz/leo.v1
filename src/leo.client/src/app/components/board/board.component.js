import template from './board.html';

export const BoardComponent = {
  template,
  bindings : {
    board : '<'
  },
  controller : class BoardComponent{
    constructor($log, BoardService, SessionService, events){
      'ngInject';
      this.$log = $log;
      this.svc = BoardService;
      this.session = SessionService;
      this.events = events;     
    }
    
    $onInit(){   
      return this.svc.connect(this.session.access_token, this.board.id);
    }

    $onDestroy(){
      this.$log.debug('board.component: destroy');
      this.svc.disconnect();      
    }
  }
};