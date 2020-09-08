import { Component, OnInit, TemplateRef } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { CategoryService } from 'src/app/_services/category.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Category } from 'src/app/_models/category';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[];
  pagination: Pagination;
  product: Product | null = null;
  prodsByName: Product[];
  modalRef: BsModalRef;
  additionMode = false;
  categories: Category[];
  findProductForm: FormGroup;
  constructor(public categoryService: CategoryService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private modalService: BsModalService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.products = data.products.result;
      this.pagination = data.products.pagination;
    });

    this.getCategories();
    this.createFindProductForm();
  }

  createFindProductForm() {
    this.findProductForm = new FormGroup({
      productName: new FormControl(''),
      categoryId: new FormControl('')
   });
  }

  prodCreated(prod: Product){
    this.products =  [prod, ...this.products];
 }

 prodModified(prod: Product){
  // this.products =  [prod, ...this.products];
   const prodMod = this.products.find(p => p.productId === prod.productId);
   prodMod.categoryName = prod.categoryName;
   if ( prodMod == null) {
    this.alertify.error('Product not found');
   }
   // return prodMod;
}

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadProducts();
  }

  setItemsPerPage(event: any) {
    this.pagination.itemsPerPage = event.target.value;
    this.loadProducts();
  }

  loadProducts() {

    this.categoryService.getProducts(this.pagination.currentPage, this.pagination.itemsPerPage, this.findProductForm.value)
    .subscribe((res: PaginatedResult<Product[]>) => {
      this.products = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  productByName() {
    const name: string = this.findProductForm.value.productName;
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 5;
    console.log(name);
    if (name != null && name !== '')
    {
      this.categoryService.getProductByName(name, this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<Product[]>) => {
      this.products = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
    }
    else {
      this.loadProducts();
    }
  }

  getCategories() {
    this.categoryService.getAllCategories()
    .subscribe(categories => {
      this.categories = categories;
    }, error => {
      this.alertify.error(error);
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  edit(product: Product, template: TemplateRef<any>) {
    this.product = product;
    this.openModal(template);
  }

  cancelAdditionMode() {
    this.modalRef.hide();
  }

  deleteProduct(id: number) {
    this.alertify.confirm('Are you sure you want to delete this product?', () => {
      this.categoryService.deleteProduct(id).subscribe(() => {
        this.products.slice(this.products.findIndex(p => p.productId === id), 1);
        this.alertify.success('Product has been deleted');
        this.loadProducts();
      }, error => {
        this.alertify.error('Failed to delete the product');
      });
    });
  }

}
