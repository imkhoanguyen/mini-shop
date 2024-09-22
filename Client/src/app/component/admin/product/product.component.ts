import { Component } from "@angular/core";
import { ProductlistComponent } from "./productlist/productlist.component";
import { ProductaddComponent } from "./productadd/productadd.component";


@Component({
  selector: 'app-product',
  standalone: true,
  imports: [ProductlistComponent, ProductaddComponent],
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],

})
export class ProductComponent {

}
