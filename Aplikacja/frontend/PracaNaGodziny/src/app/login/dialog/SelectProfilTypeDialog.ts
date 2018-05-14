
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  template: `
  <h1 style="text-align:center" mat-dialog-title>Wybierz profil</h1>
  <br/>
    <button class="bigButton" mat-raised-button color="warn" (click)="select('worker')" type="button">Pracodawca</button>
    <br/>
    <button class="bigButton" mat-raised-button color="primary" (click)="select('employer')" type="button">Pracownik</button>
  `
})

export class SelectProfilTypeDialog implements OnInit {

    isEmployer:boolean = false;
    isWorker:boolean = false;

    constructor(
      private dialogRef: MatDialogRef<SelectProfilTypeDialog>,
      @Inject(MAT_DIALOG_DATA) private data
    ) {}
  
    ngOnInit() {
      
    }

    select(chose){
        if(chose === "employer")
            this.isEmployer = true;
        else
            this.isWorker = true;

        this.submit();
    }
  
    submit() {
      var result = {
        isEmployer:this.isEmployer,
        isWorker:this.isWorker
      }
      this.dialogRef.close(result);
    }
}