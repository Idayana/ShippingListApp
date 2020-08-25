import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Category } from '../_models/category';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

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

getUser(id): Observable<Category> {
  return this.http.get<Category>(this.baseUrl + 'categories/' + id);
}

updateCategory(id: number, cat: Category) {
  return this.http.put(this.baseUrl + 'categories/' + id, cat);
}

deleteCategory(categoryId: number) {
  return this.http.delete(this.baseUrl + 'categories/' + categoryId, {});
}

}
