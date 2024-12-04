import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductUserLikedComponent } from './product-user-liked.component';

describe('ProductUserLikedComponent', () => {
  let component: ProductUserLikedComponent;
  let fixture: ComponentFixture<ProductUserLikedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductUserLikedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductUserLikedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
