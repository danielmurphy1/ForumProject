import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ForumDataService } from '../../services/forum-data.service';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit{
  posts: Post[] = [];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  boardTitle: string;
  boardText: string;

  constructor(private modalService: NgbModal, private dataService: ForumDataService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.dataService.getBoards().subscribe((boards) => {
        for(const board of boards){
          if(board.title === this.route.snapshot.params.id){
            this.posts = board.posts
            console.log(this.posts)
            this.boardTitle = board.title;
            this.boardText = board.description;
          }
        }
      this.collectionSize = this.posts.length;
    });
    this.route.params.subscribe((params) => {
      console.log(params)
    });
    console.log(this.route.snapshot)
    console.log("this.route.params", this.route.params)
    console.log("this.route.snapshote.params", this.route.snapshot.params)
  }

  open(content: any){
    this.modalService.open(content, { centered: true} )
  }
}
