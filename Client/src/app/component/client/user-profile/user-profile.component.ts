import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../../_services/account.service';
import { User } from '../../../_models/user.module';
import { AddressService } from '../../../_services/address.service';
import { Address } from '../../../_models/address.module';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { CalendarModule } from 'primeng/calendar';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from '../../../_services/toastr.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { switchMap } from 'rxjs';
@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule,
            DialogModule,
            ToastModule,
            FormsModule,
            ReactiveFormsModule,
            CalendarModule,
            ConfirmDialogModule],
  providers: [ConfirmationService],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  currentUser!: User;
  addressUser: Address[] = [];
  addressForm!: FormGroup;
  userForm!: FormGroup;

  dates: Date[] | undefined;

  headerText: string = 'Thêm địa chỉ mới';
  btnText: string = 'Thêm';
  visible: boolean = false;
  selectedFile: { src: string; file: File; } | undefined;
  accountService = inject(AccountService);
  addressService = inject(AddressService);
  toastrService = inject(ToastrService);
  confirmationService = inject(ConfirmationService);
  constructor(private builder: FormBuilder) {
    const userJson = localStorage.getItem('user');
    if(userJson){
      const user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(user);
    }

  }
  ngOnInit(): void {
    const user = this.accountService.getCurrentUser();

    if (user) {
      this.currentUser = user;
      this.loadAddress(this.currentUser.id);

      this.addressForm = this.initializeAddress();
      this.userForm = this.initializeUser();
    }

  }
  initializeAddress(){
    return this.builder.group({
      id: [0],
      city: ['', Validators.required],
      district: ['', Validators.required],
      street: ['', Validators.required],
      fullName: ['', Validators.required],
      phone: ['', Validators.required],
      appUserId: [this.currentUser.id]
    })
  }

  initializeUser(){
    return this.builder.group({
      id: [this.currentUser.id],
      fullName: [this.currentUser.fullName, Validators.required],
      userName: [this.currentUser.userName, Validators.required],
      email: [this.currentUser.email, [Validators.required, Validators.email]],

      avatar: ['']
    })
  }
  loadAddress(userId: string){
    this.addressService.getAllAddressByUserId(userId).subscribe((res: Address[]) => {
      this.addressUser = res;
      console.log(this.addressUser);
    });
  }

  openDialog(address?: Address){
    this.visible = true;
    if (address) {
      this.headerText = 'Cập nhật địa chỉ';
      this.btnText = 'Cập nhật';
      this.addressForm.patchValue(address);
    } else {
      // Đặt lại giá trị mặc định khi thêm mới
      this.addressForm.reset({
          id: 0,
          city: '',
          district: '',
          street: '',
          appUserId: this.currentUser.id
      });
      this.headerText = 'Thêm địa chỉ mới';
      this.btnText = 'Thêm';
    }
  }

  closedDialog(){
    this.visible = false;

  }
  confirmDelete(event: Event, address: Address){
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Are you sure that you want to proceed?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptIcon:"none",
      rejectIcon:"none",
      rejectButtonStyleClass:"p-button-text",
      accept: () => {
        this.addressService.deleteAddress(address.id).subscribe({
          next: () =>{
            this.toastrService.success("Xóa địa chỉ thành công");
          },
          error: () => {
            this.toastrService.success("Xóa địa chỉ thất bại");
          }
        })
      },
      reject: () => {

      }
  });
  }
  onFileSelected(event: any) {
    const file: FileList = event.target.files;
    if (file && file[0]) {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        this.selectedFile = {
          src: e.target.result,
          file: file[0],
        };
      };

      reader.readAsDataURL(file[0]);
    }
  }
  submit(): void {
    if (this.userForm.invalid) {
      this.toastrService.error("Vui lòng điền đầy đủ thông tin");
      return;
    }

    const formData = new FormData();
    formData.append('id', this.userForm.get('id')?.value);
    formData.append('fullName', this.userForm.get('fullName')?.value);
    formData.append('userName', this.userForm.get('userName')?.value);
    formData.append('email', this.userForm.get('email')?.value);

    if (this.selectedFile?.file) {
      formData.append('avatar', this.selectedFile.file);
    } else {
      formData.append('avatar', this.accountService.currentUser()?.avatar || '');
    }

    this.accountService.updateUser(formData).subscribe({
      next: (result) => {
        this.accountService.setCurrentUser(result);
        this.toastrService.success('Người dùng đã được cập nhật thành công.');
        this.visible = false;
      },
      error: (err) => {
        if (err.status === 400 && err.error) {
          const errorMessage = typeof err.error === 'string' ? err.error : 'Có lỗi xảy ra khi cập nhật.';
          this.toastrService.error(errorMessage);
        } else {
          this.toastrService.error('Có lỗi xảy ra khi cập nhật người dùng.');
        }
        console.error(err);
      }
    });
  }

  onSubmit(): void {
    //Address
    const address = { ...this.addressForm.value };
    console.log('Submit Address:', address);
    address.id = parseInt(this.addressForm.get("id")?.value, 10);
    if (address.id === 0) {
      console.log("iddđ =0")
        this.addressService.addAddress(address).subscribe({
          next: () => {
            this.toastrService.success("Thêm địa chỉ thành công")
            this.loadAddress(this.currentUser.id);
            this.visible = false;
          },
          error: () =>{
            this.toastrService.error("Lỗi khi thêm địa chỉ")
            this.visible = false;
          }
        });
    } else {
        this.addressService.updateAddress(address.id, address).subscribe({
          next: () => {
            this.toastrService.success("Cập nhật địa chỉ thành công")
            this.loadAddress(this.currentUser.id);
            this.visible = false;
          },
          error: () =>{
            this.toastrService.error("Lỗi khi cập nhật địa chỉ")
            this.visible = false;
          }
        });
    }


  }

}
