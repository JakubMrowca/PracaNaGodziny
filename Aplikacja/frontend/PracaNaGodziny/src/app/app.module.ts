import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule, } from '@angular/material';

import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { APP_BASE_HREF } from '@angular/common';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';

import {
  RouterModule,
  Routes
} from '@angular/router';
import { ProfilComponent } from './profil/profil.component';
import { HomeComponent } from './home/home.component';
import { WorkerComponent } from './worker/worker.component';
import { ClientComponent } from './client/client.component';

import {WebApiUsers} from './login/services/WebApiUsers';
import {ApplicationState} from './state/ApplicationState';
import { EmployerComponent } from './employer/employer.component';
import { SelectProfilTypeDialog } from './login/dialog/SelectProfilTypeDialog';
import { IsAuthorize } from './state/IsAuthorize';
import { RoutingEnum } from './state/RoutingEnum';
import { PhotoHelpers } from './helpers/PhotoHelpers';
import{EventService} from './state/EventService';
import { AddWorkerDialog } from './employer/dialog/AddWorkerDialog';
import { WebApiEmployers } from './employer/services/WebApiEmployers';
import { WorkerCardComponent } from './worker-card/worker-card.component';
import { AddWorkDialog } from './employer/dialog/AddWorkDialog';
import { WebApiWorkers } from './worker/services/WebApiWorkers';

const path: Routes = [
  { path: '', redirectTo: RoutingEnum.login, pathMatch: 'full' },
  { path: RoutingEnum.login, component: LoginComponent },
  { path: RoutingEnum.employer, component: EmployerComponent,canActivate:[IsAuthorize]},
  { path: RoutingEnum.worker, component: WorkerComponent,canActivate:[IsAuthorize]}
  
  // { path: 'history/:id', component: HistoryComponent},
]; // okreslenie sciezek routingu

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ProfilComponent,
    HomeComponent,
    WorkerComponent,
    ClientComponent,
    EmployerComponent,
    SelectProfilTypeDialog,
    AddWorkerDialog,
    AddWorkDialog,
    WorkerCardComponent
  ],
  imports: [
    RouterModule.forRoot(path),
    BrowserModule,
    HttpClientModule,
    RouterModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  entryComponents: [SelectProfilTypeDialog,AddWorkerDialog,AddWorkDialog],
  providers: [WebApiUsers,ApplicationState, IsAuthorize, PhotoHelpers,EventService,WebApiEmployers,WebApiWorkers],
  bootstrap: [AppComponent]
})
export class AppModule { }
