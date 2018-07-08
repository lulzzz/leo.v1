export class BoardService{
    constructor($log, $http, $q, events){
        'ngInject';
        this.$log = $log;
        this.$http = $http;
        this.$q = $q;
        this.events = events;
    }

    getBoard(id){
        return this.$http.get('http://localhost/api/boards/' + id)
            .then((response) => {
                this.$log.debug('boardservice: get board success', response);
                return response.data.board || {};
            })
    }

    connect(accessToken, id){
        return this.events.connect({ access_token : accessToken, boardid : id }, this.events.board.connection)
    }

    disconnect(){
        this.events.disconnect(this.events.board.connection);
    }    
}