import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';
import {AuthorizeStep} from 'paulvanbladel/aurelia-auth';

export class App {    
    router:any;

    configureRouter(config, router) {
        config.title = 'Celery';
        config.addPipelineStep('authorize', AuthorizeStep);
        config.map([
        { route: 'login', moduleId: './login', nav: false, title: 'Login' },
        { route: 'logout', moduleId: './logout', nav: false, title: 'Logout' },
        { route: ['', 'search'], moduleId: './search', nav: true, title: 'Search', auth: true },
        { route: ['admin'], moduleId: './admin', nav: true, title: 'Admin', auth: true }
        ]);

        this.router = router;
    }
}
