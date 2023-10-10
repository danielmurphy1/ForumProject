import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../../services/database.service';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';
import { Reply } from '../../models/Reply';

@Component({
  selector: 'app-replies',
  templateUrl: './replies.component.html',
  styleUrls: ['./replies.component.css']
})
export class RepliesComponent implements OnInit {
  topic: Post;
  postReplies: Reply[];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  replyBody: string;

  constructor(private dbService: DatabaseService, private route: ActivatedRoute){}

  ngOnInit(): void {
    console.log(this.route.snapshot.params.topic)
    this.dbService.getBoards().subscribe((boards) => {
      for(const board of boards){
        if(board.title === this.route.snapshot.params.id){
          for(const post of board.posts){
            if(post.title === this.route.snapshot.params.topic){
              this.topic = post;
              if(post.postReplies){
                this.postReplies = post.postReplies;
                this.collectionSize = this.postReplies.length;
              }
            }
            
          }
        }
      }
    })

  }

  scrollToBottom(): void{
    window.scrollTo(0, document.body.scrollHeight);
  }
}
