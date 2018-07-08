export class AccountsService{
    constructor($log, $http){
        'ngInject';
        this.$log = $log;
        this.$http = $http;
    }

    addToken(boardId, publicToken, name, id){
        return this.$http.post("http://localhost/api/" + boardId + "/accounts/token", { publicToken, name, id });
    }
    
    getAccounts(boardId){
        return this.$http.get('http://localhost/api/boards/' + boardId + '/accountgroups')
            .then((response) => {
                this.$log.debug('accounts.service: get account groups success', response);
                if(response.data.groups){
                    response.data.groups.forEach((group) => {
                        group.totalDisplay = group.total.toLocaleString('en-US', { style : 'currency', currency : 'USD' });
                    });
                }
                
                return response.data.groups || [];
            });
    }
}