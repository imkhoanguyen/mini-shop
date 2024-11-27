import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../../_services/product.service';
import { ProductDto } from '../../../_models/product.module';
import { Pagination } from '../../../_models/pagination';
import { QuickviewProductComponent } from '../quickview-product/quickview-product.component';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { SizeService } from '../../../_services/size.service';
import { Size } from '../../../_models/size.module';
import { ColorService } from '../../../_services/color.service';
import { Color } from '../../../_models/color.module';
import { CategoryService } from '../../../_services/category.service';
import { CategoryDto } from '../../../_models/category.module';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    QuickviewProductComponent,
    PaginatorModule,
    ButtonModule,
    BreadcrumbModule,
    InputIconModule,
    IconFieldModule,
    InputTextModule,
    FormsModule,
    CommonModule,
    DropdownModule,
    MultiSelectModule,
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
})
export class ShopComponent implements OnInit {
  // init
  private productService = inject(ProductService);
  private sizeService = inject(SizeService);
  private colorService = inject(ColorService);
  private categoryService = inject(CategoryService);

  products: ProductDto[] = [];

  // define params
  params = {
    pageSize: 3,
    pageNumber: 1,
    search: '',
    selectedSize: [] as number[],
    selectedColor: 0,
    selectedCategory: [] as number[],
    orderBy: 'id_desc',
  };

  pagination: Pagination | undefined;

  // breadcrumb
  items: MenuItem[] | undefined;

  ngOnInit(): void {
    this.loadListProduct();
    this.items = [
      { label: 'Trang chủ', routerLink: '/home' },
      { label: 'Sản phẩm', routerLink: '/shop' },
    ];
    this.orderList = [
      { value: 'id_desc', display: 'Mới nhất' },
      { value: 'id', display: 'Cũ nhất' },
      { value: 'name', display: 'Tên (A-Z)' },
      { value: 'name_desc', display: 'Tên (Z-A)' },
      { value: 'price_desc', display: 'Giá cao đến thấp' },
      { value: 'price', display: 'Giá thấp đến cao' },
    ];
    this.getSize();
    this.getColor();
    this.getCategory();
  }

  loadListProduct() {
    this.productService.getProductsPagedList(this.params, false).subscribe({
      next: (response) => {
        if (response.items && response.pagination) {
          this.products = response.items || [];
          this.pagination = response.pagination;
        }
      },
      error: (er) => {
        console.log(er);
      },
    });
  }

  onPageChange(event: any) {
    this.params.pageNumber = event.page + 1;
    this.params.pageSize = event.rows;
    this.loadListProduct();
  }

  @ViewChild(QuickviewProductComponent)
  quickViewProductComponent!: QuickviewProductComponent;
  openQuickViewProduct(productId: number) {
    if (this.quickViewProductComponent) {
      this.quickViewProductComponent.productId = productId;
      this.quickViewProductComponent.loadProduct();
    } else {
      console.error('QuickviewProductComponent is not initialized yet');
    }
  }

  // seach, filter
  visibleSearch = false;
  searchString: string = '';

  toggleSeachBox() {
    if (this.visibleSearch === true) {
      this.visibleSearch = false;
      this.searchString = '';
    } else this.visibleSearch = true;
  }

  visibleFilter = false;
  toggleFilterBox() {
    if (this.visibleFilter === true) this.visibleFilter = false;
    else this.visibleFilter = true;
  }

  //ordeby
  orderBy: any = null;
  orderList: any[] = [];

  //color
  selectedSizes: Size[] = [];
  listSize: Size[] = [];
  getSize() {
    this.sizeService.getAllSizes().subscribe({
      next: (response) => {
        this.listSize = response;
      },
      error: (er) => {
        console.log(er);
      },
    });
  }

  //color
  selectedColor: Color | null = null;
  listColor: Color[] = [];
  getColor() {
    this.colorService.getAllColors().subscribe({
      next: (response) => {
        this.listColor = response;
      },
      error: (er) => console.log(er),
    });
  }

  //category
  selectedCategory: CategoryDto[] = [];
  listCategory: CategoryDto[] = [];
  getCategory() {
    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.listCategory = response;
      },
      error: (er) => console.log(er),
    });
  }

  applyFilter() {
    // Reset params trước khi áp dụng filter
    this.params = {
      ...this.params, // giữ lại các giá trị mặc định
      search: '',
      selectedSize: [] as number[],
      selectedColor: 0,
      selectedCategory: [] as number[],
    };

    // handle color
    if (this.selectedColor != null) {
      this.params.selectedColor = this.selectedColor.id;
    }

    // handle size
    if (this.selectedSizes && this.selectedSizes.length > 0) {
      this.params.selectedSize = this.selectedSizes.map((s) => s.id);
    }

    //handle cate
    if (this.selectedCategory && this.selectedCategory.length > 0) {
      this.params.selectedCategory = this.selectedCategory.map((s) => s.id);
    }

    //handle orderby
    if (this.orderBy !== null) {
      this.params.orderBy = this.orderBy.value;
    }

    //handle searchString
    if (this.searchString) {
      this.params.search = this.searchString;
    }

    console.log(this.params);
    this.loadListProduct();
  }
}
