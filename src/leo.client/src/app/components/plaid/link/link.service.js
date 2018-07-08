export class LinkService{
    constructor($log, $q, handlerArgs){
        this.$log = $log;
        this.$q = $q;
        this.handlerArgs = handlerArgs;
    }

    open(){
        return this.$q((resolve, reject) => {
            this.handlerArgs.onSuccess = (public_token, metadata) => {
                this.$log.debug('plaid.link.service.onSuccess', { public_token, metadata });
                resolve({ public_token, metadata });
            }
            this.handlerArgs.onExit = (error, metadata) => {
                this.$log.debug('plaid.link.service.onExit', { error, metadata });
                if(error != null)
                    reject({ error, metadata });
                else
                    resolve({ metadata });
            }
            Plaid.create(this.handlerArgs).open();
        });
        
        this.handler.open();
    }
}