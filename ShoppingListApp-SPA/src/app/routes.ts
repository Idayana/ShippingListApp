import { Routes } from '@angular/router';
import { HomeComponent } from '../app/home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { ListsResolver } from '../app/_resolvers/lists.resolver';
import { CategoryComponent } from './category/category.component';
import {ProductListComponent } from './products/product-list/product-list.component';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'categories', component: CategoryListComponent, resolve: {categories: ListsResolver}},
    {path: 'category', component: CategoryComponent},
    {path: 'products', component: ProductListComponent},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
