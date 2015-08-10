import {bindable } from 'aurelia-framework';
import {inject} from 'aurelia-framework';
import {AuthService} from 'paulvanbladel/aurelia-auth';

export class NavBar {
    auth;
    @bindable router = null;
    
    static inject = [AuthService];
    constructor(auth) {
        this.auth = auth;
    }

    get isAuthenticated() {
        return this.auth.isAuthenticated();
    }
}
