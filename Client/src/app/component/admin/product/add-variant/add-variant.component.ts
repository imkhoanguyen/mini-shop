import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Size } from '../../../../_models/size.module';
import { Color } from '../../../../_models/color.module';
import { MessageService } from 'primeng/api';
import { SizeService } from '../../../../_services/size.service';
import { ColorService } from '../../../../_services/color.service';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { BadgeModule } from 'primeng/badge';
import { MultiSelectModule } from 'primeng/multiselect';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ColorPickerModule } from 'primeng/colorpicker';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-variant',
  standalone: true,
  imports: [DropdownModule,
    ButtonModule,
    CheckboxModule,
    CommonModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    ReactiveFormsModule,
    EditorModule,
    FormsModule,
    FileUploadModule,
    BadgeModule,
    InputNumberModule,
    MultiSelectModule,
    ToastModule,
    RadioButtonModule,
    ColorPickerModule,
    ],
  templateUrl: './add-variant.component.html',
  styleUrl: './add-variant.component.css',
  providers: [MessageService],
})
export class AddVariantComponent implements OnInit {
  imageForm!: FormGroup;
  variantsForm!: FormGroup;

  //Size
  sizes: Size[] = [];
  sizeId: number = 0;
  searchSize: string = '';
  //color
  code!: string;
  name!: string;
  colors: Color[] = [];
  colorId: number = 0;
  searchColor: string = '';

  selectedMainImage!: string;
  btnText: string = 'Thêm';
  visible: boolean = false;

  constructor(private messageService: MessageService,
              private sizeService: SizeService,
              private colorService: ColorService,
              private builder: FormBuilder,
              private router: Router
  ) {}
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
  addSize() {
    const data: Size = {
      id: 0,
      name: this.searchSize,
    };
    console.log('item: ' + this.searchSize);
    this.sizeService.addSize(data).subscribe(
      (res: any) => {
        this.showMessage(
          'success',
          'Thành Công',
          'Thêm kích thước thành công.'
        );
        this.loadSizes();
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm kích thước.');
        console.error('Failed to add Size', error);
      }
    );
  }
  showColorDialog() {
    this.visible = true;
  }
  updateColorFromInput(value: string) {
    this.code = value;
  }
  loadSizes(): void {
    this.sizeService.getAllSizes().subscribe(
      (data: Size[]) => {
        this.sizes = data;
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi tải kích thước.');
        console.error('Failed to load Sizes', error);
      }
    );
  }
  loadColors(): void {
    this.colorService.getAllColors().subscribe(
      (data: Color[]) => {
        this.colors = data;
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi tải màu sắc.');
        console.error('Failed to load Colors', error);
      }
    );
  }
  onSizeFilter(event: any) {
    this.searchSize = event.filter;
  }
  onColorFilter(event: any) {
    this.searchColor = event.filter;
  }

  addColor() {
    const data: Color = {
      id: 0,
      name: this.name,
      code: this.code,
    };
    this.colorService.addColor(data).subscribe(
      (res: any) => {
        this.showMessage('success', 'Thành Công', 'Thêm màu thành công.');
        this.loadColors();
        setTimeout(() => {
          this.visible = false;
        }, 1000);
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm màu.');
        console.error('Failed to add Color', error);
      }
    );
  }
  onUpload(event: any, i: number): void {
    const variantImages = this.variants.at(i).get('imageUrls') as FormArray;
    for (let file of event.files) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        variantImages.push(
          this.builder.group({
            name: file.name,
            size: file.size,
            url: e.target.result,
            file: file,
            isMain: false,
          })
        );
      };
      reader.readAsDataURL(file);
    }
    this.messageService.add({
      severity: 'info',
      summary: 'File Uploaded',
      detail: '',
    });
  }
  get variants(): FormArray {
    return this.variantsForm.get('variants') as FormArray;
  }
  addVariant(): void {
    const variantGroup = this.builder.group({
      price: this.builder.control(null, Validators.required),
      priceSell: this.builder.control(null),
      quantity: this.builder.control(null, Validators.required),
      colorId: this.builder.control(null),
      sizeId: this.builder.control(null),
      imageUrls: this.builder.array([]),
      selectedMainImage: this.builder.control('', Validators.required),
    });
    this.variants.push(variantGroup);
  }
  removeVariant(index: number): void {
    this.variants.removeAt(index);
  }
  onSubmit(): void {
    if (this.variantsForm.valid) {
      this.router.navigateByUrl('/admin/product');
    } else {
      this.showMessage('error', 'Thất Bại', 'Vui lòng điền đầy đủ thông tin.');
    }
  }
  nextPage() {
    this.router.navigate(['/admin/productForm/addProduct']);
  }

  prevPage() {
    this.router.navigate(['/admin/productForm/addProduct']);
  }
}
