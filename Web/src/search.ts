import {computedFrom} from 'aurelia-framework';
import {bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

export class Search {
    @bindable query:string;

    heading = 'Search for food!';
    http: HttpClient;
    foods: Data.Food;

    static inject = [HttpClient];
    constructor(http: HttpClient) {
        this.http = http;
    }

    queryChanged() {
        this.http.get(`api/food/?query=${this.query}`).then(result => {
            this.foods = JSON.parse(result.response);
        });
    }
}
