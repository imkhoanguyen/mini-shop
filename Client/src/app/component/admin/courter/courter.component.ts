import { Component,NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-courter',
  standalone: true,
  imports: [
    InputTextModule,
    ButtonModule,
    FormsModule,
    CardModule
  ],
  templateUrl: './courter.component.html',
  styleUrl: './courter.component.css'
})
export class CourterComponent {
  onSearch(){
    throw new Error('');
  }
  searchString: string='';
}
