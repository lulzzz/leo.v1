import template from './boards.html';

export const BoardsComponent = {
  template,
  bindings : {
    boards : '<'
  },
  controller : class BoardsComponent{
    constructor($log, $scope, BoardsService, events){
      'ngInject';
      this.$log = $log;
      this.$scope = $scope;
      this.BoardsService = BoardsService;
      this.events = events;      
    }

    $onInit(){
      this.boardAddedSub = this.events.user.events.boardAdded
        .safeApply(this.$scope, (board) => {
          this.$log.debug('board added', board);          
          this.boards.push(board);
        })
        .subscribe();
    }

    $onDestroy(){
      this.boardAddedSub.dispose();
    }

    onAddBoard(board){
      this.BoardsService.addUserBoard(board.name);
    }
  }
}