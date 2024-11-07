// import { Component, OnInit } from '@angular/core';
// import { MultiSelectModule } from 'primeng/multiselect';
// import { Category } from '../../../../_models/category.module';
// import {
//   FormArray,
//   FormBuilder,
//   FormGroup,
//   FormsModule,
//   ReactiveFormsModule,
//   Validators,
// } from '@angular/forms';
// import { CategoryService } from '../../../../_services/category.service';
// import { MessageService } from 'primeng/api';
// import { ActivatedRoute, Router } from '@angular/router';
// import { CommonModule } from '@angular/common';
// import { ToastModule } from 'primeng/toast';
// import { InputTextModule } from 'primeng/inputtext';
// import { InputTextareaModule } from 'primeng/inputtextarea';
// import { ProductAdd, ProductUpdate } from '../../../../_models/product.module';
// import { StepperModule } from 'primeng/stepper';
// import { Size } from '../../../../_models/size.module';
// import { Color } from '../../../../_models/color.module';
// import { SizeService } from '../../../../_services/size.service';
// import { ColorService } from '../../../../_services/color.service';
// import { DropdownModule } from 'primeng/dropdown';
// import { ButtonModule } from 'primeng/button';
// import { CheckboxModule } from 'primeng/checkbox';
// import { DialogModule } from 'primeng/dialog';
// import { FileUploadModule } from 'primeng/fileupload';
// import { BadgeModule } from 'primeng/badge';
// import { InputNumberModule } from 'primeng/inputnumber';
// import { RadioButtonModule } from 'primeng/radiobutton';
// import { ColorPickerModule } from 'primeng/colorpicker';
// import { ProductService } from '../../../../_services/product.service';
// import { Variant, VariantAdd } from '../../../../_models/variant.module';
// import { VariantService } from '../../../../_services/variant.service';
// import { ImageService } from '../../../../_services/image.service';
// import { Image } from '../../../../_models/image.module';
// @Component({
//   selector: 'app-add-product',
//   standalone: true,
//   imports: [
//     MultiSelectModule,
//     ReactiveFormsModule,
//     FormsModule,
//     CommonModule,
//     ToastModule,
//     InputTextModule,
//     InputTextareaModule,
//     StepperModule,
//     DropdownModule,
//     ButtonModule,
//     CheckboxModule,
//     CommonModule,
//     InputTextModule,
//     DialogModule,
//     ToastModule,
//     ReactiveFormsModule,
//     FormsModule,
//     FileUploadModule,
//     BadgeModule,
//     InputNumberModule,
//     MultiSelectModule,
//     RadioButtonModule,
//     ColorPickerModule,
//   ],
//   templateUrl: './product-form.component.html',
//   styleUrl: './product-form.component.css',
//   providers: [MessageService],
// })
// export class ProductFormComponent implements OnInit {
//   productForm!: FormGroup;
//   imageForm!: FormGroup;
//   variantsForm!: FormGroup;

//   //Category
//   categories: Category[] = [];
//   selectedCategories: any[] = [];
//   searchCategory: string = '';
//   //Size
//   sizes: Size[] = [];
//   sizeId: number = 0;
//   searchSize: string = '';
//   //color
//   code!: string;
//   name!: string;
//   colors: Color[] = [];
//   colorId: number = 0;
//   searchColor: string = '';

//   //Image
//   selectedImages: any[] = [];
//   mainImage: { variantIndex: number; imageIndex: number } | null = null;
//   visible: boolean = false;
//   isUpdate: boolean = false;

//   constructor(
//     private categoryService: CategoryService,
//     private variantService: VariantService,
//     private productService: ProductService,
//     private imageService: ImageService,
//     private sizeService: SizeService,
//     private colorService: ColorService,
//     private messageService: MessageService,
//     private router: Router,
//     private fb: FormBuilder,
//     private route: ActivatedRoute
//   ) {}
//   ngOnInit(): void {
//     this.initializeProductForm();
//     this.loadCategories();
//     this.initializeVariantForm();
//     this.loadColors();
//     this.loadSizes();
//     this.route.paramMap.subscribe((params) => {
//       const id = Number(params.get('id'));
//       if (id) {
//         this.getProductToUpdate(id);
//       }
//     });
//   }

//   private showMessage(severity: string, detail: string): void {
//     const summary: string = '';
//     const life: number = 3000;
//     if (severity === 'error') {
//       this.messageService.add({ severity, summary: 'Thất Bại', detail, life });
//     } else if (severity === 'success') {
//       this.messageService.add({
//         severity,
//         summary: 'Thành Công',
//         detail,
//         life,
//       });
//     } else {
//       this.messageService.add({ severity, summary: 'Cảnh báo', detail, life });
//     }
//   }
//   //Update Product
//   getProductToUpdate(id: number) {
//     this.productService.getProductById(id).subscribe(
//       (data: any) => {
//         console.log('Product data:', data);
//         this.productForm.patchValue({
//           id: data.id,
//           name: data.name,
//           selectedCategories: data.categoryIds,
//           description: data.description,
//         });
//         this.loadVariants(data.variants);
//         this.isUpdate = true;
//       },
//       (error) => {
//         this.showMessage('error', 'Lỗi khi tải sản phẩm.');
//       }
//     );
//   }
//   loadVariants(variants: Variant[]) {
//     variants.forEach((variant, variantIndex) => {
//       console.log('Variant:', variant);
//       const variantGroup = this.fb.group({
//         id: variant.id,
//         price: variant.price,
//         priceSell: variant.priceSell,
//         quantity: variant.quantity,
//         sizeId: variant.sizeId,
//         colorId: variant.colorId,
//         imageUrls: this.fb.array([]),
//       });
//       if (variant.imageUrls) {
//         const imageUrlsArray = variant.imageUrls.map((image) =>
//           this.fb.group({
//             id: image.id,
//             url: image.url,
//             isMain: image.isMain,
//           })
//         );
//         variantGroup.setControl(
//           'imageUrls',
//           this.fb.array(imageUrlsArray) as FormArray
//         );
//         this.selectedImages[variantIndex] = variant.imageUrls.map((image) => {
//           return { src: image.url, file: null };
//         });
//       }

//       this.variants.push(variantGroup);
//     });
//   }
//   initializeVariantForm() {
//     this.variantsForm = this.fb.group({
//       variants: this.fb.array([]),
//     });
//   }
//   // ----------------- Category -----------------
//   loadCategories(): void {
//     this.categoryService.getAllCategories().subscribe(
//       (data: Category[]) => {
//         this.categories = data;
//       },
//       (error) => {
//         this.showMessage('error', 'Lỗi khi tải danh mục.');
//       }
//     );
//   }
//   onCategoryFilter(event: any) {
//     this.searchCategory = event.filter;
//   }
//   addCategory() {
//     const categoryName = this.searchCategory.trim();
//     if (!categoryName) {
//       this.showMessage('error', 'Tên danh mục không được trống.');
//       return;
//     }
//     const data: Category = {
//       id: 0,
//       name: categoryName,
//       created: new Date(),
//       updated: new Date(),
//     };
//     this.categoryService.addCategory(data).subscribe(
//       (res: any) => {
//         this.showMessage('success', 'Thêm danh mục thành công.');
//         this.loadCategories();
//       },
//       (error) => {
//         this.showMessage('error', 'Lỗi khi thêm danh mục.');
//       }
//     );
//   }

//   // ----------------- Size -----------------
//   loadSizes(): void {
//     this.sizeService.getAllSizes().subscribe(
//       (data: Size[]) => {
//         this.sizes = data;
//       },
//       (error) => {
//         this.showMessage('error', 'Lỗi khi tải kích thước.');
//       }
//     );
//   }
//   onSizeFilter(event: any) {
//     this.searchSize = event.filter;
//   }
//   addSize() {
//     const sizeName = this.searchSize.trim();
//     if (!sizeName) {
//       this.showMessage('error', 'Tên kích thước không được trống.');
//       return;
//     }
//     const data: Size = {
//       id: 0,
//       name: sizeName,
//     };
//     this.sizeService.addSize(data).subscribe({
//       next: (res) => {
//         this.showMessage('success', 'Thêm kích thước thành công.');
//         this.loadSizes();
//       },
//       error: (error) => {
//         this.showMessage('error', 'Lỗi khi thêm kích thước.');
//       },
//     });
//   }
//   // ----------------- Color -----------------
//   showColorDialog() {
//     this.visible = true;
//   }
//   updateColorFromInput(value: string) {
//     this.code = value;
//   }

//   loadColors(): void {
//     this.colorService.getAllColors().subscribe(
//       (data: Color[]) => {
//         this.colors = data;
//       },
//       (error) => {
//         this.showMessage('error', 'Lỗi khi tải màu sắc.');
//       }
//     );
//   }
//   onColorFilter(event: any) {
//     this.searchColor = event.filter;
//   }

//   addColor() {
//     if (!this.name.trim() || !this.code) {
//       this.showMessage('error', 'Vui lòng điền đầy đủ thông tin.');
//       return;
//     }
//     const data: Color = { id: 0, name: this.name, code: this.code };
//     this.colorService.addColor(data).subscribe({
//       next: (res) => {
//         this.showMessage('success', 'Thêm màu thành công.');
//         this.loadColors();
//         this.visible = false;
//       },
//       error: (error) => {
//         this.showMessage('error', 'Lỗi khi thêm màu.');
//       },
//     });
//   }
//   // ----------------- Image -----------------
//   onFileSelected(event: Event, variantIndex: number) {
//     const input = event.target as HTMLInputElement;
//     const imageUrls = this.variants
//       .at(variantIndex)
//       .get('imageUrls') as FormArray;
//     if (input?.files) {
//       const files = Array.from(input.files);
//       if (!this.selectedImages[variantIndex]) {
//         this.selectedImages[variantIndex] = [];
//       }

//       files.forEach((file) => {
//         const reader = new FileReader();
//         reader.onload = (e: any) => {
//           const imagePreview = {
//             src: e.target.result,
//             file: file,
//           };
//           this.selectedImages[variantIndex].push(imagePreview);
//           imageUrls.push(
//             this.fb.group({
//               name: file.name,
//               url: e.target.result,
//               file: file,
//               isMain: false,
//             })
//           );
//         };
//         reader.readAsDataURL(file);
//       });
//     }
//   }
//   selectMainImage(variantIndex: number, imageIndex: number) {
//     const imageUrls = (this.variantsForm.get('variants') as FormArray)
//       .at(variantIndex)
//       .get('imageUrls') as FormArray;
//     imageUrls.controls.forEach((imageControl: any, i: number) => {
//       imageControl.patchValue({ isMain: i === imageIndex });
//     });
//   }
//   isMainImage(variantIndex: number, imageIndex: number) {
//     return (this.variantsForm.get('variants') as FormArray)
//       .at(variantIndex)
//       .get('imageUrls')?.value[imageIndex].isMain;
//   }
//   removeImage(variantIndex: number, imageIndex: number) {
//     this.selectedImages[variantIndex].splice(imageIndex, 1);
//     const imageUrls = (this.variantsForm.get('variants') as FormArray)
//       .at(variantIndex)
//       .get('imageUrls') as FormArray;
//     imageUrls.removeAt(imageIndex);

//     if (
//       this.mainImage?.variantIndex === variantIndex &&
//       this.mainImage?.imageIndex === imageIndex
//     ) {
//       this.mainImage = null;
//     } else if (
//       this.mainImage?.variantIndex === variantIndex &&
//       this.mainImage?.imageIndex > imageIndex
//     ) {
//       this.mainImage.imageIndex--;
//     }
//   }
//   addImages(variantId: number, variantIndex: number) {
//     const variant = this.variants.at(variantIndex);
//     const variantImages = variant.get('imageUrls') as FormArray;

//     const hasMainImage = variantImages.controls.some(
//       (image: any) => image.get('isMain').value
//     );

//     if (!hasMainImage) {
//       this.showMessage('error', 'Vui lòng chọn một ảnh chính.');
//       return;
//     }
//     variantImages.controls.forEach((image: any, index: number) => {
//       const file = image.value.file;
//       const isMain = this.isMainImage(variantIndex, index);

//       const formData = new FormData();
//       formData.append('url', file);
//       formData.append('isMain', isMain.toString());
//       formData.append('variantId', variantId.toString());

//       this.imageService.addImage(formData).subscribe(
//         (response: any) => {
//           this.showMessage('success', 'Ảnh được thêm thành công!');
//           setTimeout(() => {
//             this.router.navigate(['/admin/product']);
//           }, 2000);
//         },
//         (error) => {
//           this.showMessage('error', 'Lỗi khi thêm ảnh.');
//           console.error('Failed to upload image:', error);
//         }
//       );
//     });
//   }
//   deleteImages(images: number){
//     this.imageService.deleteImage(images).subscribe({
//       next: () => {
//         console.log(`Ảnh ${images} đã bị xóa thành công.`);
//         this.showMessage('success', 'Xóa ảnh thành công.');
//       },
//       error: () => {
//         console.log(`Lỗi khi xóa ảnh ${images}.`);
//         this.showMessage('error', 'Lỗi khi xóa ảnh.');
//       }
//     });
//   }

//   // ----------------- Variant ----------------

//   get variants(): FormArray {
//     return this.variantsForm.get('variants') as FormArray;
//   }
//   createVariant(): void {
//     const variantGroup = this.fb.group({
//       price: this.fb.control(null, Validators.required),
//       priceSell: this.fb.control(null),
//       quantity: this.fb.control(null, Validators.required),
//       colorId: this.fb.control(null),
//       sizeId: this.fb.control(null),
//       imageUrls: this.fb.array([]),
//     });
//     this.variants.push(variantGroup);
//   }
//   removeVariant(index: number): void {
//     const variant = this.variants.at(index).value;
//     if(variant.id){
//       this.deleteVariant(variant);
//     }
//     this.variants.removeAt(index);
//   }
//   onSubmit(): void {
//     console.log('productForm', this.productForm.value);
//     console.log('variantsForm', this.variantsForm.value);
//     console.log(
//       'selectedCategories',
//       this.productForm.value.selectedCategories
//     );

//     if (this.productForm.valid && this.variantsForm.valid) {
//       this.addOrUpdateProduct();
//     } else {
//       this.showMessage('error', 'Vui lòng điền đầy đủ thông tin.');
//     }
//   }
//   initializeProductForm() {
//     this.productForm = this.fb.group({
//       id: [0],
//       name: ['', [Validators.required]],
//       description: [''],
//       selectedCategories: [[], [Validators.required]],
//     });
//   }
//   addVariants(productId: number) {
//     this.variants.controls.forEach((variantGroup, index) => {
//       const variantAdd: VariantAdd = {
//         price: variantGroup.value.price,
//         priceSell: variantGroup.value.priceSell,
//         quantity: variantGroup.value.quantity,
//         sizeId: variantGroup.value.sizeId,
//         colorId: variantGroup.value.colorId,
//         productId: productId,
//       };

//       console.log('variant', variantAdd);
//       this.variantService.addVariant(variantAdd).subscribe(
//         (variantResponse) => {
//           const variantId = variantResponse?.id;
//           console.log('variantId', variantId);
//           this.addImages(variantId, index);
//           this.showMessage('success', 'Biến thể được thêm thành công!');
//         },
//         (error) => this.showMessage('error', 'Lỗi khi thêm biến thể.')
//       );
//     });
//   }
//   updateVariant(productId: number) {
//     this.variants.controls.forEach((variantGroup, index) => {
//       const variantId = variantGroup.value.id;
//       if (!variantId) {
//         this.addVariants(productId);
//         return;
//       } else {
//         const variantUpdate: Variant = {
//           id: variantId,
//           price: variantGroup.value.price,
//           priceSell: variantGroup.value.priceSell,
//           quantity: variantGroup.value.quantity,
//           sizeId: variantGroup.value.sizeId,
//           colorId: variantGroup.value.colorId,
//           imageUrls: variantGroup.value.imageUrls,
//           productId: productId,
//         };
//         console.log('variantUpdate', variantUpdate);
//         this.variantService.updateVariant(variantUpdate).subscribe(
//           (variantResponse: any) => {
//             if(this.selectedImages[index]){
//               this.addImages(variantUpdate.id, index);
//             }
//             const oldImages = variantGroup.value.imageUrls;
//             oldImages.forEach((image: any) => {
//               if (image.markedForDeletion) {
//                 this.deleteImages(image.id);
//               }
//             });
//             this.showMessage(
//               'success',
//               'Biến thể đã được cập nhật thành công!'
//             );
//           },
//           (error) => this.showMessage('error', 'Lỗi khi cập nhật biến thể.')
//         );
//       }
//     });
//   }
//   deleteVariant(variant: Variant) {
//     this.variantService.deleteVariant(variant).subscribe({
//       next: () => {
//         console.log(`Biến thể ${variant.id} đã bị xóa thành công.`);
//         this.showMessage('success', 'Xóa biến thể thành công.');
//       },
//       error: () => {
//         console.log(`Lỗi khi xóa biến thể ${variant.id}.`);
//         this.showMessage('error', 'Lỗi khi xóa biến thể.');
//       },
//     });
//   }

//   // ----------------- ProductCU ----------------

//   addOrUpdateProduct() {
//     const productAdd: ProductAdd = {
//       name: this.productForm.value.name,
//       description: this.productForm.value.description,
//       categoryIds: this.productForm.value.selectedCategories,
//     };

//     if (this.isUpdate) {
//       this.route.paramMap.subscribe((params) => {
//         const productId = Number(params.get('id'));
//         console.log('Product ID:', productId);
//         const productUpdate: ProductUpdate = {
//           id: productId,
//           name: this.productForm.value.name,
//           description: this.productForm.value.description,
//           categoryIds: this.productForm.value.selectedCategories,
//           status: 0,
//         };
//         this.productService.updateProduct(productUpdate).subscribe(
//           (response: any) => {

//             this.updateVariant(productId);
//             this.showMessage('success', 'Cập nhật sản phẩm thành công.');
//           },
//           (error) => {
//             this.showMessage('error', 'Lỗi khi cập nhật sản phẩm.');
//           }
//         );
//       });
//     } else {
//       this.productService.addProduct(productAdd).subscribe(
//         (response: any) => {
//           const productId = response.id;
//           this.addVariants(productId);
//           this.showMessage('success', 'Thêm sản phẩm thành công.');
//         },
//         (error) => {
//           this.showMessage('error', 'Lỗi khi thêm sản phẩm.');
//         }
//       );
//     }
//   }
// }
