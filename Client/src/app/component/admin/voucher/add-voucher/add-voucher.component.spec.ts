import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVoucherComponent } from './add-voucher.component';

describe('AddVoucherComponent', () => {
  let component: AddVoucherComponent;
  let fixture: ComponentFixture<AddVoucherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddVoucherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddVoucherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
