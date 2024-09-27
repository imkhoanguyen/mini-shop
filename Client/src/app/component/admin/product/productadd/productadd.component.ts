import { Component, EventEmitter } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import {FormArray,FormBuilder,FormGroup,FormsModule,ReactiveFormsModule,Validators,} from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../../../_services/product.service';
import { StepperModule } from 'primeng/stepper';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { BadgeModule } from 'primeng/badge';
import { InputNumberModule } from 'primeng/inputnumber';
import { MultiSelectModule } from 'primeng/multiselect';
import { RadioButtonModule } from 'primeng/radiobutton';
import { Category } from '../../../../_models/category.module';
import { CategoryService } from '../../../../_services/category.service';
import { Size } from '../../../../_models/size.module';
import { Color } from '../../../../_models/color.module';
import { SizeService } from '../../../../_services/size.service';
import { ColorService } from '../../../../_services/color.service';
import { ImageService } from '../../../../_services/image.service';
import { VariantService } from '../../../../_services/variant.service';
import { Variant, VariantAdd } from '../../../../_models/variant.module';
import { Image, ImageAdd } from '../../../../_models/image.module';
import { Product, ProductAdd } from '../../../../_models/product.module';
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
    ToastModule,
    RadioButtonModule,
  ],
  providers: [MessageService],
  templateUrl: './productadd.component.html',
  styleUrls: ['./productadd.component.css'],
})
export class ProductaddComponent {
  productForm!: FormGroup;
  imageForm!: FormGroup;
  variantsForm!: FormGroup;
  //Category
  categories: Category[] = [];
  selectedCategories: any[] = [];
  searchCategory: string = '';
  //Size
  sizes: Size[] = [];
  sizeId: number = 0;
  searchSize: string = '';
  //color
  colors: Color[] = [];
  colorId: number = 0;
  searchColor: string = '';

  uploadedFiles: any[] = [];
  selectedMainImage!: string;
  btnText: string = 'Thêm';

  nextCallback = new EventEmitter<void>();
  totalSize: number = 0;
  totalSizePercent: number = 0;

  constructor(
    private builder: FormBuilder,
    private productService: ProductService,
    private messageService: MessageService,
    private categoryService: CategoryService,
    private variantService: VariantService,
    private imageService: ImageService,
    private sizeService: SizeService,
    private colorService: ColorService
  ) {}

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
      selectedCategories: [[], Validators.required],
      description: ['', Validators.required],
    });
    this.variantsForm = this.builder.group({
      variants: this.builder.array([]),
    });
    this.imageForm = this.builder.group({
      selectedMainImage: [''],
    });
  }
  private showMessage(
    severity: string,
    summary: string,
    detail: string,
    life: number = 3000
  ): void {
    this.messageService.add({ severity, summary, detail, life });
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
  addColor() {
    const data: Color = {
      id: 0,
      name: this.searchColor,
      code: '',
    };
    console.log('item: ' + this.searchColor);
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
        description: productItems.description,
        variants: productItems.variants,
        imageUrls: productItems.imageUrls,
      });
      this.btnText = productItems.id > 0 ? 'Cập Nhật' : 'Thêm';
    }
  }
  addProduct() {
    const productAdd: ProductAdd = {
      name: this.productForm.value.name,
      description: this.productForm.value.description,
      categoryIds: this.productForm.value.selectedCategories,
    };
    console.log('product', productAdd);
    this.productService.addProduct(productAdd).subscribe(
      (productResponse: any) => {
        const productId = productResponse?.id;

        if (productId) {
          console.log('Product ID received:', productId);
          this.addImages(productId);
          this.addVariants(productId);
        } else {
          console.error('Product ID not received:', productResponse);
        }

        this.showMessage('success', 'Thành Công', 'Thêm sản phẩm thành công.');
      },
      (error) => {
        this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm sản phẩm.');
        console.error('Failed to add product', error);
      }
    );
  }
  addImages(productId: number) {
    const mainImage = this.imageForm.value.selectedMainImage;
    this.uploadedFiles.forEach((file) => {
      const isMain = file.name === mainImage;
      const imageAdd = new FormData();
      imageAdd.append('url', file);
      imageAdd.append('isMain', isMain.toString());
      imageAdd.append('productId', productId.toString());
      this.imageService.addImage(imageAdd).subscribe(
        (response: any) => {
          this.showMessage('success', 'Thành Công', 'Ảnh được thêm thành công!');
        },
        (error) => {
          this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm ảnh.');
          console.error('Failed to upload image:', error);
        }
      );
    });
  }


  addVariants(productId: number) {
    this.variants.controls.forEach(variantGroup => {
      const variantAdd: VariantAdd = {
        price: variantGroup.value.price,
        priceSell: variantGroup.value.priceSell,
        quantity: variantGroup.value.quantity,
        sizeId: variantGroup.value.sizeId,
        colorId: variantGroup.value.colorId,
        productId: productId,
      };

      console.log('variant', variantAdd);
      this.variantService.addVariant(variantAdd).subscribe(
        () => this.showMessage('success', 'Thành Công', 'Biến thể được thêm thành công!'),
        (error) => this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm biến thể.')
      );
    });
  }
  //-----------Variant------------
  get variants(): FormArray {
    return this.variantsForm.get('variants') as FormArray;
  }
  createVariant(): FormGroup {
    return this.builder.group({
      price: [null, Validators.required],
      priceSell: [null],
      quantity: [1, Validators.required],
      sizeId: [''],
      colorId: [''],
    });
  }
  addVariant(): void {
    const variantGroup = this.builder.group({
      colorId: this.builder.control(0),
      sizeId: this.builder.control(0),
      price: this.builder.control(null, Validators.required),
      priceSell: this.builder.control(null),
      quantity: this.builder.control(null, Validators.required),
    });
    this.variants.push(variantGroup);
  }
  removeVariant(index: number): void {
    this.variants.removeAt(index);
  }
  onUpload(event: any): void {
    for (let file of event.files) {
      this.uploadedFiles.push(file);
    }
    this.messageService.add({
      severity: 'info',
      summary: 'File Uploaded',
      detail: '',
    });
  }
  onSubmit(): void {
    if (this.productForm.valid && this.variantsForm.valid) {
      this.addProduct();
    } else {
      this.showMessage('error', 'Thất Bại', 'Vui lòng điền đầy đủ thông tin.');
    }
  }
}
