import { CommonModule } from '@angular/common';
import { Component, OnInit, effect } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../../../_services/category.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { Category } from '../../../../_models/category.module';

@Component({
  selector: 'app-categoryadd',
  standalone: true,
  imports: [
    DropdownModule,
    ButtonModule,
    CheckboxModule,
    CommonModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    ReactiveFormsModule,
  ],
  providers: [MessageService],
  templateUrl: './categoryadd.component.html',
  styleUrl: './categoryadd.component.css',
})
export class CategoryaddComponent implements OnInit {
  pageSize: { name: string }[] | undefined;
  visible: boolean = false;
  btnText: string = 'Thêm';
  categoryForm: any;

  constructor(
    private builder: FormBuilder,
    public categoryService: CategoryService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.categoryForm = this.builder.group({
      id: this.builder.control(0),
      name: this.builder.control(''),
    });

    const categoryItems = this.categoryService.categoryItems();
    this.categoryForm.setValue({
      id: categoryItems.id,
      name: categoryItems.name,
    });
    this.btnText = categoryItems.id > 0 ? 'Cập Nhật' : 'Thêm';
  }

  showDialog() {
    this.visible = true;
  }

  addCategory() {
    const data: Category = {
      id: 0,
      name: this.categoryForm.value.name,
    };

    console.log('Dữ liệu gửi đi:', data);

    if (data.id > 0) {
      this.categoryService.updateCategory(data).subscribe(
        (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật danh mục thành công',
            life: 3000,
          });
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Thất Bại',
            detail: 'Cập nhật danh mục thất bại',
            life: 3000,
          });
        }
      );
    } else {
      this.categoryService.addCategory(data).subscribe(
        (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm danh mục thành công',
            life: 3000,
          });
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Thất Bại',
            detail: 'Thêm danh mục thất bại',
            life: 3000,
          });
        }
      );
    }
  }
}
