import { Component, OnInit, Output, EventEmitter, Input, ViewChild, ElementRef } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { CategoryService } from 'src/app/_services/category.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';
import { Category } from 'src/app/_models/category';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css']
})
export class ProductAddComponent implements OnInit {
  @Output() cancelAddition = new EventEmitter();
  @Output() prodCreated = new EventEmitter<Product>();
  product: Product;
  addProductForm: FormGroup;
  @Input() categories: Category[];
  categoriesList: Category[];
  selected: Category;
  @Input() notModal = true;

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private fb: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.createAddProductForm();
    this.getCategories();
  }

  createAddProductForm() {
    this.addProductForm = new FormGroup({
      productName: new FormControl(),
      categoryId: new FormControl()
   });
  }

  addProduct(){
      this.product = Object.assign({}, this.addProductForm.value);
      this.categoryService.addProduct(this.product).subscribe((returnedItem: {
        productId: number, productName: string, categoryName: string
      }) => {
        this.alertify.success('Product added uccessfully');
        this.prodCreated.emit(returnedItem);
        this.addProductForm.reset();
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/products']);
      });
  }

  getCategories() {
    this.categoryService.getAllCategories()
    .subscribe(categories => {
      this.categoriesList = categories;
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    console.log(this.notModal);
    this.cancelAddition.emit(false);
    if (this.notModal) {
      this.router.navigate(['/home']);
      return;
    }
    this.router.navigate(['/products']);
  }

}
