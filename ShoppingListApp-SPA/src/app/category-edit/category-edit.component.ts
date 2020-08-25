import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Category } from '../_models/category';
import { CategoryService } from '../_services/category.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {
  category: Category;
  editForm: FormGroup;
  @Output() cancelAddition = new EventEmitter();

  constructor(private categoryService: CategoryService,
              private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.category = data.category;
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
