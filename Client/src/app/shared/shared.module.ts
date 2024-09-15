import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';  
import { SplitterModule } from 'primeng/splitter';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ReactiveFormsModule } from '@angular/forms';
@NgModule({
  exports: [
    CardModule,
    ButtonModule,
    DividerModule,
    SplitterModule,
    CommonModule,
    InputTextModule,
    FormsModule,
    FloatLabelModule,
    KeyFilterModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
