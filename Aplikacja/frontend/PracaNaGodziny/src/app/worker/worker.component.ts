import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoutingEnum } from '../state/RoutingEnum';
import { ApplicationState } from '../state/ApplicationState';
import { WorkerVm } from './vm/WorkerVm';
import { MatTableDataSource, MatSort, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AddWorkDialog } from '../employer/dialog/AddWorkDialog';
import { AddWorkForWorkerCommand } from '../employer/commands/AddWorkForWorkerCommand';
import { WebApiEmployers } from '../employer/services/WebApiEmployers';
import { WebApiWorkers } from './services/WebApiWorkers';

@Component({
  selector: 'app-worker',
  templateUrl: './worker.component.html',
  styleUrls: ['./worker.component.css']
})
export class WorkerComponent implements OnInit {

  loadedData = true;
  worker:WorkerVm;
  addWorkDialog: MatDialogRef<AddWorkDialog>;
  constructor(private employerApi:WebApiEmployers, private workerApi:WebApiWorkers, private router:Router,public dialog: MatDialog,private appState:ApplicationState) {
    this.worker = appState.SelectedWorker;
    this.loadWorker();
   }

  ngOnInit() {
  }

  addWork(){
    this.addWorkDialog = this.dialog.open(AddWorkDialog, {
      hasBackdrop: false,
      width: "300px",
      data:{rate:15}
    });

    this.addWorkDialog
      .afterClosed()
      .subscribe(result => {
        if (result != "") {
          var command = new AddWorkForWorkerCommand(result);
          command.employerId = this.appState.LoggedUser.employerId;
          command.workerId = this.worker.id;
          this.loadedData = true;
          this.employerApi.addWork(command).subscribe(data => {
            this.loadWorker();
          });
        }
      });
  }

  loadWorker(){
    this.loadedData = true;
    this.workerApi.getWorker(this.worker.id)
      .subscribe((data: WorkerVm) => {
        this.worker = data;
        this.loadedData = false;
        console.log(data);
      });
  }

  back(){
    this.appState.SelectedWorker = null;
    this.router.navigate([RoutingEnum.employer]);
  }
}
