import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/_services/category.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/_models/category';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  @Input() product: Product;
  @Input() categories: Category[];
  editForm: FormGroup;
  @Output() cancelAddition = new EventEmitter();
  @Output() prodModified = new EventEmitter<Product>();

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
    this.createEditForm();
  }

  createEditForm() {
    this.editForm = this.fb.group({
      productName: new FormControl(this.product.productName, Validators.required),
      categoryId: new FormControl(this.product.categoryId),
      // categoryName: new FormControl(this.product.categoryName)
    });
  }

  updateProduct() {
    this.categoryService.updateProduct(this.product.productId, this.product).subscribe((returnedItem: {
      productId: number, productName: string, categoryName: string
    }) => {
      this.alertify.success('Product updated successfully');
      this.prodModified.emit(returnedItem);
      this.editForm.reset();
      this.cancelAddition.emit(false);
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.cancelAddition.emit(false);
  }


}
