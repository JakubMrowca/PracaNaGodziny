import { LocationVm } from "../../client/vm/LocationVm";

export class WorkerVm{ 
        id:string
        firstName:string
        lastName:string
        address:string
        works:any
        paidHour:number
        totalHour:number
        unpaidHour:number
        wage:number
        photo:any

        Locations:LocationVm
        totalHourInThisMonth:number
        totalHourInThisWeek:number
        paidHourInThisMonth:number
        paidHourInThisWeek:number
        //
        additionalHourInThisMonth:number
        additionalWageInThisMonth:number
        additionalHourInThisWeek:number
        additionalWageInThisWeek:number

        //
        wageInThisMonth:number
        wageInThisWeek:number

        //
        totalHourInLastMonth:number
}