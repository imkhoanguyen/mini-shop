import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../../_services/category.service';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { StepperModule } from 'primeng/stepper';
import { Size } from '../../../../_models/size.module';
import { Color } from '../../../../_models/color.module';
import { SizeService } from '../../../../_services/size.service';
import { ColorService } from '../../../../_services/color.service';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { BadgeModule } from 'primeng/badge';
import { InputNumberModule } from 'primeng/inputnumber';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ColorPickerModule } from 'primeng/colorpicker';
import { ProductService } from '../../../../_services/product.service';
import { VariantAdd, VariantDto } from '../../../../_models/variant.module';
import { VariantService } from '../../../../_services/variant.service';
import { CategoryAdd, CategoryDto } from '../../../../_models/category.module';
import { ToastrService } from '../../../../_services/toastr.service';
import { ProgressBarModule } from 'primeng/progressbar';
import { AccordionModule } from 'primeng/accordion';
import { ProductStatus } from '../../../../_models/product.module';
@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [
    FileUploadModule,
    BadgeModule,
    ProgressBarModule,
    MultiSelectModule,
    ReactiveFormsModule,
    CommonModule,
    InputTextModule,
    InputTextareaModule,
    StepperModule,
    DropdownModule,
    ButtonModule,
    CheckboxModule,
    DialogModule,
    ToastModule,
    InputNumberModule,
    RadioButtonModule,
    ColorPickerModule,
    FormsModule,
    AccordionModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
  providers: [MessageService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductFormComponent implements OnInit {
  productForm!: FormGroup;
  variantsForm!: FormGroup;
  categories: CategoryDto[] = [];
  sizes: Size[] = [];
  colors: Color[] = [];
  isUpdate: boolean = false;

  searchCategory: string = '';
  searchSize: string = '';
  searchColor: string = '';

  visible: boolean = false;
  name: string = '';
  code: string = '';

  files: { src: string; file: File }[] = [];
  selectedFiles: any[] = [];
  totalSize: number = 0;
  totalSizePercent: number = 0;

  selectedStatus!: ProductStatus;

  status: { name: string; key: ProductStatus }[] = [
    { name: 'Draft', key: ProductStatus.Draft },
    { name: 'Publish', key: ProductStatus.Publish },
  ];

  constructor(
    private categoryService: CategoryService,
    private variantService: VariantService,
    private productService: ProductService,
    private toastService: ToastrService,
    private sizeService: SizeService,
    private colorService: ColorService,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForms();
    this.loadCategories();
    // this.loadColors();
    // this.loadSizes();
    //this.addVariant();
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      if (id) {
        this.isUpdate = true;
        this.getProductToUpdate(id);
      }
    });
  }

  initializeForms() {
    this.productForm = this.fb.group({
      id: [null],
      name: ['', Validators.required],
      selectedCategories: [[], Validators.required],
      description: ['', Validators.required],
      status: [ProductStatus.Draft, Validators.required],
      imageFile: this.fb.array([]),
    });

    this.variantsForm = this.fb.group({
      id: [null],
      price: [null, Validators.required],
      priceSell: [null],
      quantity: [null, Validators.required],
      colorId: [0],
      sizeId: [0],
      imageFiles: this.fb.array([]),
    });
  }
  // removeVariant(index: number) {
  //   this.variants.removeAt(index);
  // }

  // get variants() {
  //   return this.variantsForm.get('variants') as FormArray;
  // }
  // addVariant() {
  //   const variantGroup = this.fb.group({
  //     id: [null],
  //     price: [null, Validators.required],
  //     priceSell: [null],
  //     quantity: [null, Validators.required],
  //     colorId: [0],
  //     sizeId: [0],
  //     imageFiles: [[]]
  //   });
  //   this.variants.push(variantGroup);
  // }
  // setVariants(variants: any[]) {
  //   const variantFGs = variants.map(variant => this.fb.group({
  //     id: [variant.id || null],
  //     price: [variant.price || null, Validators.required],
  //     priceSell: [variant.priceSell || null],
  //     quantity: [variant.quantity || null, Validators.required],
  //     colorId: [variant.colorId || 0],
  //     sizeId: [variant.sizeId || 0],
  //     imageFiles: [variant.imageFiles || []]
  //   }));

  //   const variantFormArray = this.fb.array(variantFGs);
  //   this.variantsForm.setControl('variants', variantFormArray);
  // }

  getProductToUpdate(id: number) {
    this.productService.getProductById(id).subscribe(
      (data: any) => {
        this.productForm.patchValue({
          id: data.id,
          name: data.name,
          selectedCategories: data.categoryIds,
          description: data.description,
          image: data.image?.imgUrl,

        });
        console.log('data.image', data.image.imgUrl);
        //this.loadVariants(data.variants);
        //is.isUpdate = true;
      },
      (error) => {
        this.toastService.error('Lỗi khi tải sản phẩm.');
      }
    );
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data: CategoryDto[]) => {
        this.categories = data;
      },
      error: () => {
        this.toastService.error('Lỗi khi tải danh mục.');
      }
    });
  }

  onCategoryFilter(event: any): void {
    this.searchCategory = event.filter.trim();
  }

  addCategory(): void {
    const categoryName = this.searchCategory.trim();
    if (this.isCategoryNameValid(categoryName)) {
      const data: CategoryAdd = { name: categoryName };
      this.categoryService.addCategory(data).subscribe({
        next: () => {
          this.toastService.success('Thêm danh mục thành công.');
          this.resetCategoryInput();
          this.loadCategories();
        },
        error: () => {
          this.toastService.error('Lỗi khi thêm danh mục.');
        }
      });
    }
  }

  private isCategoryNameValid(categoryName: string): boolean {
    if (!categoryName) {
      this.toastService.error('Tên danh mục không được trống.');
      return false;
    }
    return true;
  }

  private resetCategoryInput(): void {
    this.searchCategory = '';
  }


  // ----------------- Image -----------------
  choose(event: any, callback: () => void) {
    callback();
  }

  onSelectedFiles(event: any) {
    const totalFiles = event.files.length;
    let filesRead = 0;

    if (Array.isArray(event.files) || event.files instanceof FileList) {
      (Array.from(event.files) as File[]).forEach((file: File) => {
        if (file instanceof File) {
          const reader = new FileReader();

          reader.onload = (e: any) => {
            const imageFormArray = this.productForm.get('imageFile') as FormArray;
            imageFormArray.push(this.fb.control(file));
            filesRead++;
            if (filesRead === totalFiles) {
              this.files = imageFormArray.controls.map(control => ({
                src: e.target.result,
                file: control.value
              }));
              console.log('All selected files:', this.files);
            }
          };
          reader.readAsDataURL(file);
        }
      });
    } else {
      console.error('error', event.files);
    }
  }

  onSelectedVariantFiles(event: any) {
    const totalFiles = event.files.length;
    let filesRead = 0;

    if (Array.isArray(event.files) || event.files instanceof FileList) {
      (Array.from(event.files) as File[]).forEach((file: File) => {
        if (file instanceof File) {
          const reader = new FileReader();

          reader.onload = (e: any) => {
            const imagesFormArray = this.variantsForm.get('imageFiles') as FormArray;

            imagesFormArray.push(this.fb.control(file));
            filesRead++;
            if (filesRead === totalFiles) {
              this.files = imagesFormArray.controls.map(control => ({
                src: e.target.result,
                file: control.value
              }));

              console.log('All selected variant files:', this.files);
            }
          };
          reader.readAsDataURL(file);
        }
      });
    } else {
      console.error('error', event.files);
    }
  }

  // Xóa một file đã chọn
  onRemoveTemplatingFile(event: any, file: { size: any }, removeFileCallback: (arg0: any, arg1: any) => void, index: any) {
    removeFileCallback(event, index);
    this.totalSize -= file.size;
    this.totalSizePercent = this.totalSize / 10;
  }

  // Xóa tất cả file đã chọn
  onClearTemplatingUpload(clear: () => void) {
    clear();
    this.totalSize = 0;
    this.totalSizePercent = 0;
    this.selectedFiles = [];
  }


  onSubmit(): void {
    //if (this.productForm.valid && this.variantsForm.valid) {
      console.log('productForm', this.productForm.value);
      console.log('variantsForm', this.variantsForm.value);
      console.log('selectedCategories', this.productForm.value.selectedCategories);
      this.addOrUpdateProduct();
    //} else {
      //this.toastService.error('Vui lòng điền đầy đủ thông tin.');
    //}
  }

  addVariants(productId: number) {
      const variantAdd: FormData = this.createFormDataForAdd(productId);

      this.variantService.addVariant(variantAdd).subscribe({
        next: () => this.toastService.success('Thêm biến thể thành công.'),
        error: (error) => this.toastService.error('Lỗi khi thêm biến thể: ' + error),
      });

  }

  private createFormDataForAdd(productId: number): FormData {
    const formData = new FormData();
    formData.append('productId', productId.toString());
    formData.append('price', this.variantsForm.value.price);
    formData.append('priceSell', this.variantsForm.value.priceSell);
    formData.append('quantity', this.variantsForm.value.quantity);

    const imagesFormArray = this.variantsForm.get('imageFiles') as FormArray;
    imagesFormArray.controls.forEach((control: any) => {
      const file = control.value;
      if (file instanceof File) {
        formData.append('imageFiles', file);
      } else {
        console.error('Dự kiến là một file, nhưng nhận được:', file);
      }
    });


    return formData;
  }

  addOrUpdateProduct() {
    const formData = this.createProductFormData();

    if (this.isUpdate) {
      this.route.paramMap.subscribe((params) => {
        const productId = Number(params.get('id'));
        console.log('Product ID:', productId);
        //this.updateProduct(productId, formData);
      });
    } else {
      this.addProduct(formData);
    }
  }

  private createProductFormData(): FormData {
    const formData = new FormData();
    formData.append('name', this.productForm.value.name);
    formData.append('description', this.productForm.value.description);
    this.productForm.value.selectedCategories.forEach((categoryId: number) => {
      formData.append('categoryIds', categoryId.toString());
    });
    formData.append('status', "1");

    const imageFormArray = this.productForm.get('imageFile') as FormArray;
    imageFormArray.controls.forEach((control: any) => {
      const file = control.value; // Đây là file thực tế
      if (file instanceof File) {
        formData.append('imageFile', file); // Thêm file vào FormData
      } else {
        console.error('Dự kiến là một file, nhưng nhận được:', file);
      }
    });

    return formData;
  }

  private async addProduct(formData: FormData) {
    try {
      const response = await this.productService.addProduct(formData).toPromise();
      if (response && response.id) {
        const productId = response.id;
        this.variantsForm.patchValue({
          productId: productId
        });
        this.addVariants(productId);
        this.toastService.success('Thêm sản phẩm thành công.');
      } else {
        this.toastService.error('Lỗi khi thêm sản phẩm: Không nhận được ID sản phẩm.');
      }
    } catch (error) {
      this.toastService.error('Lỗi khi thêm sản phẩm: ' + error);
    }
  }

   //----------------- Size -----------------
 loadSizes(): void {
  this.sizeService.getAllSizes().subscribe(
    (data: Size[]) => {
      this.sizes = data;
    },
    (error) => {
      this.toastService.error('Lỗi khi tải kích thước.');
    }
  );
}

onSizeFilter(event: any) {
  this.searchSize = event.filter;
}

addSize() {
  const sizeName = this.searchSize.trim();
  if (!sizeName) {
    this.toastService.error('Tên kích thước không được trống.');
    return;
  }
  const data: Size = {
    id: 0,
    name: sizeName,
  };
  this.sizeService.addSize(data).subscribe({
    next: () => {
      this.toastService.success('Th êm kích thước thành công.');
      this.loadSizes();
    },
    error: () => {
      this.toastService.error('Lỗi khi thêm kích thước.');
    },
  });
}

// ----------------- Color -----------------
showColorDialog() {
  this.visible = true;
}

updateColorFromInput(value: string) {
  this.code = value;
}

loadColors(): void {
  this.colorService.getAllColors().subscribe(
    (data: Color[]) => {
      this.colors = data;
    },
    (error) => {
      this.toastService.error('Lỗi khi tải màu sắc.');
    }
  );
}

onColorFilter(event: any) {
  this.searchColor = event.filter;
}

addColor() {
  if (!this.name.trim() || !this.code) {
    this.toastService.error('Vui lòng điền đầy đủ thông tin.');
    return;
  }
  const data: Color = { id: 0, name: this.name, code: this.code };
  this.colorService.addColor(data).subscribe({
    next: () => {
      this.toastService.success('Thêm màu thành công.');
      this.loadColors();
      this.visible = false;
    },
    error: () => {
      this.toastService.error('Lỗi khi thêm màu.');
    },
  });
}
}

