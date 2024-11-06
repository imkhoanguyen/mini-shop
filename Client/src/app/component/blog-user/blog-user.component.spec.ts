import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlogUserComponent } from './blog-user.component';

describe('BlogUserComponent', () => {
  let component: BlogUserComponent;
  let fixture: ComponentFixture<BlogUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BlogUserComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlogUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
