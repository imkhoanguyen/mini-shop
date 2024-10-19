import { Component } from '@angular/core';
import { ReviewComponent } from "../review/review.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ReviewComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
