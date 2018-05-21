export class AddWorkForWorkerCommand{
    locationId:string
    workerId:string
    locationName:string
    rate:number
    employerId:string
    workDate:Date
    hours:number
    additionalHours:number
    additionalRate:number

    constructor(result:any){
        this.locationId = result.locationId;
        this.locationName = result.locationName;
        this.workDate = result.date;
        this.rate = result.rate;
        this.hours = result.hours;
    }
}