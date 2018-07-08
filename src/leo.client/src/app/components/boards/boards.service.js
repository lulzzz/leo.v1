export class BoardsService{
    constructor($log, $http, $state){
        'ngInject';
        this.$log = $log;
        this.$http = $http;
        this.$state = $state;
    }

    addUserBoard(name){
        return this.$http.post('http://localhost/api/boards', { name : name })
            .then((response) => {
                this.$log.debug('boardsservice: post board success', response);
                return response.data;
            });
    }

    getBoards(){
        return this.$http.get('http://localhost/api/boards')
            .then((response) => {
                this.$log.debug('boardsservice: get boards success', response);
                return response.data.boards || [];
            });
    }
}