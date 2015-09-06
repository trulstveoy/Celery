var configForDevelopment = {
    authHeader: 'Authorization',
    authToken: 'Bearer',
    loginRedirect: '/#register',
    profileUrl: '/api/auth/me',

    providers: {
        google: {
            clientId: '778108841247-tt5cm23c8ntepi8dessrussvbr61qri9.apps.googleusercontent.com',
            url: '/api/auth/google'
        }
    }
};

var configForProduction = {
    authHeader: 'Authorization',
    authToken: 'Bearer',
    loginRedirect: '/#register',
    profileUrl: '/api/auth/me',

    providers: {
        google: {
            clientId: '778108841247-tnp9dqvgvrucrag02fb8f9umc6h76fn0.apps.googleusercontent.com',
            url: '/api/auth/google'
        }
    }
};

var config;
if (window.location.hostname === 'localhost') {
    config = configForDevelopment;
}
else {
    config = configForProduction;
}

export default config;