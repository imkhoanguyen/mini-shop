import { Component, EventEmitter } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../../../_services/product.service';
import { StepperModule } from 'primeng/stepper';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { BadgeModule } from 'primeng/badge';
import { InputNumberModule } from 'primeng/inputnumber';
@Component({
  selector: 'app-productadd',
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
    StepperModule,
    EditorModule,
    FormsModule,
    FileUploadModule,
    BadgeModule,
    InputNumberModule
  ],
  providers: [MessageService],
  templateUrl: './productadd.component.html',
  styleUrls: ['./productadd.component.css'],
})
export class ProductaddComponent {
  visible: boolean = false;
  btnText: string = 'Thêm';
  productForm: any;
  text: string | undefined;
  files = [];
   variants: any[] = [{ type: 'Size', value: '' }];

  // Options for the dropdown (Size, Color, etc.)
  variantOptions = [
    { label: 'Size', value: 'Size' },
    { label: 'Color', value: 'Color' },
    { label: 'Material', value: 'Material' }
  ];
  nextCallback = new EventEmitter<void>();
  totalSize: number = 0;
  totalSizePercent: number = 0;

  constructor(
    private builder: FormBuilder,
    public productService: ProductService,
    private messageService: MessageService,
    private config: PrimeNGConfig
  ) {}

  ngOnInit(): void {
    this.productForm = this.builder.group({
      id: this.builder.control(0),
      name: this.builder.control(''),
    });
    this.variants.push(1);

    const productItems = this.productService.productItems();
    this.productForm.setValue({
      id: productItems.id,
      name: productItems.name,
    });
    this.btnText = productItems.id > 0 ? 'Cập Nhật' : 'Thêm';
  }

  showForm() {
    this.visible = true;
  }

  addProduct() {}

  choose(event: any, callback: Function) {
    callback();
  }

  onRemoveTemplatingFile(event: any, file: any, removeFileCallback: Function, index: number) {
    removeFileCallback(event, index);
    this.totalSize -= parseInt(this.formatSize(file.size));
    this.totalSizePercent = this.totalSize / 10;
  }

  onClearTemplatingUpload(clear: Function) {
    clear();
    this.totalSize = 0;
    this.totalSizePercent = 0;
  }

  onTemplatedUpload() {
    this.messageService.add({
      severity: 'info',
      summary: 'Success',
      detail: 'File Uploaded',
      life: 3000,
    });
  }

  onSelectedFiles(event: any) {
    this.files = event.currentFiles;
    this.files.forEach((file: any) => {
      this.totalSize += parseInt(this.formatSize(file.size));
    });
    this.totalSizePercent = this.totalSize / 10;
  }

  uploadEvent(callback: Function) {
    callback();
  }

  formatSize(bytes: number): string {
    const k = 1024;
    const dm = 3;
    const sizes = this.config.translation?.fileSizeTypes || ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes === 0) {
      return `0 ${sizes[0]}`;
    }

    const i = Math.floor(Math.log(bytes) / Math.log(k));
    const formattedSize = parseFloat((bytes / Math.pow(k, i)).toFixed(dm));

    return `${formattedSize} ${sizes[i]}`;
  }

   addVariant() {
    this.variants.push({ type: 'Size', value: '' });
    console.log(this.variants);
  }

  // Method to remove a variant
  removeVariant(index: number) {
    this.variants.splice(index, 1);
  }

}
