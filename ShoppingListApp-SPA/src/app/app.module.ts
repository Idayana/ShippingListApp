import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ErrorInterceptorProvider } from '../app/_services/error.interceptor';
import { AlertifyService } from '../app/_services/alertify.service';
import {CategoryService } from '../app/_services/category.service';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';
import { ListsResolver } from './_resolvers/lists.resolver';
import { CategoryComponent } from './category/category.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryEditComponent } from './category-edit/category-edit.component';
import { ProductAddComponent } from './products/product-add/product-add.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';

@NgModule({
  declarations: [
    AppComponent,
      NavComponent,
      HomeComponent,
      CategoryComponent,
      CategoryListComponent,
      CategoryEditComponent,
      ProductAddComponent,
      ProductListComponent,
      ProductEditComponent

   ],
  imports: [
    HttpClientModule,
    ModalModule.forRoot(),
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes),
    PaginationModule.forRoot(),
  ],
  providers: [
    ErrorInterceptorProvider,
    AlertifyService,
    CategoryService,
    ListsResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
