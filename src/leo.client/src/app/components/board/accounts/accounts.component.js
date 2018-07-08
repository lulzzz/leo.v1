import template from './accounts.html';

export const AccountsComponent = {
  template,
  bindings : {
    boardId : '<'
  },
  controller : class AccountsComponent{
    constructor($log, $scope, AccountsService, events, plaidLink){
      'ngInject';
      this.$log = $log;
      this.$scope = $scope;
      this.svc = AccountsService;      
      this.events = events;  
      this.plaidLink = plaidLink;
    }
    
    $onInit(){
      this.$log.debug('accounts.component: board events', this.events.board);
      this.accountAddedSub = this.events.board.events.accountAdded
        .safeApply(this.$scope, (account) => {
          this.$log.debug('accounts.component: account added', account);  
          var groupsFiltered = [];
          if(this.groups && this.groups.length > 0){
            groupsFiltered = this.groups.filter((group) => { return group.name.toLowerCase() === account.groupName.toLowerCase() })
          }        
          
          if(groupsFiltered && groupsFiltered.length > 0){
            this.groups[0].accounts.push(account);
            this.groups[0].total += account.current;
          }
          else{
            this.groups.push({
              name: account.groupName,
              total : account.current,
              accounts: [account]
            });            
          }

          this.$log.debug('accounts.component: groups updated', this.groups);
        })
        .subscribe();

        return this.svc.getAccounts(this.boardId)
          .then((groups) => {
            this.groups = groups;
          });
    }

    addAccounts(){
      this.plaidLink.open()
        .then((response) => {
            this.$log.debug('accounts.component: plaid link onSuccess', response);
            return this.svc.addToken(this.boardId, response.public_token, response.metadata.institution.name, response.metadata.institution.institution_id);
        })
        .catch((response) => {
          this.$log.debug('accounts.component: plaid link onError', response);
        });
    }

    $onDestroy(){
      this.accountAddedSub.dispose();
    }
  }
};