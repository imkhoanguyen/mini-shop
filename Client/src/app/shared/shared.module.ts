import { NgModule } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { ToastModule } from 'primeng/toast';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [

    TabViewModule,
    TagModule,
    TerminalModule,
    TableModule,
    ButtonModule,
    RatingModule,
    ToastModule,
    CommonModule,

  ],
  providers: [],
})
export class SharedModule {}
