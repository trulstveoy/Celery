import {AuthService} from 'paulvanbladel/aurelia-auth';
import {inject} from 'aurelia-framework';

export class Login {
    auth;
    heading = 'Login';
    email = '';
    password = '';

    static inject = [AuthService];
    constructor(auth) {
        this.auth = auth;
    }

    
    login() {
        return this.auth.login(this.email, this.password)
            .then(response=> {
                console.log('success logged ' + response);
            })
            .catch(err=> {
                console.log('login failure');
            });
    }

    authenticate(name) {
        return this.auth.authenticate(name, false, null)
            .then((response) => {
                console.log('auth response ' + response);
            });

    }
}