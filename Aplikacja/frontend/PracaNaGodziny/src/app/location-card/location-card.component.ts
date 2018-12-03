import { Component, OnInit, Input } from '@angular/core';
import { LocationVm } from '../client/vm/LocationVm';

@Component({
  selector: 'app-location-card',
  templateUrl: './location-card.component.html',
  styleUrls: ['./location-card.component.css']
})
export class LocationCardComponent implements OnInit {
  @Input() location:LocationVm

  constructor() { }

  ngOnInit() {
  }

}
