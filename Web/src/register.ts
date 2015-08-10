import {computedFrom} from 'aurelia-framework';
import {bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Register {
    @bindable query:string;
    http: HttpClient;
    foods: Data.Food;

    static inject = [HttpClient];
    constructor(http: HttpClient) {
        this.http = http;
    }

    queryChanged() {
        if (this.query === '')
            return;

        this.http.get(`api/food/?query=${this.query}`).then(result => {
            this.foods = JSON.parse(result.response);
        });
    }
}
