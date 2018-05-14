import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ApplicationState } from './ApplicationState';

@Injectable()
export class IsAuthorize implements CanActivate {
  constructor(private appState: ApplicationState) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      return this.appState.IsAuthorize;
  }
}