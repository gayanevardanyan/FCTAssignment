import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AlertService, ProductService } from '../services';
import { Sorter } from '../helpers';
import { Product } from '../models'

@Component({
  selector: 'app-product-data',
    templateUrl: './product.component.html'
})
export class ProductComponent implements OnInit {
    public products: Product[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string,
        private productService: ProductService,
        private alertService: AlertService,
        private sorter: Sorter,
        public router: Router) {

        this.productService.getAllProducts()
            .subscribe(
                data => {
                    this.products = data;
                },
                error => {
                    this.alertService.error(error);
                });
    }

    ngOnInit() {
    }

    sort(prop: string) {
        this.sorter.sort(this.products, prop);
    }
}

