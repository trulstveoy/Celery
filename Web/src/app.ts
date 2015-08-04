import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';

export class App {    
    router:any;

    configureRouter(config, router) {
        config.title = 'Celery';
        config.map([
            { route: 'login', moduleId: './login', nav: false, title: 'Login' },
            { route: 'logout', moduleId: './logout', nav: false, title: 'Logout' },
            { route: ['', 'search'], moduleId: './search', nav: true, title: 'Search' },
            { route: ['admin'], moduleId: './admin', nav: true, title: 'Admin' }
        ]);

        this.router = router;
    }
}
