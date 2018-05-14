import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
 
@Injectable()
export class EventService {
    private subject = new Subject<any>();
 
    sendEvent<T>(message:T) {
        this.subject.next(message);
    }
 
    clearMessage() {
        this.subject.next();
    }
 
    getMessage<T>(): Observable<T> {
        return this.subject.asObservable();
    }
}