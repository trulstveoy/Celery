﻿var configForDevelopment = {

    loginRedirect: '/#search',

    providers: {
        google: {
            clientId: '778108841247-tnp9dqvgvrucrag02fb8f9umc6h76fn0.apps.googleusercontent.com',
            url: '/api/auth/google'
        }
    }
};

var configForProduction = {
    loginRedirect: '/#search',

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