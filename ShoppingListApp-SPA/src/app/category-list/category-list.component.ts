import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  categories: Category[];
  pagination: Pagination;
  showForm = false;
  modalRef: BsModalRef;
  additionMode = false;

  constructor(
    public categoryService: CategoryService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private modalService: BsModalService
    ) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.categories = data.categories.result;
      this.pagination = data.categories.pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<Category[]>) => {
      this.categories = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  /*openModalWithComponent() {
    this.showForm = !this.showForm;
    console.log(this.showForm);
  }*/

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  cancelAdditionMode() {
    this.modalRef.hide();
  }

  deleteCategory(id: number) {
    this.alertify.confirm('Are you sure you want to delete this category', () => {
      console.log(id);
      this.categoryService.deleteCategory(id).subscribe(() => {
        this.categories.splice(this.categories.findIndex(m => m.categoryId === id), 1);
        this.alertify.success('Message has been deleted');
      }, error => {
        this.alertify.error('Failed to delete the message');
      });
    });
  }
}

