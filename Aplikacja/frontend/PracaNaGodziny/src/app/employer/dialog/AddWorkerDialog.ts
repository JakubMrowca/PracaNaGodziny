//Dialog wykorzystywany do dodawania i edycji uzywa formbuildera
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    template: `
  <form [formGroup]="form" (ngSubmit)="submit(form)">
  <h1 mat-dialog-title>Add file</h1>
  <mat-dialog-content>
    <mat-form-field>
      <input matInput formControlName="firstName" placeholder="Imie">
    </mat-form-field>
    <br>
    <mat-form-field>
    <input matInput formControlName="lastName" placeholder="Nazwisko">
  </mat-form-field>
  <br>
  <mat-form-field>
  <input matInput formControlName="address" placeholder="Adres">
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
export class AddWorkerDialog implements OnInit {

    form: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<AddWorkerDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            firstName: "",
            lastName: "",
            address: ""});
    }
    //zwraca wynik do miejsca gdzie zostalo otworzone
    submit(form) {
        var result = {
            firstName: form.value.firstName,
            lastName: form.value.lastName,
            address: form.value.address
            // email: form.value.email,
            // phone: form.value.phone,
        }
        this.dialogRef.close(result);
    }
}