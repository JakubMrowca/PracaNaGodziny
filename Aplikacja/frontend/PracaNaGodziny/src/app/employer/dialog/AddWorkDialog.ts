//Dialog wykorzystywany do dodawania i edycji uzywa formbuildera
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { LocationVm } from '../../client/vm/LocationVm';
import { ApplicationState } from '../../state/ApplicationState';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FormControl } from '@angular/forms';

@Component({
    template: `
  <form [formGroup]="form" (ngSubmit)="submit(form)">
  <h1 mat-dialog-title>Add file</h1>
  <mat-dialog-content>
  <mat-form-field class="example-full-width">
    <input type="text" placeholder="Pick one" aria-label="Number" name="location" matInput [formControl]="myControl" [matAutocomplete]="auto" [(ngModel)]="locationName">
    <mat-autocomplete #auto="matAutocomplete">
      <mat-option *ngFor="let option of filteredOptions | async" [value]="option.name">
        {{ option.name }}
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

    myControl: FormControl = new FormControl();
    form: FormGroup;
    locations: Array<LocationVm>;
    locationId: string;
    filteredOptions: Observable<LocationVm[]>;
    locationName;


    constructor(
        private formBuilder: FormBuilder,
        private appState: ApplicationState,
        private dialogRef: MatDialogRef<AddWorkDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.locations = appState.Locations;
    }

    ngOnInit() {
        this.filteredOptions = this.myControl.valueChanges
            .pipe(
                startWith(''),
                map(val => this.filter(val))
            );

        this.form = this.formBuilder.group({
            hours: 0,
            rate: this.data.rate,
            locationName: "",
            address: "",
            date: new Date()
        });
    }

    filter(val: string): LocationVm[] {
        return this.locations.filter(option =>
            option.name.toLowerCase().includes(val.toLowerCase()));
    }
    //zwraca wynik do miejsca gdzie zostalo otworzone
    submit(form) {
        var loc:LocationVm;
        for(let i =0; i < this.locations.length; i++){
            if(this.locations[i].name.toLowerCase() === this.locationName.toLowerCase().trim()){
                loc = this.locations[i];
                break;
            }
        }
        this.locationId = loc != null ? loc.id : null;

        var result = {
            locationName: this.locationId == null ? this.locationName : null,
            hours: form.value.hours,
            date: form.value.date,
            rate: form.value.rate,
            locationId: this.locationId
            //  phone: form.value.phone,
        }
         this.dialogRef.close(result);
    }

    setLocation(location: LocationVm) {
        this.locationId = location.id;
    }

}