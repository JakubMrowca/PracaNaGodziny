export class AddWorkerForEmployerCommand{
    employerId:string
    firstName:string
    lastName:string
    address:string
    userId:string

    constructor(result:any){
        this.firstName = result.firstName;
        this.lastName = result.lastName;
        this.address = result.address;
    }
}