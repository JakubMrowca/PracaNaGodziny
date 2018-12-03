import { Component, OnInit } from '@angular/core';
import { CreateUserCommand } from './command/CreateUserCommand';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { WebApiUsers } from './services/WebApiUsers';
import { AuthorizeUserCommand } from './command/AuthorizeUserCommand';
import { ApplicationState } from '../state/ApplicationState';
import { Router } from '@angular/router';
import { SelectProfilTypeDialog } from './dialog/SelectProfilTypeDialog';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { RoutingEnum } from '../state/RoutingEnum';
import { EventService } from '../state/EventService';
import { UserAuthorized } from './events/UserAuthorized';
import { SignalREventEmiter } from '../state/SignalREventEmiter';
import { UserVm } from './vm/userVm';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  createUserCommand: CreateUserCommand;
  authorizeUserCommand: AuthorizeUserCommand;
  reapetPassword: string;
  profilTypeDialog: MatDialogRef<SelectProfilTypeDialog>;
  authorization = false;

  constructor(private router: Router,public signalR:SignalREventEmiter, public dialog: MatDialog, public snackBar: MatSnackBar, public webApiUsers: WebApiUsers, public appState: ApplicationState, private eventService: EventService) {
    this.createUserCommand = new CreateUserCommand();
    this.authorizeUserCommand = new AuthorizeUserCommand();
    signalR.initUserConnection();
    this.eventService.getMessage<UserAuthorized>("UserAuthorized").subscribe(data =>{
      this.onAuthorize(data)
    })
  }

  ngOnInit() {

  }

  onAuthorize(data){
    this.authorization = false;
    if (this.appState.IsEmployer() && this.appState.IsWorker()) {

      this.profilTypeDialog = this.dialog.open(SelectProfilTypeDialog, {
        hasBackdrop: false
      });

      this.profilTypeDialog
        .afterClosed()
        .subscribe(result => {
          if (result.isEmployer)
            this.navigate(RoutingEnum.employer);
          else
            this.navigate(RoutingEnum.worker);
        });
    }
    else if (this.appState.IsEmployer() && !this.appState.IsWorker())
      this.navigate(RoutingEnum.employer);
    else
      this.navigate(RoutingEnum.worker)
  }
  onSubmit(form: NgForm) {
    if (form.invalid == true)
      return;
    if (this.reapetPassword != this.createUserCommand.password) {
      this.snackBar.open("Hasla musza byc takie same!", "", {
        duration: 2000,
      });
      return;
    }

    this.webApiUsers.createuser(this.createUserCommand)
      .subscribe(data => {
        this.snackBar.open("Użytkownik utworzony!", "", {
          duration: 2000,
        });
        this.clear();
      });
  }

  login() {
    this.authorization = true;
    this.authorizeUserCommand.connectionId = this.appState.HubConnectionId;
    this.webApiUsers.authorizeuser(this.authorizeUserCommand)
      .subscribe(data => {},error =>{
        this.authorization = false;
      })
  }

  navigate(to: RoutingEnum) {
    this.router.navigate([to.toString()])
  }

  clear() {
    this.createUserCommand = new CreateUserCommand();
    this.reapetPassword = null;
  }

}
