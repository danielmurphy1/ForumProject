import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DatabaseService } from '../../services/database.service';
import { Post } from '../../models/Post';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit{
  posts: Post[];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;

  constructor(private modalService: NgbModal, private dbservice: DatabaseService, private router: Router){}

  ngOnInit(): void {
    //need way to check board name and only select posts that match board name


    // this.dbservice.getPosts().subscribe((posts) => {
    //   for(let post of posts){
    //     if post.
    //   }
    // })
    
    this.dbservice.getPosts().subscribe((posts) => {
      this.posts = posts;
      this.collectionSize = this.posts.length;
    })
    console.log(this.router.routerState)
  }

  open(content: any){
    this.modalService.open(content, { centered: true} )
  }
}
