import { Component } from '@angular/core';
import { ApplicationState } from './state/ApplicationState';
import { UserVm } from './login/vm/userVm';
import { Router } from '@angular/router';
import { RoutingEnum } from './state/RoutingEnum';
import {ElementRef,Renderer2} from '@angular/core';
import { PhotoHelpers } from './helpers/PhotoHelpers';
import { WebApiUsers } from './login/services/WebApiUsers';
import { AddPhotoForEmployerCommand } from './employer/commands/AddPhotoForEmployerCommand';
import { Observable } from 'rxjs';
import { Subscription } from 'rxjs/Subscription';
import { EventService } from './state/EventService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  employerName:string;
  subscription:Subscription;
  el:ElementRef;
  command:AddPhotoForEmployerCommand;
  constructor(private appState:ApplicationState, private router:Router, private photoHelper:PhotoHelpers,private userApi:WebApiUsers,private eventService:EventService){
    this.command = new AddPhotoForEmployerCommand();
    this.subscription = this.eventService.getMessage().subscribe(message => {
      this.changePhoto()
    });
  }

  checkUser():boolean{
    if(this.appState.IsAuthorize){
      this.employerName = this.appState.GetEmployerName();
      return true;
    }
    return false;
  }

  logout(){
    this.appState.Logout()
    this.router.navigate([RoutingEnum.login]);
  }

  changePhoto(){
    var photo = document.getElementById("Photo");
    photo.style.backgroundImage = this.photoHelper.GetPhoto(this.appState.LoggedUser.photo);
    this.photoHelper.AddPhoto();
  }

  addPhoto(){
    this.command.employerId = this.appState.LoggedUser.employerId;
    this.command.photo = this.photoHelper.AddPhoto();
    this.userApi.addPhotoForEmployer(this.command).subscribe(data => {
    }
     );
  }

}
