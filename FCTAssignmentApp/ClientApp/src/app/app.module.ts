import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';
import { PurchaseComponent } from './purchase/purchase.component';
import { LoginComponent } from './login';
import { ModalComponent } from './modal';
import { RegisterComponent } from './register';
import { AuthGuard } from './helpers';
import { AlertComponent } from './components';
import { JwtInterceptor, ErrorInterceptor } from './helpers';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
        LoginComponent,
        RegisterComponent,
        AlertComponent,
        ProductComponent,
        PurchaseComponent,
        ModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
      FormsModule,
      NgbModule,
      ReactiveFormsModule,
    RouterModule.forRoot([
        { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard]  },
        { path: 'login', component: LoginComponent },
        { path: 'register', component: RegisterComponent },
        { path: 'listproduct', component: ProductComponent, canActivate: [AuthGuard] },
        { path: 'listproduct/:id', component: PurchaseComponent, canActivate: [AuthGuard] }
    ])
    ],
    entryComponents: [
        ModalComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
