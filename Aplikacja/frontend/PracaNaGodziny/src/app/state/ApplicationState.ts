import { Injectable } from '@angular/core';
import { UserVm } from '../login/vm/userVm';

@Injectable()
export class ApplicationState {
    public LoggedUser: UserVm;
    IsAuthorize: boolean;

    IsEmployer(): boolean {
        if (this.LoggedUser.employerId != null && this.LoggedUser.employerId != "")
            return true;
        else
            return false;
    }

    IsWorker(): boolean {
        if (this.LoggedUser.workerId != null && this.LoggedUser.workerId != "")
            return true;
        else
            return false;
    }

    SetLoggedUser(user: UserVm) {
        this.LoggedUser = user;
        if (user != null)
            this.IsAuthorize = true;
        else
            this.IsAuthorize = false;
    }

    SetPhoto(){
       if(this.IsAuthorize){

       }
    }

    GetEmployerName():string{
        return this.LoggedUser.employerName;
    }

    Logout(){
        this.IsAuthorize = false;
        this.LoggedUser = null;
    }
}