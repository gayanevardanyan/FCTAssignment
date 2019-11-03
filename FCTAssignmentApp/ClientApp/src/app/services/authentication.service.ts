import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private url: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
        this.loggedIn.next(true);
        this.url = baseUrl;
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(email, password): Observable<any> {
        return this.http.post<any>(this.url+'Customer/authenticate', { email, password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                this.loggedIn.next(true);
                return user;
            }));
    }

    get isUserLoggedIn() {
        return this.loggedIn.asObservable();
    }

    get loggedInUser() {
        return this.currentUserSubject.asObservable();
    }
    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        this.loggedIn.next(false);
    }
}
