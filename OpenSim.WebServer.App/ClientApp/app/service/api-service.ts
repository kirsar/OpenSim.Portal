import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService {

    constructor(
        private readonly http: HttpClient,
        @Inject("BASE_URL") private readonly baseUrl: string) { }

    get<T>(path: string): Observable<T> {
        return this.http.get<T>(this.baseUrl + "api/v1/" + path);
    }
}