import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { Category } from '../../../_models/category.module';
import { CategoryService } from '../../../_services/category.service';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [
    DropdownModule,
    ButtonModule,
    CheckboxModule,
    CommonModule,
    TableModule,
    InputTextModule,
  ],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  pageSize: { name: string }[] | undefined;
  category!: Category[];
  selectedCategories!: Category[];

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.pageSize = [
      { name: '5' },
      { name: '7' },
      { name: '10' },
    ];

    this.categoryService.getAllCategories().subscribe(
      (data: Category[]) => {
        this.category = data;
      },
      (error) => {
        console.error('Error fetching categories', error);
      }
    );
  }
}
