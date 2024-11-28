import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../_services/order.service';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ChartModule } from 'primeng/chart';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-statistic',
  standalone: true,
  imports: [
    DropdownModule,
    InputTextModule,
    ChartModule,
    CalendarModule,
    FormsModule ,
    CommonModule
  ],
  templateUrl: './statistic.component.html',
  styleUrl: './statistic.component.css',
})
export class StatisticComponent implements OnInit{
  dropdownOptions = [
    { label: 'Hiện tại', value: '0' },
    { label: 'Ngày', value: '1' },
    { label: 'Tháng', value: '2' },
    { label: 'Năm', value: '3' },
  ];

  selectedOption: any;

  selectedDate: Date | null = null;

  chartData: any;
  chartOptions: any;
  
  newDate:string='';

  revenueOrder:string='';
  countOrder:string='';

  constructor(
    private orderService: OrderService,
  ) {}
  ngOnInit(){
    this.selectedDate = new Date();
    this.createChart();
    this.loadChartRevenueByYear(this.selectedDate.getFullYear())
    this.loadRevenueToday()
    this.loadCountOrderToday()

  }
  createChart(){
    this.chartData = {
      labels: [],
      datasets: [
        {
          label: 'Doanh thu (VNĐ)',
          data: [],
          backgroundColor: '#42A5F5',
        },
      ],
    };
  }

  onDropdownChange(event: any) {
    // Lấy giá trị từ combobox
    const selectedTimeOption = event; 
  
    // Lấy giá trị từ lịch
    const selectedDateValue = this.selectedDate;
  
    if (selectedDateValue) {
  
      if (selectedTimeOption === '1') {
        this.loadRevenueDate(selectedDateValue)
        this.loadCountOrderDate(selectedDateValue)
      } 
      else if (selectedTimeOption === '2') {
        this.loadRevenueMonth(selectedDateValue.getFullYear(),selectedDateValue.getMonth() + 1)
        this.loadCountOrderMonth(selectedDateValue.getFullYear(),selectedDateValue.getMonth() + 1)
      }
      else if (selectedTimeOption === '3') {
        this.loadChartRevenueByYear(selectedDateValue.getFullYear())
        this.loadRevenueYear(selectedDateValue.getFullYear())
        this.loadCountOrderYear(selectedDateValue.getFullYear())
      }
      else{
        this.loadRevenueToday()
        this.loadCountOrderToday()
      }
    }
  }
  loadChartRevenueByYear(year:number){
    this.orderService.getRevenueOrderAllMonthInYear(year).subscribe(
      (response: any) => {
        // Extract data for the chart
        const labels = response.breakdown.map((entry: any) => `${entry.month}`);
        const data = response.breakdown.map((entry: any) => entry.revenue);

        // Update chart data
        this.chartData = {
          labels: labels,
          datasets: [
            {
              label: 'Doanh thu (VNĐ)',
              data: data,
              backgroundColor: '#42A5F5',
            },
          ],
        };
        this.chartOptions = {
          responsive: true,
          plugins: {
            legend: {
              position: 'top',
            },
          },
          scales: {
            x: {
              title: {
                display: true,
                text: `Biểu đồ doanh thu của tất cả các tháng trong ${year}`,
                font: {
                  size: 16,
                },
                padding: 10,
              },
            },
          },
        };
      },
      (error) => {
        console.log('Error loading revenue data:', error);
      }
    );
  }

  formattedDate(date:any){
    this.newDate=`${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`
  }

  loadRevenueToday(){
    this.orderService.getRevenueOrderToday().subscribe(
      (response: any) => {
        this.revenueOrder=response.totalRevenue
        console.log(this.revenueOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }
  loadCountOrderToday(){
    this.orderService.getCountOrderToday().subscribe(
      (response: any) => {
        this.countOrder=response.count;
        console.log(this.countOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }

  loadRevenueDate(date:any){
    this.formattedDate(date)
    this.orderService.getRevenueOrderDate(this.newDate).subscribe(
      (response: any) => {
        this.revenueOrder=response.totalRevenue
        console.log(this.revenueOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }

  loadCountOrderDate(date:any){
    this.formattedDate(date)
    this.orderService.getCountOrderDate(this.newDate).subscribe(
      (response: any) => {
        this.countOrder=response.count;
        console.log(this.countOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }

  loadRevenueMonth(year:number, month:number){
    this.orderService.getRevenueOrderMonth(year,month).subscribe(
      (response: any) => {
        this.revenueOrder=response.totalRevenue
        console.log(this.revenueOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }
  loadCountOrderMonth(year:number, month:number){
    this.orderService.getCountOrderMonth(year,month).subscribe(
      (response: any) => {
        this.countOrder=response.count;
        console.log(this.countOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }

  loadRevenueYear(year:number){
    this.orderService.getRevenueOrderYear(year).subscribe(
      (response: any) => {
        this.revenueOrder=response.totalRevenue
        console.log(this.revenueOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }
  loadCountOrderYear(year:number){
    this.orderService.getCountOrderYear(year).subscribe(
      (response: any) => {
        this.countOrder=response.count;
        console.log(this.countOrder)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }
  

}
