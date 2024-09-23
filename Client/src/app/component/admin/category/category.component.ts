import { Component } from '@angular/core';
import { CategoryaddComponent } from "./categoryadd/categoryadd.component";
import { CategorylistComponent } from "./categorylist/categorylist.component";

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CategoryaddComponent, CategorylistComponent],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {}
