import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ForumDataService } from '../../services/forum-data.service';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../models/User';

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
  boardImg: string;

  constructor(private modalService: NgbModal, private dataService: ForumDataService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.dataService.getSingleBoardWithPosts(this.route.snapshot.params.id).subscribe((board) =>{
      this.boardTitle = board.title;
      this.boardText = board.description;
      this.boardImg = board.imgUrl;
      this.posts = board.posts;
      this.collectionSize = board.posts.length;
    })
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

  addNewPost(post: Post){
    this.dataService.addNewPost(post).subscribe((p) => {
      // this.posts.push(p);
      //User object is added to the response - user object not sent to server/database
      p.user = {
        id: p.userId,
        username: "Ben"
      }
      console.log("response post", p)
      this.posts.push(p);
    });
    console.log("inbound post", post)
  }
}
