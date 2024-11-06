import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogUserDetailComponent } from './blog-user-detail.component';

describe('BlogUserDetailComponent', () => {
  let component: BlogUserDetailComponent;
  let fixture: ComponentFixture<BlogUserDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogUserDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogUserDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
