import {AuthService} from 'aurelia-auth';

export class Profile{
    auth;
    user:Data.User;

    static inject = [AuthService];
    constructor(auth) {
        this.auth = auth;
    }

    activate() {
        this.auth.getMe().then(data => {
            this.user = data;
        });
    }
    
}