import { Component, OnInit } from '@angular/core';
import { LocationVm } from '../client/vm/LocationVm';
import { ApplicationState } from '../state/ApplicationState';
import { LocationWebApi } from './services/LocationWebApi';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {

  location:LocationVm
  loadedData = false;
  constructor(public appState:ApplicationState,public locationApi:LocationWebApi ) {
    this.location = appState.SelectedLocation;
    this.loadWorker();
   }

   loadWorker(){
    this.loadedData = true;
    this.locationApi.getLocation(this.location.id)
      .subscribe((data: LocationVm) => {
        this.location = data;
        this.loadedData = false;
        console.log(data);
      });
  }
  ngOnInit() {
  }

}
