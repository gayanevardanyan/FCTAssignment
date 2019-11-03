import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../models';

@Injectable({ providedIn: 'root' })
export class UserService {
    private url: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
    }

    getAll() {
        return this.http.get<User[]>(this.url +'Customer');
    }

    register(user: User) {
        return this.http.post(this.url+'Customer/register', user);
    }

}
