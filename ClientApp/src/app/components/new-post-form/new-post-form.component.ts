import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-new-post-form',
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
export class NewPostFormComponent implements OnInit{
  title: string;
  postBody: string;
  @Output() onNewPostSave: EventEmitter<Post> = new EventEmitter();
  currentBoardId: number;
  currentUserId: number;

  constructor(private route: ActivatedRoute){}

  ngOnInit(): void {
    this.currentBoardId = this.route.snapshot.params.id;
    this.currentUserId = +localStorage.getItem('userId')!;
  }

  postSaveButtonClickHandler(){
    const newPost: Post = {
      title: this.title, 
      body: this.postBody,
      boardId: this.currentBoardId,
      createdAt: new Date(),
      userId: this.currentUserId,
      replies: 0, 
      views: 0
    }
    this.onNewPostSave.emit(newPost);
  }
}
