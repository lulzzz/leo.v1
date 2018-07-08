export class EventsService{
    constructor($log, $q, publishers){
        this.$log = $log;
        this.$q = $q;
        for(var publisher in publishers){
            this[publisher] = publishers[publisher];
        }
    }

    connect(qs, connection){
        this.$log.debug('starting signalr connection', connection.url);
        return this.$q((resolve, reject) => {
            if(connection.state === 1){
                this.$log.debug('signalr connection, already started', connection.state);
                resolve(connection);
            }
            else{
                connection.qs = qs;
                connection.start()
                    .done((response) => {
                        this.$log.debug('signalr connection started', response);
                        resolve(response);
                    })
                    .catch((response) => {
                        this.$log.error('signalr connection failed to start', response);
                        reject(response);
                    })
            }                
        });
    }

    disconnect(connection){
        connection.stop();
    }
}