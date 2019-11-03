import { Component,  OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { first } from 'rxjs/operators';
import { AlertService, ProductService } from '../services';
import { Product } from '../models'
import { ModalComponent } from '../modal';


@Component({
  selector: 'app-purchase',
    templateUrl: './purchase.component.html'
})
export class PurchaseComponent implements OnInit {
    public product: Product;
    public purchases: any[];
    modalOptions: NgbModalOptions;

    constructor(
        private router: Router, 
        private route: ActivatedRoute,
        private alertService: AlertService,
        private productService: ProductService,
        private modalService: NgbModal) {

        this.productService.getAllPurchases()
            .subscribe(
                data => {
                    this.purchases = data;
                },
                error => {
                    this.alertService.error(error);
                });
    }

    ngOnInit() {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
            
            this.productService.getProduct(id)
                .subscribe(
                    data => {
                        this.product = data;
                    },
                    error => {
                        this.alertService.error(error);
                    });
        }
    }

    order() {
        this.productService.purchase(this.product)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Purchase successful', true);
                    this.openModal();
                    this.router.navigate(['/listproduct']);
                },
                error => {
                    this.alertService.error(error);
                });
    }

    cancel() {
        this.router.navigate(['/listproduct']);
    }

    openModal() {
        const modalRef = this.modalService.open(ModalComponent);
        modalRef.componentInstance.modal_title = 'Purchase';
        modalRef.componentInstance.modal_content = 'Thank you for your purchase. Successfully comleted the order.';
    }
}
