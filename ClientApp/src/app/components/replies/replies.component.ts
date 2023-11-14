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
  // postReplies: Reply[];
  page: number = 1;
  pageSize: number = 10;
  collectionSize: number;
  replyBody: string;
  currentPostId: number;
  currentUserId: number;
  currentUsername: string; 

  constructor(private dataService: ForumDataService, private route: ActivatedRoute){}

  ngOnInit(): void {
    console.log(this.route.snapshot)
    this.currentUserId = +localStorage.getItem('userId')!;
    this.currentUsername = localStorage.getItem('username')!;
    console.log(this.route.snapshot.params)
    // this.dataService.getBoards().subscribe((boards) => {
    //   for(const board of boards){
    //     if(board.title === this.route.snapshot.params.id){
    //       for(const post of board.posts){
    //         if(post.title === this.route.snapshot.params.topic){
    //           this.topic = post;
    //           if(post.postReplies){
    //             this.postReplies = post.postReplies;
    //             this.collectionSize = this.postReplies.length;
    //           }
    //         }
            
    //       }
    //     }
    //   }
    // })
    this.dataService.getSinglePostWithReplies(this.route.snapshot.params.topic).subscribe((post) =>{
      this.topic = post;
      this.collectionSize = post.replyMessages?.length || 0;
      // this.postReplies = this.topic.postReplies;
      console.log("topic", this.topic)
    })

  }

  scrollToBottom(): void {
    window.scrollTo(0, document.body.scrollHeight);
  }

  saveNewReply(): void {
    console.log("replyBody", this.replyBody)
    const newReply: Reply ={
      postId: this.topic.id!,
      body: this.replyBody,
      //userId will need to be added dynamically later
      userId: this.currentUserId,
      createdAt: new Date()
    }
    // console.log("newReplyRequest: ", newReply)
    this.dataService.addNewReply(newReply).subscribe((r) => {
      r.user = {
        username: this.currentUsername,
        id:  r.userId
      }

      this.topic.replyMessages?.push(r);
      // console.log("pushed reply: ", r)
    });
  }

  updatePostReplies(): void {
    console.log("this.topic", this.topic)
    this.dataService.updatePostViewsOrReplies(this.topic, "Replies").subscribe();
  }

  saveReplyButtonClickHandler(): void {
    this.saveNewReply();
    this.updatePostReplies();
  }
}
