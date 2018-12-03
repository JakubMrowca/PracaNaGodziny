import {IEvent} from '../../state/IEvent'
export class UserAuthorized implements IEvent{
    id: string;
    employerId: string
    employerName: string;
    employerAddress: string;
    workerId: string;
    workerName: string;
    workerAddress: string;
    photo:any;
}