import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  isAuthenticated: boolean;
  constructor(private modalService: NgbModal, private authService: AuthService){}

  ngOnInit(): void {
    this.authService.isAuthenticated.subscribe(value => {
      this.isAuthenticated = value;
    })
  }

  open(content: any){
    console.log("contnet", content)
    //for later beforeDismiss() callback might be able to be used - see docs
    // this.modalService.open(content, { centered: true, beforeDismiss() {
    //   return false
    // }, });
    this.modalService.open(content, { centered: true });
  }
}
