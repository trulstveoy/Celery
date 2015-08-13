//import 'jquery';
//import 'bootstrap-sass';
import './style.css!';
import {AuthorizeStep} from 'paulvanbladel/aurelia-auth';
import HttpClientConfig from 'paulvanbladel/aurelia-auth/app.httpClient.config';

export class App {    
    router:any;
    httpClientConfig;

    static inject = [HttpClientConfig];
    constructor(httpClientConfig, exceptionless) {
        this.httpClientConfig = httpClientConfig;
    }

    activate() {
        this.httpClientConfig.configure();
    }
    
    configureRouter(config, router, httpClient) {
        config.title = 'Celery';
        config.addPipelineStep('authorize', AuthorizeStep);
        config.map([
        { route: 'login', moduleId: './login', nav: false, title: 'Login' },
        { route: 'logout', moduleId: './logout', nav: false, title: 'Logout' },
        { route: ['', 'register'], moduleId: './register', nav: true, title: 'Register', auth: true },
        { route: ['admin'], moduleId: './admin', nav: true, title: 'Admin', auth: true },
        { route: ['profile'], moduleId: './profile', nav: true, title: 'Profile', auth: true }
        ]);

        this.router = router;
    }
}
