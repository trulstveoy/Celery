import {HttpClient} from 'aurelia-http-client';

export class Stuffs {
    http: HttpClient;

    static inject = [HttpClient];
    constructor(http: HttpClient) {
        this.http = http;
    }

    repopulate() {
        this.http.get('api/stuffs/throwexception').then(() => {
            //alert('done');
        });
    }
}