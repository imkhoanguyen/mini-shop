import { Component, EventEmitter } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
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
import {
  Product,
  ProductAdd,
  ProductUpdate,
} from '../../../../_models/product.module';
import { ActivatedRoute, Router } from '@angular/router';
import { ColorPickerModule } from 'primeng/colorpicker';
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
    ColorPickerModule,
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
  code!: string;
  name!: string;
  colors: Color[] = [];
  colorId: number = 0;
  searchColor: string = '';

  selectedMainImage!: string;
  btnText: string = 'Thêm';
  visible: boolean = false;

  constructor(
    private builder: FormBuilder,
    private productService: ProductService,
    private messageService: MessageService,
    private categoryService: CategoryService,
    private variantService: VariantService,
    private imageService: ImageService,
    private sizeService: SizeService,
    private colorService: ColorService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeProductForm();
    this.loadCategories();
    this.loadSizes();
    this.loadColors();
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      if (id) {
        this.productService.getProductById(id).subscribe((product) => {
          console.log('Product:', product);
          this.productForm.patchValue({
            name: product.name,
            selectedCategories: product.categoryIds,
            description: product.description,
          });
          const variantsFormArray = this.variantsForm.get(
            'variants'
          ) as FormArray;
          product.variants.forEach((variant) => {
            const variantGroup = this.builder.group({
              id: [variant.id],
              price: [variant.price],
              priceSell: [variant.priceSell],
              quantity: [variant.quantity],
              sizeId: [variant.sizeId],
              colorId: [variant.colorId],
              imageUrls: [variant.imageUrls],
              selectedMainImage: variant.imageUrls.find(
                (image) => image.isMain
              ),
            });
            variantsFormArray.push(variantGroup);
          });
        });
      }
    });
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
  showColorDialog() {
    this.visible = true;
  }
  updateColorFromInput(value: string) {
    this.code = value;
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

  addOrUpdateProduct() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id == 0) {
      const productAdd: ProductAdd = {
        name: this.productForm.value.name,
        description: this.productForm.value.description,
        categoryIds: this.productForm.value.selectedCategories,
      };
      this.productService.addProduct(productAdd).subscribe(
        (productResponse: any) => {
          const productId = productResponse?.id;
          if (productId) {
            this.addVariants(productId);
          } else {
            console.error('Product ID not received:', productResponse);
          }
          this.showMessage(
            'success',
            'Thành Công',
            'Thêm sản phẩm thành công.'
          );
        },
        (error) => {
          this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm sản phẩm.');
          console.error('Failed to add product', error);
        }
      );
    } else {
      const productUpdate: ProductUpdate = {
        id: id,
        name: this.productForm.value.name,
        description: this.productForm.value.description,
        categoryIds: this.productForm.value.selectedCategories,
        status: 1,
      };
      this.productService.updateProduct(productUpdate).subscribe(
        (productResponse: any) => {
          const productId = productResponse?.id;
          if (productId) {
            console.log('Product ID received:', productId);
            this.addVariants(productId);
          } else {
            console.error('Product ID not received:', productResponse);
          }
          this.showMessage(
            'success',
            'Thành Công',
            'Cập nhật sản phẩm thành công.'
          );
        },
        (error) => {
          this.showMessage('error', 'Thất Bại', 'Lỗi khi cập nhật sản phẩm.');
          console.error('Failed to update product', error);
        }
      );
    }
  }
  addVariants(productId: number) {
    this.variants.controls.forEach((variantGroup, index) => {
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
        (variantResponse) => {
          const variantId = variantResponse?.id;
          console.log('variantId', variantId);
          this.addImages(variantId, index);
          this.showMessage(
            'success',
            'Thành Công',
            'Biến thể được thêm thành công!'
          );
        },
        (error) =>
          this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm biến thể.')
      );
    });
  }
  updateVariant() {
    this.variants.controls.forEach((variantGroup, index) => {
      const variantUpdate: Variant = {
        id: variantGroup.value.id,
        price: variantGroup.value.price,
        priceSell: variantGroup.value.priceSell,
        quantity: variantGroup.value.quantity,
        sizeId: variantGroup.value.sizeId,
        colorId: variantGroup.value.colorId,
        imageUrls: variantGroup.value.imageUrls,
      };
      this.variantService.updateVariant(variantUpdate).subscribe(
        (variantResponse : any) => {
          const variantId = variantResponse?.id;
          console.log('variantId', variantId);
          this.addImages(variantId, index);
          this.showMessage(
            'success',
            'Thành Công',
            'Biến thể được cập nhật thành công!'
          );
        },
        (error) =>
          this.showMessage('error', 'Thất Bại', 'Lỗi khi cập nhật biến thể.')
      );
    });
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

  getFormControlName(index: number): string {
    return `imageUrls.${index}.isMain`;
  }
  addImages(variantId: number, variantIndex: number) {
    const variant = this.variants.at(variantIndex);
    const variantImages = variant.get('imageUrls') as FormArray;
    const selectedMainImage = variant.get('selectedMainImage')?.value;
    variantImages.controls.forEach((image: any) => {
      const file = image.value.file;
      const isMain = image.value.name === selectedMainImage;
      const imageAdd = new FormData();
      imageAdd.append('url', file);
      imageAdd.append('isMain', isMain.toString());
      imageAdd.append('variantId', variantId.toString());

      this.imageService.addImage(imageAdd).subscribe(
        (response: any) => {
          this.showMessage(
            'success',
            'Thành Công',
            'Ảnh được thêm thành công!'
          );
        },
        (error) => {
          this.showMessage('error', 'Thất Bại', 'Lỗi khi thêm ảnh.');
          console.error('Failed to upload image:', error);
        }
      );
    });
  }
  updateImage(){
    
  }
  //-----------Variant------------
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
    if (this.productForm.valid && this.variantsForm.valid) {
      this.addOrUpdateProduct();
      this.router.navigateByUrl('/admin/product');
    } else {
      this.showMessage('error', 'Thất Bại', 'Vui lòng điền đầy đủ thông tin.');
    }
  }
}
