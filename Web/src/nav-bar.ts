import {bindable } from 'aurelia-framework';
import {inject} from 'aurelia-framework';
import {AuthService} from 'paulvanbladel/aurelia-auth';

export class NavBar {
    auth;
    @bindable router = null;
    profile;

    static inject = [AuthService];
    constructor(auth) {
        this.auth = auth;

        this.auth.getMe().then(data => {
            this.profile = data;
        });
    }

    get isAuthenticated() {
        return this.auth.isAuthenticated();
    }

    get name() {
        var a = this.profile;
        return 'foo';
    }
}
