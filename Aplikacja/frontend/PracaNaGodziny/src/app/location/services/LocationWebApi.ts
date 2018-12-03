import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { LocationVm } from '../../client/vm/LocationVm';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class LocationWebApi {
    constructor(private http: HttpClient) {

    }

    getLocation(id): Observable<LocationVm>{
        return this.http.get<LocationVm>("http://localhost:62576/api/client/location/"+ id,httpOptions);
    }

}