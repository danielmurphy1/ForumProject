import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  title: string = "Eclectic Forums";
  subtitle: string = "A Place To Discuss The Varied Interests of the Webmaster";
}
