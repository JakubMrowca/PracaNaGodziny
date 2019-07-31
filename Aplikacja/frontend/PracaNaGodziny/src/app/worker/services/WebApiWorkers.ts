import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WorkerVm } from '../vm/WorkerVm';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class WebApiWorkers {
    constructor(private http: HttpClient) {

    }

    getWorker(id): Observable<WorkerVm>{
        return this.http.get<WorkerVm>("http://localhost:62576/api/worker/"+ id,httpOptions);
    }

}