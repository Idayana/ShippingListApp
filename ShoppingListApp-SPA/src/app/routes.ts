import { Routes } from '@angular/router';
import { HomeComponent } from '../app/home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { ListsResolver } from '../app/_resolvers/lists.resolver';
import { CategoryComponent } from './category/category.component';
import {ProductListComponent } from './products/product-list/product-list.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';
import { ProductAddComponent } from './products/product-add/product-add.component';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'categories', component: CategoryListComponent, resolve: {categories: ListsResolver}},
    {path: 'category', component: CategoryComponent},
    {path: 'product', component: ProductAddComponent},
    {path: 'products', component: ProductListComponent, resolve: {products: ProductListResolver}},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
