require.config({
    baseUrl: "../",
    waitSeconds: 0,
    paths: {
        "jquery": "Scripts/jquery-3.1.1",
        "knockout": "Scripts/knockout-3.4.2.debug",
        "komapping": "Scripts/knockout.mapping-latest.debug",
        "text": "Scripts/text",
        "jqueryval": "Scripts/jquery.validate",
        "bootstrap": "Scripts/bootstrap.min",

        "repository": "App/repository"
    },
    shim: {
        "komapping": {
            deps: ['knockout'],
            exports: 'komapping'
        },
        "knockout": {
            deps: ['bootstrap']
        },
        "bootstrap": {
            deps: ['jquery']
        },
        "repository": {
            deps: ['jquery']
        }
    }
});
