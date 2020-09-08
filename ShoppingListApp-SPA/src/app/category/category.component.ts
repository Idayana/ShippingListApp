import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  @Output() cancelAddition = new EventEmitter();
  @Output() catCreated = new EventEmitter<Category>();
  category: Category;
  addCategoryForm: FormGroup;
  @Input() notModal = true;

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private fb: FormBuilder,
              private router: Router) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.createAddCategoryForm();
  }

  // tslint:disable-next-line: typedef
  createAddCategoryForm() {
    this.addCategoryForm = this.fb.group({
      categoryName: ['', Validators.required],
    });
  }

  // tslint:disable-next-line: typedef
  addCategory(){
    if (this.addCategoryForm.valid)
    {
      this.category = Object.assign({}, this.addCategoryForm.value);
      this.categoryService.addCategory(this.category).subscribe((returnedItem: {
        categoryId: number, categoryName: string
      }) => {
        this.alertify.success('Category added uccessfully');
        this.catCreated.emit(returnedItem);
        this.addCategoryForm.reset();
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/categories']);
      });
    }
  }

  cancel() {
    this.cancelAddition.emit(false);
    if (this.notModal) {
      this.router.navigate(['/home']);
      return;
    }
    this.router.navigate(['/categories']);
  }

}
