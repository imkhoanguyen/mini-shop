import { Component, EventEmitter } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService} from 'primeng/api';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../../../_services/product.service';
import { StepperModule } from 'primeng/stepper';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule, FileUploadEvent} from 'primeng/fileupload';
import { BadgeModule } from 'primeng/badge';
import { InputNumberModule } from 'primeng/inputnumber';
import { MultiSelectModule } from 'primeng/multiselect';
import { Category } from '../../../../_models/category.module';
import { CategoryService } from '../../../../_services/category.service';
import { Size } from '../../../../_models/size.module';
import { Color } from '../../../../_models/color.module';
import { SizeService } from '../../../../_services/size.service';
import { ColorService } from '../../../../_services/color.service';

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
    InputNumberModule,
    MultiSelectModule,
    ToastModule

  ],
  providers: [MessageService],
  templateUrl: './productadd.component.html',
  styleUrls: ['./productadd.component.css'],
})
export class ProductaddComponent {
  productForm!: FormGroup;
  variantsForm: FormGroup;
  //Category
  categories: Category[] = [];
  selectedCategories: any[] = [];
  searchCategory: string = '';

  //Size
  sizes: Size[] = [];
  selectedSizes: any[] = [];
  searchSize: string = '';

  //color
  colors: Color[] = [];
  selectedColors: any[] = [];
  searchColor: string = '';

  uploadedFiles: any[] = [];
  btnText: string = 'Thêm';

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
    private productService: ProductService,
    private messageService: MessageService,
    private categoryService: CategoryService,
    private sizeService: SizeService,
    private colorService: ColorService)
  {
    this.variantsForm = this.builder.group({
      variants: this.builder.array([]),
    });
  }

  ngOnInit(): void {
    this.initializeProductForm();
    this.loadCategories();
    this.loadSizes();
    this.loadColors();
    this.loadProductData();

  }
  initializeProductForm(): void {
    this.productForm = this.builder.group({
      id: [0],
      name: ['', Validators.required],
      selectedCategory: [null, Validators.required],
      description: ['', Validators.required],
    });
  }
  private showMessage(severity: string, summary: string, detail: string, life: number = 3000): void {
    this.messageService.add({ severity, summary, detail, life });
  }
  addCategory() {
    const data : Category = {
      id: 0,
      name: this.searchCategory,
      created: new Date(),
      updated: new Date(),

    };
    console.log('item: '+ this.searchCategory);
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
  addSize() {
    const data : Size = {
      id: 0,
      name: this.searchSize,

    };
    console.log('item: '+ this.searchSize);
    this.sizeService.addSize(data).subscribe(
      (res: any) => {
        this.showMessage('success', 'Thành Công', 'Thêm kích thước thành công.');
        this.loadSizes();
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm kích thước.');
        console.error('Failed to add Size', error);
      }
    );
  }
  addColor() {
    const data : Color = {
      id: 0,
      name: this.searchColor,
      code: ""

    };
    console.log('item: '+ this.searchColor);
    this.colorService.addColor(data).subscribe(
      (res: any) => {
        this.showMessage('success', 'Thành Công', 'Thêm màu thành công.');
        this.loadColors();
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Li khi thêm màu.');
        console.error('Failed to add Color', error);
      }
    );
  }
  // ----------------Category------------------
  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe(
      (data: Category[]) => {
        this.categories = data
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi tải danh mục.');
        console.error('Failed to load categories', error);
      }
    );
  }
  loadSizes(): void {
    this.sizeService.getAllSizes().subscribe(
      (data: Size[]) => {
        this.sizes = data
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
  onCategoryFilter(event: any) {
    this.searchCategory = event.filter;
  }
  onSizeFilter(event: any) {
    this.searchSize = event.filter;
  }
  onColorFilter(event: any) {
    this.searchColor = event.filter;
  }
  loadProductData(): void {
    const productItems = this.productService.productItems();
    if (productItems) {
      this.productForm.patchValue({
        id: productItems.id,
        name: productItems.name,
        selectedCategory: productItems.categoryIds,
        description: productItems.description
      });
      this.btnText = productItems.id > 0 ? 'Cập Nhật' : 'Thêm';
    }
  }
  addProduct(){}
  // ----------------Image------------------
  onUpload(event: any): void {
    for (let file of event.files) {
      this.uploadedFiles.push(file);
    }
    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }

  //-----------Variant------------
  get variants(): FormArray {
    return this.variantsForm.get('variants') as FormArray;
  }
  createVariant(): FormGroup {
    return this.builder.group({
      price: ['', Validators.required],
      discountPrice: [null],
      quantity: [1, Validators.required],
      size: ['', Validators.required],
      color: ['', Validators.required]
    });
  }
  addVariant(): void {
    const variantGroup = this.builder.group({
      selectedColors: this.builder.control([], Validators.required),
      selectedSizes: this.builder.control([], Validators.required),
      price: this.builder.control(null, Validators.required),
      priceSell: this.builder.control(null),
      quantity: this.builder.control(null, Validators.required),
    });

    this.variants.push(variantGroup);
  }
  removeVariant(index: number): void {
    this.variants.removeAt(index);
  }


  onSubmit(): void {
    console.log(this.variantsForm.value);
  }

}
