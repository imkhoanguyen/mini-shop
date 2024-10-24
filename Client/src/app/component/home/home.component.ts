import { Component } from '@angular/core';
import { ReviewComponent } from "../review/review.component";
import { HeaderComponent } from "../../layout/header/header.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ReviewComponent, HeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
