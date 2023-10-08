import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit{
  constructor(private modalService: NgbModal){}

  ngOnInit(): void {

  }

  open(content: any){
    this.modalService.open(content, { centered: true} )
  }
}
