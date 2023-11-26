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
  currentUsername: string;

  constructor(private modalService: NgbModal, private dataService: ForumDataService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.currentUsername = localStorage.getItem('username')!;
    this.dataService.getSingleBoardWithPosts(this.route.snapshot.params.id).subscribe((board) =>{
      this.boardTitle = board.title;
      this.boardText = board.description;
      this.boardImg = board.imgUrl;
      this.posts = board.posts;
      this.collectionSize = board.posts.length;
    })
  }

  open(content: any): void {
    this.modalService.open(content, { centered: true} )
  }

  addNewPost(post: Post): void {
    this.dataService.addNewPost(post).subscribe((p) => {
      //User object is added to the response - user object not sent to server/database
      p.user = {
        id: p.userId,
        username: this.currentUsername
      };
      this.posts.push(p);
    });
  }

  updatePostViews(post: Post, property: string): void {
    this.dataService.updatePostViewsOrReplies(post, property).subscribe().unsubscribe();
  }
}
