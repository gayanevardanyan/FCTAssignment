import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html'
})
export class ModalComponent implements OnInit {

    @Input() modal_title;
    @Input() modal_content;

    constructor(public activeModal: NgbActiveModal) { }

    ngOnInit() {
    }

}
