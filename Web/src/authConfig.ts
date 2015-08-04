var configForDevelopment = {
    providers: {
        google: {
            clientId: '778108841247-tnp9dqvgvrucrag02fb8f9umc6h76fn0.apps.googleusercontent.com'
        }
    }
};

var configForProduction = {
    providers: {
        google: {
            clientId: '239531826023-3ludu3934rmcra3oqscc1gid3l9o497i.apps.googleusercontent.com'
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