import rx from 'rx-angular';
import { EventsService } from './events.service';

export const EventsProvider = function(){
    var _publishers = {};

    this.publisher = (name, publisher) => {
        if(publisher){
            var publishernew = {};
            if(publisher.url){
                const conn = $.hubConnection(publisher.url);
                conn.logging = publisher.logging || false;
                publishernew.connection = conn;
                publisher.hubs = publisher.hubs || [];
                
                for(var hub in publisher.hubs){
                    var proxy = conn.createHubProxy(publisher.hubs[hub].name);
                    publishernew.proxy = proxy;
                    publishernew.events = {};
                    for(var event in publisher.hubs[hub].events){
                        publishernew.events[event] = new rx.Subject();

                        //Have to create a new closure so that its value for event and subscription are not affected by the loop.
                        (function(eventName, subscription){
                            proxy.on(eventName, function(args){
                                subscription.onNext(args);
                            });
                        })(event, publishernew.events[event])
                    }
                }

                _publishers[name] = publishernew;                
            }
            else{
                throw 'publisher url required.';
            }
        }
        else{
            throw 'publisher required.';
        }
    }

    this.$get = ($log, $q) => {
        "ngInject";
        return new EventsService($log, $q, _publishers);
    }
}