export class SessionService {
    constructor($log, locker){
        this.$log = $log;
        this.locker = locker;
    }

    get access_token(){
        return this.locker.get('access_token') || undefined;
    }
    set access_token(value){
        this.locker.put('access_token', value);
    }    
    removeAccessToken(){
        this.locker.forget('access_token');
    }
}