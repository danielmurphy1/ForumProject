import { Component, OnInit } from '@angular/core';
import { ForumDataService } from '../../services/forum-data.service';
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
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  replyBody: string;
  currentPostId: number;
  currentUserId: number;
  currentUsername: string; 

  constructor(private dataService: ForumDataService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.currentUserId = +localStorage.getItem('userId')!;
    this.currentUsername = localStorage.getItem('username')!;
    this.dataService.getSinglePostWithReplies(this.route.snapshot.params.topic).subscribe((post) =>{
      this.topic = post;
      this.collectionSize = post.replyMessages?.length || 0;
    })

  }

  scrollToBottom(): void {
    window.scrollTo(0, document.body.scrollHeight);
  }

  saveNewReply(): void {
    const newReply: Reply ={
      postId: this.topic.id!,
      body: this.replyBody,
      userId: this.currentUserId,
      createdAt: new Date()
    }
    this.dataService.addNewReply(newReply).subscribe((r) => {
      r.user = {
        username: this.currentUsername,
        id:  r.userId
      }

      this.topic.replyMessages?.push(r);
    });
  }

  updatePostReplies(): void {
    this.dataService.updatePostViewsOrReplies(this.topic, "Replies").subscribe();
  }

  saveReplyButtonClickHandler(): void {
    this.saveNewReply();
    this.updatePostReplies();
  }
}
