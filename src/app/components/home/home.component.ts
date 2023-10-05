import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  constructor(private modalService: NgbModal){}

  ngOnInit(): void {
    
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
