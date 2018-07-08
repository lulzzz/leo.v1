export class EnvelopesNewService{
    constructor($log, $http){
        'ngInject';
        this.$log = $log;
        this.$http = $http;
    }

    addEnvelope(boardId, envelope){
        return this.$http.post('http://localhost/api/boards/' + boardId + '/envelopes', envelope)
            .then((response) => {
                this.$log.debug('envelopesnewservice: post envelope success', response);
                return response.data;
            });
    }

    searchCategories(boardId, search){
        return this.$http.get('http://localhost/api/boards/' + boardId + '/categories', { params: { search } })
            .then((response) => {
                this.$log.debug('envelopesnewservice: search categories success', response);
                var rvalues = [response.data.categories.length];
                for(var i = 0; i < response.data.categories.length; i++){
                    var text = undefined;
                    response.data.categories[i].hierarchy.forEach(h => {
                        if(text){
                            text += ' > ' + h;
                        }
                        else{
                            text = h;
                        }
                    });

                    rvalues[i] = { text, id : response.data.categories[i].categoryId };
                    //rvalues[i] = text;
                }
                this.$log.debug('envelopesnewservice: search categories formatted', rvalues);
                return rvalues;
            })
    }
}