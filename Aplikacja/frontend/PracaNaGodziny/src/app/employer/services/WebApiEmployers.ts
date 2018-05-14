import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { AddWorkerForEmployerCommand } from '../commands/AddWorkerForEmployerCommand';
import { Observable } from 'rxjs/Observable';
import { EmployerVm } from '../vm/EmployerVm';
import { AddPhotoForEmployerCommand } from '../../employer/commands/AddPhotoForEmployerCommand';
import { WorkerVm } from '../vm/WorkerVm';

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class WebApiEmployers{
    constructor(private http: HttpClient){

    }

    addWorker(command:AddWorkerForEmployerCommand): Observable<void>{
        return this.http.post<void>("http://localhost:62576/api/work",command,httpOptions)
    }

    getWorkers(id): Observable<Array<WorkerVm>>{
        return this.http.get<Array<WorkerVm>>("http://localhost:62576/api/work/"+ id,httpOptions);
    }
}