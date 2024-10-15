import { Component, OnInit } from '@angular/core';
import { MultiSelectModule } from 'primeng/multiselect';
import { Category } from '../../../../_models/category.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../../../_services/category.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [MultiSelectModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css',
  providers: [MessageService],
})
export class AddProductComponent implements OnInit {

  categories: Category[] = [];
  selectedCategories: any[] = [];
  searchCategory: string = '';

  constructor(private categoryService: CategoryService,
              private messageService: MessageService,
              private router: Router
  ) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  private showMessage(
    severity: string,
    summary: string,
    detail: string,
    life: number = 3000
  ): void {
    this.messageService.add({ severity, summary, detail, life });
  }
  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe(
      (data: Category[]) => {
        this.categories = data;
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi tải danh mục.');
        console.error('Failed to load categories', error);
      }
    );
  }
  onCategoryFilter(event: any) {
    this.searchCategory = event.filter;
  }
  addCategory() {
    const data: Category = {
      id: 0,
      name: this.searchCategory,
      created: new Date(),
      updated: new Date(),
    };
    console.log('item: ' + this.searchCategory);
    this.categoryService.addCategory(data).subscribe(
      (res: any) => {
        this.showMessage('success', 'Thành Công', 'Thêm danh mục thành công.');
        this.loadCategories();
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm danh mục.');
        console.error('Failed to add category', error);
      }
    );
  }
  nextPage(){
    this.router.navigate(['/admin/productForm/addVariant']);
  }
}
