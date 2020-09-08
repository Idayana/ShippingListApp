import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Category } from '../_models/category';
import { PaginatedResult } from '../_models/pagination';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

// tslint:disable-next-line: typedef
addCategory(category: Category)
{
  return this.http.post(this.baseUrl + 'categories', category);
}

getCategories(page?, itemsPerPage?): Observable<PaginatedResult<Category[]>> {
  const paginatedResult: PaginatedResult<Category[]> = new PaginatedResult<Category[]>();
  let params = new HttpParams();
  if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
  return this.http.get<Category[]>(this.baseUrl + 'categories', {observe: 'response', params}).pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}

getCategory(id): Observable<Category> {
  return this.http.get<Category>(this.baseUrl + 'categories/' + id);
}

updateCategory(id: number, cat: Category) {
  return this.http.put(this.baseUrl + 'categories/' + id, cat);
}

deleteCategory(categoryId: number) {
  return this.http.delete(this.baseUrl + 'categories/' + categoryId, {});
}

getAllCategories(): Observable<Category[]>
{
  return this.http.get<Category[]>(this.baseUrl + 'categories/allCategories');
}

addProduct(product: Product)
{
  return this.http.post(this.baseUrl + 'products', product);
}

getProducts(page?: number, itemsPerPage?: number, prodParams?: any): Observable<PaginatedResult<Product[]>> {
  const paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();
  let params = new HttpParams();
  if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

  // tslint:disable-next-line: forin
  for (const p in prodParams) {
       if (prodParams[p] != null && prodParams[p] !== '') {
         params = params.append(p, prodParams[p]);
       }
    }

  return this.http.get<Product[]>(this.baseUrl + 'products', {observe: 'response', params}).pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}

getProduct(id): Observable<Product> {
  return this.http.get<Product>(this.baseUrl + 'products/' + id);
}

getProductByName(name: string, page?, itemsPerPage?): Observable<PaginatedResult<Product[]>> {
  const paginatedResult: PaginatedResult<Product[]> = new PaginatedResult<Product[]>();
  let params = new HttpParams();
  if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
  return this.http.get<Product[]>(this.baseUrl + 'products/productByName/' + name, {observe: 'response', params}).pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
    })
  );
}

getProductsByCategory(id: number) {
  return this.http.get<Product[]>(this.baseUrl + 'products/category' + id);
}

updateProduct(id: number, prod: Product) {
  return this.http.put(this.baseUrl + 'products/' + id, prod);
}

deleteProduct(productId: number) {
  return this.http.delete(this.baseUrl + 'products/' + productId, {});
}

}
