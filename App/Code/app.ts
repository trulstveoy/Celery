import {Router} from 'aurelia-router';

export class App {
	router:Router;

  configureRouter(config, router:Router){
    config.title = 'Celery';
    config.map([
      { route: ['','search'],  moduleId: './search', nav: true, title:'Search' }
    ]);

    this.router = router;
  }
}
