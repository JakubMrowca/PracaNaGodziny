//Dialog wykorzystywany do dodawania i edycji uzywa formbuildera
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { LocationVm } from '../../client/vm/LocationVm';
import { ApplicationState } from '../../state/ApplicationState';

@Component({
    template: `
  <form [formGroup]="form" (ngSubmit)="submit(form)">
  <h1 mat-dialog-title>Add file</h1>
  <mat-dialog-content>
  <mat-form-field>
    <input type="text" placeholder="Miejsce" aria-label="text" formControlName="locationName" matInput [matAutocomplete]="auto">
    <mat-autocomplete #auto="matAutocomplete">
    <mat-option (onSelect)="setLocation(location)" *ngFor="let location of locations" [value]="location.name">
      {{location.name}}
    </mat-option>
  </mat-autocomplete>
</mat-form-field>
    <mat-form-field>
      <input type="number" matInput formControlName="hours" placeholder="Liczba godzin">
    </mat-form-field>
    <br>
    <mat-form-field>
        <input matInput [matDatepicker]="picker" placeholder="Data" formControlName="date">
        <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
    <mat-datepicker #picker></mat-datepicker>
    </mat-form-field>
    <mat-form-field>
    <input type="number" matInput formControlName="rate" placeholder="Stawka">
  </mat-form-field>
  <br>
  </mat-dialog-content>
  <mat-dialog-actions>
    <button mat-button type="submit">Dodaj</button>
    <button mat-button type="button" mat-dialog-close>Anuluj</button>
  </mat-dialog-actions>
</form>
  `
})
export class AddWorkDialog implements OnInit {

    form: FormGroup;
    locations: Array<LocationVm>;
    locationId:string;

    constructor(
        private formBuilder: FormBuilder,
        private appState: ApplicationState,
        private dialogRef: MatDialogRef<AddWorkDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.locations = appState.Locations;
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            hours: 0,
            rate:this.data.rate,
            locationName: "",
            address: "",
            date:new Date()
        });
    }
    //zwraca wynik do miejsca gdzie zostalo otworzone
    submit(form) {
        var result = {
            locationName: this.locationId == null ? form.value.locationName : null,
            hours: form.value.hours,
            date: form.value.date,
            rate: form.value.rate,
            locationId: this.locationId
            //  phone: form.value.phone,
        }
        this.dialogRef.close(result);
    }

    setLocation(location:LocationVm){
        this.locationId = location.id;
    }

}