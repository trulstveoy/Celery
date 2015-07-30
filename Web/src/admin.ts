import {HttpClient} from 'aurelia-http-client';

export class Admin {
    http: HttpClient;

    static inject = [HttpClient];
    constructor(http: HttpClient) {
        this.http = http;
    }

    repopulate() {
        this.http.put('api/admin/repopulate', null).then(() => {
            alert('done');
        });
    }
}