import { Component, OnInit } from '@angular/core';
import { EventService } from '../state/EventService';
import { UserAuthorized } from '../login/events/UserAuthorized';
import { MatTableDataSource, MatSort, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AddWorkerDialog } from './dialog/AddWorkerDialog';
import { AddWorkerForEmployerCommand } from './commands/AddWorkerForEmployerCommand';
import { ApplicationState } from '../state/ApplicationState';
import { WebApiEmployers } from './services/WebApiEmployers';
import { WorkerVm } from './vm/WorkerVm';

@Component({
  selector: 'app-employer',
  templateUrl: './employer.component.html',
  styleUrls: ['./employer.component.css']
})
export class EmployerComponent implements OnInit {
  addWorkerDialog: MatDialogRef<AddWorkerDialog>;
  workers: Array<WorkerVm>;
  constructor(public eventService: EventService, public dialog: MatDialog, public appState: ApplicationState, public employerApi: WebApiEmployers) {
    
  }

  ngOnInit() {
    var event = new UserAuthorized();
    this.eventService.sendEvent<UserAuthorized>(event);
    this.loadWorkers();
  }

  add() {
    this.addWorkerDialog = this.dialog.open(AddWorkerDialog, {
      hasBackdrop: false,
      width: "300px"
    });

    this.addWorkerDialog
      .afterClosed()
      .subscribe(result => {
        if (result != "") {
          var command = new AddWorkerForEmployerCommand(result);
          command.employerId = this.appState.LoggedUser.employerId;
          this.employerApi.addWorker(command).subscribe(data => {
            this.loadWorkers();
          });
        }
      });
  }

  loadWorkers() {
    this.employerApi.getWorkers(this.appState.LoggedUser.employerId)
      .subscribe((data: Array<WorkerVm>) => {
        this.workers = { ...data }
      });
  }
}
