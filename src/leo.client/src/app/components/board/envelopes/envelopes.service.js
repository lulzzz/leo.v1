export class EnvelopesService{
    constructor($log, $http){
        'ngInject';
        this.$log = $log;
        this.$http = $http;
    }
    
    getEnvelopes(boardId){
        return this.$http.get('http://localhost/api/boards/' + boardId + '/envelopes')
            .then((response) => {
                this.$log.debug('envelopes.service: get envelopes success', response);                
                return response.data.envelopes || [];
            });
    }
}