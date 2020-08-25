import { Routes } from '@angular/router';
import { HomeComponent } from '../app/home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { ListsResolver } from '../app/_resolvers/lists.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'categories', component: CategoryListComponent, resolve: {categories: ListsResolver}},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
