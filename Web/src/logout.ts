import {AuthService} from 'paulvanbladel/aurelia-auth';
import {inject} from 'aurelia-framework';

export class Logout {
    auth;

    static inject = [AuthService];
    constructor(auth) {
        this.auth = auth;
    }

    activate() {
        this.auth.logout('#/login')
            .then(response=> {
                console.log('ok logged out on  logout.js');
            })
            .catch(err=> {
                console.log('error logged out  logout.js');
            });
    }
}