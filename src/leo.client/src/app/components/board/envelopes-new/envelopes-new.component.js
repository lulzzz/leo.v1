import template from './envelopes-new.html';

export const EnvelopesNewComponent = {
  template,
  bindings : {
    boardId : '<',
    categories : '<'
  },
  controller : class EnvelopesNewComponent{

    constructor($log, EnvelopesNewService){
      'ngInject';
      this.$log = $log;
      this.svc = EnvelopesNewService;
    }

    $onInit(){
      this.master = {};
    }

    add(envelope){
      this.$log.debug('envelopesnewcontroller: adding envelope', { boardId : this.boardId, envelope });
      //this.svc.addEnvelope(this.boardId, envelope)
      this.board = angular.copy(this.master);
    }

    searchCategories($query){
      this.$log.debug('envelopesnewcontroller: searching categories', { boardId : this.boardId, $query });
      return this.svc.searchCategories(this.boardId, $query);      
    }
  }
}