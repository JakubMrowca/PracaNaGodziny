import { Component, OnInit } from '@angular/core';
import { EventService } from '../state/EventService';
import { UserAuthorized } from '../login/events/UserAuthorized';
import { MatTableDataSource, MatSort, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AddWorkerDialog } from './dialog/AddWorkerDialog';
import { AddWorkerForEmployerCommand } from './commands/AddWorkerForEmployerCommand';
import { ApplicationState } from '../state/ApplicationState';
import { WebApiEmployers } from './services/WebApiEmployers';
import { WorkerVm } from '../worker/vm/WorkerVm';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { Router } from '@angular/router';
import { RoutingEnum } from '../state/RoutingEnum';
import { LocationVm } from '../client/vm/LocationVm';

@Component({
  selector: 'app-employer',
  templateUrl: './employer.component.html',
  styleUrls: ['./employer.component.css']
})
export class EmployerComponent implements OnInit {
  addWorkerDialog: MatDialogRef<AddWorkerDialog>;
  workers: Array<WorkerVm>;
  locations: Array<LocationVm>;
  loadedData: boolean;
  constructor(private router:Router,public eventService: EventService, public dialog: MatDialog, public appState: ApplicationState, public employerApi: WebApiEmployers) {
    
  }

  ngOnInit() {
    var event = new UserAuthorized();
    this.eventService.sendEvent<UserAuthorized>(event);
    this.loadWorkers();
    this.loadLocations();
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

  loadLocations(){
    this.loadedData = true;
    this.employerApi.getLocations(this.appState.LoggedUser.employerId)
      .subscribe((data: Array<LocationVm>) => {
        this.locations = data;
        this.appState.Locations = data;
        this.loadedData = false;
      });
  }

  loadWorkers() {
    this.loadedData = true;
    this.employerApi.getWorkers(this.appState.LoggedUser.employerId)
      .subscribe((data: Array<WorkerVm>) => {
        this.workers = data
        this.loadedData = false;
      });
  }

  goToWorker(worker:WorkerVm){
    this.appState.SelectedWorker = worker;
    this.router.navigate([RoutingEnum.worker])
  }


}
