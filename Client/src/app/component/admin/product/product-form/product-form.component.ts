import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MultiSelectModule } from 'primeng/multiselect';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
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
import { VariantAdd, VariantDto, VariantStatus, VariantUpdate } from '../../../../_models/variant.module';
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

  files: any[] = [];
  selectedFile: any[] = [];
  selectedFiles: any[] = [];
  selectedFilesMap: { [key: number]: { src: string; file: File | null; imageId: number }[] } = {};
  totalSize: number = 0;
  totalSizePercent: number = 0;

  hasImage: boolean = false;
  hasVariantImage: boolean = false;
  selectedStatus!: ProductStatus;

  productStatus: { name: string; key: ProductStatus }[] = [
    { name: 'Draft', key: ProductStatus.Draft },
    { name: 'Publish', key: ProductStatus.Publish },
  ];
  variantStatus: { name: string; key: VariantStatus }[] = [
    { name: 'Draft', key: VariantStatus.Draft },
    { name: 'Publish', key: VariantStatus.Publish },
  ];

  constructor(
    private categoryService: CategoryService,
    private variantService: VariantService,
    private productService: ProductService,
    private toastService: ToastrService,
    private sizeService: SizeService,
    private colorService: ColorService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.initializeForms();
    this.loadCategories();
    this.loadColors();
    this.loadSizes();
    this.addVariant();
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
      description: [''],
      status: [ProductStatus.Draft],
      imageFile: this.fb.array([]),
    });

    this.variantsForm = this.fb.group({
      variants: this.fb.array([]),
    });
    this.productForm.addControl('variants', this.variantsForm);
  }
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

        if (data.image?.imgUrl) {
          this.selectedFile.push({
            src: data.image.imgUrl,
            file: null,
            imageId: data.image.id
          });
          this.hasImage = true;
        }
        this.loadVariants(data.variants);
        this.isUpdate = true;
      },
      (error) => {
        this.toastService.error('Lỗi khi tải sản phẩm.');
      }
    );
  }

  loadVariants(variants: VariantDto[]) {
    this.variants.clear();

    if (variants && variants.length > 0) {

      variants.forEach((variant) => {
        const variantGroup = this.fb.group({
          id: [variant.id || null],
          price: [variant.price, Validators.required],
          priceSell: [variant.priceSell || null],
          quantity: [variant.quantity, Validators.required],
          colorId: [variant.colorId || null],
          sizeId: [variant.sizeId || null],
          status: [variant.status || VariantStatus.Draft],
          imageFiles: this.fb.array([]),
        });

        const imagesFormArray = variantGroup.get('imageFiles') as FormArray;
        this.selectedFilesMap[variant.id] = [];
        if (variant.images && variant.images.length > 0) {
          variant.images.forEach((image) => {
            imagesFormArray.push(this.fb.control(image.imgUrl));
            if (image.imgUrl) {
              this.selectedFilesMap[variant.id].push({ src: image.imgUrl, file: null, imageId: image.id });
              this.hasVariantImage = true;
            }
          });
        }

        this.variants.push(variantGroup);
      });
    } else {
      console.log("Không có dữ liệu biến thể.");
    }
  }

  removeVariant(index: number): void {
    const variant = this.variants.at(index).value;
    if (variant.id) {
      this.variantService.deleteVariant(variant.id).subscribe({
        next: () => {
          this.toastService.success("Xóa biến thể thành công")
        },
        error: (error) => {
          this.toastService.error("Lỗi khi xóa biến thể" + error)
        }
      });
    }
    this.variants.removeAt(index);
  }

  get variants() {
    return this.variantsForm.get('variants') as FormArray;
  }
  addVariant() {
    const variantGroup = this.fb.group({
      price: this.fb.control(null, Validators.required),
      priceSell: this.fb.control(null),
      quantity: this.fb.control(null, Validators.required),
      colorId: this.fb.control(null),
      sizeId: this.fb.control(null),
      status: this.fb.control(VariantStatus.Draft),
      imageFiles: this.fb.array([]),
    });
    this.variants.push(variantGroup);
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

  onSelectedFile(event: any) {
    const imageFormArray = this.productForm.get('imageFile') as FormArray;

    if (Array.isArray(event.files) || event.files instanceof FileList) {
      (Array.from(event.files) as File[]).forEach((file: File) => {
        if (file instanceof File) {
          const reader = new FileReader();

          reader.onload = (e: any) => {
            imageFormArray.push(this.fb.control(file));
            this.selectedFile.push({
              src: e.target.result,
              file: file
            });
            this.totalSize += file.size;
            this.totalSizePercent = this.totalSize / 10;
          };
          reader.readAsDataURL(file);
        }
      });
    }
  }


  onSelectedVariantFiles(event: any, index: number) {
    const variantGroup = this.variants.at(index) as FormGroup;
    const imagesFormArray = variantGroup.get('imageFiles') as FormArray;

    const existingFiles = imagesFormArray.controls.map(control => control.value);

    if (Array.isArray(event.files) || event.files instanceof FileList) {
      (Array.from(event.files) as File[]).forEach((file: File) => {
        if (file instanceof File) {
          const reader = new FileReader();

          reader.onload = (e: any) => {

            imagesFormArray.push(this.fb.control(file));

            this.selectedFiles.push({
              src: e.target.result,
              file: file
            });

            this.totalSize += file.size;
            this.totalSizePercent = this.totalSize / 10;
          };

          reader.readAsDataURL(file);
        }
      });
    }
    this.selectedFiles = existingFiles.map((file: File) => {
      return {
        src: URL.createObjectURL(file),
        file: file
      };
    }).concat(this.selectedFiles);
  }

  removeProductImage(file: { src: string; file: File | null, imageId: number }) {
    const index = this.selectedFile.indexOf(file);
    if (index > -1) {
      this.selectedFile.splice(index, 1);
      this.hasImage = false;
      this.productService.removeProductImage(this.productForm.value.id, file.imageId).subscribe({
        next: () => {
          this.toastService.success('Xóa ảnh sản phẩm thành công.');
        },
        error: () => {
          this.toastService.error('Lỗi khi xóa ảnh sản phẩm.');
        }
      });
    }
  }
  removeVariantImage(variantId: number, file: { src: string; file: File | null; imageId: number }) {
    const filesArray = this.selectedFilesMap[variantId];
    if (!filesArray) return;
    const index = filesArray.indexOf(file);
    if (index > -1) {
      filesArray.splice(index, 1);
      this.hasVariantImage = filesArray.length > 0;
      this.variantService.removeVariantImage(variantId, file.imageId).subscribe({
        next: () => {
          this.toastService.success('Xóa ảnh biến thể thành công.');
        },
        error: () => {
          this.toastService.error('Lỗi khi xóa ảnh biến thể.');
        }
      });
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
    if (this.productForm.valid && this.variantsForm.valid) {
    console.log('productForm', this.productForm.value);
    console.log('variantsForm', this.variantsForm.value);

    this.addOrUpdateProduct();
    } else {
    this.toastService.error('Vui lòng điền đầy đủ thông tin.');
    }
  }



  private async addProduct(formData: FormData) {
    try {
      const response = await this.productService.addProduct(formData).toPromise();
      if (response && response.id) {
        await this.processVariants(response.id, this.variantsForm.get('variants') as FormArray);
        this.toastService.success('Thêm sản phẩm thành công.');
      } else {
        this.toastService.error('Lỗi khi thêm sản phẩm: Không nhận được ID sản phẩm.');
      }
    } catch (error) {
      this.toastService.error('Lỗi khi thêm sản phẩm: ' + error);
    }
  }

  private async updateProduct(formData: FormData) {
    try {
      const response = await this.productService.updateProduct(formData).toPromise();
      if (response && response.id) {
        await this.processVariants(response.id, this.variantsForm.get('variants') as FormArray);
        this.toastService.success('Cập nhật sản phẩm thành công.');
      } else {
        this.toastService.error('Lỗi khi cập nhật sản phẩm: Không nhận được ID sản phẩm.');
      }
    } catch (error) {
      this.toastService.error('Lỗi khi cập nhật sản phẩm: ' + error);
    }
  }

  private async processVariants(productId: number, variantsArray: FormArray) {
    const variantPromises = variantsArray.controls.map(async (control: AbstractControl) => {
        const variantGroup = control as FormGroup;

        const variant = variantGroup.value;
        const variantFormData = this.createVariantFormData(productId, variant);

        if (variant.id) {
            const currentVariant = await this.variantService.getVariantById(variant.id).toPromise();
            if (!currentVariant) {
                throw new Error(`Variant with id ${variant.id} not found`);
            }

            const hasChanged = (currentVariant.price !== variant.price ||
                currentVariant.priceSell !== variant.priceSell ||
                currentVariant.quantity !== variant.quantity ||
                currentVariant.colorId !== variant.colorId ||
                currentVariant.sizeId !== variant.sizeId ||
                currentVariant.status !== variant.status);

            if (hasChanged) {
                const variantUpdate: VariantUpdate = {
                    id: variant.id,
                    productId: productId,
                    price: variant.price.toString(),
                    priceSell: variant.priceSell != null ? variant.priceSell.toString() : 0,
                    quantity: variant.quantity.toString(),
                    colorId: variant.colorId ? variant.colorId.toString() : null,
                    sizeId: variant.sizeId ? variant.sizeId.toString() : null,
                    status: variant.status
                };

                console.log("variantUpdate", variantUpdate);
                await this.variantService.updateVariant(variantUpdate).toPromise();
            }

            const imagesFormArray = variantGroup.get('imageFiles') as FormArray;
            if (imagesFormArray && imagesFormArray.length > 0) {
                const files: File[] = imagesFormArray.controls
                    .map(ctrl => ctrl.value)
                    .filter(file => file instanceof File) as File[];

                if (files.length > 0) {
                    await this.variantService.addVariantImages(variant.id, files).toPromise();
                    this.toastService.success('Cập nhật hình ảnh biến thể thành công.');
                } else {
                    this.toastService.success('Không có hình ảnh nào để cập nhật.');
                }
            }

            this.toastService.success('Cập nhật biến thể thành công.');

        } else {
            const imagesFormArray = variantGroup.get('imageFiles') as FormArray;
            if (imagesFormArray && imagesFormArray.length > 0) {
                for (const ctrl of imagesFormArray.controls) {
                    const file = ctrl.value;
                    if (file instanceof File) {
                        variantFormData.append('imageFiles', file);
                    }
                }
                await this.variantService.addVariant(variantFormData).toPromise();
                this.toastService.success('Thêm biến thể thành công.');
            }
        }
    });

    try {
        await Promise.all(variantPromises);
        this.router.navigateByUrl("/admin/product");
    } catch (error) {
        console.error('Chi tiết lỗi từ API:', error);
        this.toastService.error('Lỗi khi xử lý biến thể: ' + error);
    }
}

  private createVariantFormData(productId: number, variant: any): FormData {
    const variantFormData = new FormData();
    variantFormData.append('productId', productId.toString());
    variantFormData.append('price', variant.price.toString());
    if (variant.priceSell) {
      variantFormData.append('priceSell', variant.priceSell.toString());
    }
    variantFormData.append('quantity', variant.quantity.toString());
    variantFormData.append('status', variant.status.toString());
    if (variant.colorId) {
        variantFormData.append('colorId', variant.colorId.toString());
    }
    if (variant.sizeId) {
        variantFormData.append('sizeId', variant.sizeId.toString());
    }

    return variantFormData;
  }
  private getProductId(): number {
    let productId: number = 0;
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id !== null) {
        productId = Number(id);
      }
    });
    return productId;
  }

  addOrUpdateProduct() {
    const formData = this.createProductFormData(this.isUpdate ? this.getProductId() : 0);

    if (this.isUpdate) {
      this.updateProduct(formData);
    } else {
      this.addProduct(formData);
    }
  }

  private createProductFormData(productId?: number): FormData {
    const formData = new FormData();
    if (productId) {
      formData.append('id', productId.toString());
    }
    formData.append('name', this.productForm.value.name);
    if(this.productForm.value.description){
      formData.append('description', this.productForm.value.description);
    }
    this.productForm.value.selectedCategories.forEach((categoryId: number) => {
      formData.append('categoryIds', categoryId.toString());
    });
    formData.append('status', this.productForm.value.status);

    const imageFormArray = this.productForm.get('imageFile') as FormArray;
    imageFormArray.controls.forEach((control: any) => {
      const file = control.value;
      if (file instanceof File) {
        formData.append('imageFile', file);
      } else {
        console.error('Dự kiến là một file, nhưng nhận được:', file);
      }
    });

    return formData;
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
        this.visible = false;
        this.toastService.success('Thêm màu thành công.');
        this.loadColors();

      },
      error: () => {
        this.toastService.error('Lỗi khi thêm màu.');
      },
    });
  }
}

