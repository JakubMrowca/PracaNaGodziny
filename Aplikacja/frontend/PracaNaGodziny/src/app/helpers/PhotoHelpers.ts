import { Injectable } from "@angular/core";
import { UserVm } from "../login/vm/userVm";

@Injectable()
export class PhotoHelpers {
    constructor() {

    }
    SetPhoto() {

    }
    AddPhoto() {
        return "string";
    }
    GetPhoto(photo) {
        return "url('data:image/png;base64," + photo + "')";
    }

}