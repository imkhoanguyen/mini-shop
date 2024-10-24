import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { VoucherService } from '../../../../_services/voucher.service';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgIf } from '@angular/common';
import { Voucher } from '../../../../_models/voucher.module';

@Component({
  selector: 'app-add-voucher',
  standalone:true,
  templateUrl: './add-voucher.component.html',
  styleUrls: ['./add-voucher.component.css'],
  imports:[
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    CalendarModule,
    CheckboxModule,
    RouterModule,
    NgIf 
  ],
})
export class AddVoucherComponent implements OnInit {
  voucherForm!: FormGroup;
  currentUrl: string = '';
  id!:number;
  voucher!: Voucher;

  constructor(
    private formBuilder: FormBuilder,
    private voucherService: VoucherService,
    private router:Router,
    private route: ActivatedRoute
  ) {
    this.currentUrl= this.router.url
    this.voucherForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      percentage: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      start_date: ['', Validators.required],
      end_date: ['', Validators.required],
      
      is_active: [true]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = +params['id']; // Convert sang số nếu cần
      // console.log('Voucher ID:', this.id);
      if (this.currentUrl !== '/admin/voucher/new') {
        this.getVoucherById();
        
      }
    });
  }

  getVoucherById() {
    this.voucherService.getVoucherById(this.id).subscribe(
      (data: Voucher) => {
        this.voucher = data;
        // console.log(this.voucher)
        this.voucherForm.patchValue({
          title: this.voucher.title,
          description: this.voucher.description,
          percentage: this.voucher.percentage,
          start_date:new Date(this.voucher.start_date),
          end_date:new Date(this.voucher.end_date),
          is_active: this.voucher.is_active,
        });
      },
      (error) => {
        console.error('Error fetching', error);
      }
    );
  }

  onSubmit(): void {
    if (this.voucherForm.valid) {
      // Tạo một đối tượng FormData
      const formData = new FormData();

      formData.append('title', this.voucherForm.get('title')?.value);
      formData.append('description', this.voucherForm.get('description')?.value);
      formData.append('percentage', this.voucherForm.get('percentage')?.value.toString());
      formData.append('is_active', this.voucherForm.get('is_active')?.value ? 'true' : 'false');

      const startDate = new Date(this.voucherForm.get('start_date')?.value);
      const endDate = new Date(this.voucherForm.get('end_date')?.value);

      startDate.setDate(startDate.getDate() + 1);
      endDate.setDate(endDate.getDate() + 1); 
      startDate.setUTCHours(0, 0, 0, 0);
      endDate.setUTCHours(0, 0, 0, 0);

      formData.append('start_date', startDate.toISOString());
      formData.append('end_date', endDate.toISOString());

      const currentDate = new Date(); 
      formData.append('updated_at', currentDate.toISOString());

      if (this.currentUrl=='/admin/voucher/new'){
        formData.append('created_at', currentDate.toISOString());
        
        // Gọi service để thêm voucher với dữ liệu FormData
        this.voucherService.addVoucher(formData).subscribe({
          next: (response) => {
            console.log('Voucher added successfully:', response);
            this.voucherForm.reset();
          },
          error: (err) => {
            console.error('Failed to add voucher:', err);
          }
        });
      }
      else{   
        this.voucherService.updateVoucher(this.id,formData).subscribe({
          next:(response)=>{
            console.log('Voucher updated Successfully',response);
            this.router.navigate(['/admin/voucher']);
          },
          error:(err)=> {
            console.error('Failed to updated voucher:',err);
          }
        })
      }
    } else {
      console.log('Form is invalid!');
    }
  }
  
  checkDateRange() {
    const startDate = this.voucherForm.get('start_date')?.value;
    const endDate = this.voucherForm.get('end_date')?.value;

    // Kiểm tra nếu cả hai ngày đều được chọn
    if (startDate && endDate) {
      const start = new Date(startDate);
      const end = new Date(endDate);

      if (start > end) {
        alert('Ngày kết thúc phải sau ngày bắt đầu');
        this.voucherForm.get('end_date')?.setValue(null); // Reset end_date
      } else {
        // Nếu valid, có thể hiển thị thông báo hợp lệ (nếu cần)
        console.log('Ngày bắt đầu và ngày kết thúc hợp lệ');
      }
    }
  }
}
