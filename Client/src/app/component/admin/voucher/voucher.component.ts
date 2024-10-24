import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import { TableModule, TableRowCollapseEvent, TableRowExpandEvent } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { ToastModule } from 'primeng/toast';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Voucher } from '../../../_models/voucher.module';
import { VoucherService } from '../../../_services/voucher.service';
import { RouterModule } from '@angular/router';
import { InputSwitchModule } from 'primeng/inputswitch';
@Component({
  selector: 'app-voucher',
  standalone: true,
  imports: [
    TabViewModule,
    TagModule,
    TerminalModule,
    TableModule,
    ButtonModule,
    RatingModule,
    ToastModule,
    CommonModule,
    FormsModule,
    RouterModule,
    InputSwitchModule
  ],
  templateUrl: './voucher.component.html',
  styleUrls: ['./voucher.component.css'],
  providers: [MessageService],
})
export class VoucherComponent implements OnInit {
  vouchers: Voucher[] = []; // Mảng chứa danh sách voucher
  expandedRows: { [key: number]: boolean } = {};
  searchQuery: string = '';
  constructor(
    private voucherService: VoucherService,
  ) {}

  ngOnInit() {
    this.loadVouchers();
    
  }

  // Tải danh sách voucher từ service
  loadVouchers() {
    this.voucherService.getAllVoucher().subscribe(
      (data: Voucher[]) => {
        this.vouchers = data;
        this.checkVoucherValidity();
      },
      (error) => {
        console.error('Error fetching ', error);
      }
    );
  }

  checkVoucherValidity() {
    const currentDate = new Date();
    this.vouchers.forEach((voucher) => {
      const voucherEndDate = new Date(voucher.end_date);
      if (voucherEndDate < currentDate && voucher.is_active) {
        voucher.is_active = false;
        this.onActiveChange(voucher)
        // console.log("hết hạn"+voucher.id)
      } 
      
    });
  }

  onActiveChange(voucher:any) {
    // console.log(`Voucher ID: ${voucher.id} is now ${voucher.is_active ? 'active' : 'inactive'}`);
    if(!voucher.is_active){
      this.voucherService.deleteVoucher(voucher.id).subscribe(
        (response)=>{
          console.log('Voucher deleted successfully:', response);
        },
        (error) => {
          console.error('Delete failed ', error);
        }
      )
    }
    else{
      // console.log(voucher.id)
      this.voucherService.restoreVoucher(voucher.id).subscribe(
        (response)=>{
          console.log('Voucher restored successfully:', response);
        },
        (error) => {
          console.error('Restore failed ', error);
        }
      )
    }

  }
  
  get filteredVouchers() {
    if (!this.searchQuery) {
      return this.vouchers; // If no search query, return all vouchers
    }
    return this.vouchers.filter(voucher =>
      voucher.title.toLowerCase().includes(this.searchQuery.toLowerCase()) 
      // || voucher.description.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
  
}