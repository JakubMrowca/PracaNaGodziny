import { Component, OnInit, Input } from '@angular/core';
import { WorkerVm } from '../worker/vm/WorkerVm';

@Component({
  selector: 'app-worker-card',
  templateUrl: './worker-card.component.html',
  styleUrls: ['./worker-card.component.css']
})
export class WorkerCardComponent implements OnInit {
  @Input() worker:WorkerVm
  @Input() isInLocation = false
  constructor() { }

  ngOnInit() {
    
  }

}
