import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { CreateUserCommand } from '../command/CreateUserCommand';
import { Observable } from 'rxjs/Observable';
import { AuthorizeUserCommand } from '../command/AuthorizeUserCommand';
import { UserVm } from '../vm/userVm';
import { AddPhotoForEmployerCommand } from '../../employer/commands/AddPhotoForEmployerCommand';

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class WebApiUsers{
    constructor(private http: HttpClient){

    }

    createuser(command:CreateUserCommand): Observable<void>{
        return this.http.post<void>("http://localhost:62576/api/users",command,httpOptions)
    }

    authorizeuser(command:AuthorizeUserCommand): Observable<UserVm>{
        return this.http.post<UserVm>("http://localhost:62576/api/users/authorize",command,httpOptions)
    }

    addPhotoForEmployer(command:AddPhotoForEmployerCommand): Observable<void>{
        return this.http.post<void>("http://localhost:62576/api/users/photo",command,httpOptions)
    }
}