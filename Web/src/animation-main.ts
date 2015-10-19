import config from 'authConfig';

export function configure(aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();

    aurelia.use.plugin('aurelia-animator-css');
    aurelia.use.plugin('aurelia-auth', (baseConfig) => {
        baseConfig.configure(config);
    });

    aurelia.start().then(a => a.setRoot());
}
