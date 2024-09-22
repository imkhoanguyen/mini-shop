import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { Category } from '../../../../_models/category.module';
import { CategoryService } from '../../../../_services/category.service';

@Component({
  selector: 'app-categorylist',
  standalone: true,
  imports: [
    ButtonModule,
    CheckboxModule,
    CommonModule,
    TableModule,],
  templateUrl: './categorylist.component.html',
  styleUrl: './categorylist.component.css'
})
export class CategorylistComponent {
  categories: Category[] = [];
  selectedCategory: Category[] = [];

  constructor(public categoryService: CategoryService) {}

  ngOnInit(): void {

    this.categoryService.getAllCategories();
    this.categories = this.categoryService.categoryList();
    this.selectedCategory = [this.categoryService.categoryItems()];
  }

}
