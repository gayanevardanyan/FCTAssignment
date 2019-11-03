import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { AuthenticationService } from '../services';
import { Product } from '../models';

@Injectable({ providedIn: 'root' })
export class ProductService {
    private url: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
        private authenticationService: AuthenticationService) {
        this.url = baseUrl;
    }

    getAllProducts() {
        const headers = new HttpHeaders();
        if (this.authenticationService.isUserLoggedIn && this.authenticationService.currentUserValue.token) {
            headers.set('Authorization', `Bearer ${this.authenticationService.currentUserValue.token}`);
        }
        return this.http.get<Product[]>(this.url + 'product', { headers: headers });
    }

    getAllPurchases() {
        const headers = new HttpHeaders();
        if (this.authenticationService.isUserLoggedIn && this.authenticationService.currentUserValue.token) {
            headers.set('Authorization', `Bearer ${this.authenticationService.currentUserValue.token}`);
        }
        return this.http.get<any[]>(this.url + 'product/purchases', { headers: headers });
    }

    getProduct(id) {
        const headers = new HttpHeaders();
        if (this.authenticationService.isUserLoggedIn && this.authenticationService.currentUserValue.token) {
            headers.set('Authorization', `Bearer ${this.authenticationService.currentUserValue.token}`);
        }
        return this.http.get<Product>(this.url + 'product/' + id, { headers: headers });
    }

    purchase(product: Product) {
        const headers = new HttpHeaders();
        if (this.authenticationService.isUserLoggedIn && this.authenticationService.currentUserValue.token) {
            headers.set('Authorization', `Bearer ${this.authenticationService.currentUserValue.token}`);
        }
        return this.http.post(this.url + 'product/purchase', product, { headers: headers });
    }

}
