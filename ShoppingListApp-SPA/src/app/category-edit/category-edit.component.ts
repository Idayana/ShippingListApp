import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {
  @Input() category: Category;
  editForm: FormGroup;
  @Output() cancelAddition = new EventEmitter();

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
    /*this.route.data.subscribe(data => {
      this.category = data.category;
    });*/
    this.createEditForm();
  }

  createEditForm() {
    this.editForm = this.fb.group({
      categoryName: new FormControl(this.category.categoryName, Validators.required),
    });
  }

  updateCategory() {
    this.categoryService.updateCategory(this.category.categoryId, this.category).subscribe(next => {
      this.alertify.success('Category updated successfully');
      this.editForm.reset();
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.cancelAddition.emit(false);
  }

}
