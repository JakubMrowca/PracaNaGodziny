export class LocationVm{
    id:string
    name:string
    address:string
    paidHour:number
    totalHour:number
    unpaidHour:number
    wage:number
    photo:any

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