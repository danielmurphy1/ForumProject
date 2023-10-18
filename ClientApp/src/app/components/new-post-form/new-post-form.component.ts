import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-new-post-form',
  templateUrl: './new-post-form.component.html',
  styleUrls: ['./new-post-form.component.css']
})
export class NewPostFormComponent implements OnInit{
  title: string;
  postBody: string;
  @Output() onNewPostSave: EventEmitter<Event> = new EventEmitter();

  constructor(){}

  ngOnInit(): void {
    
  }

  postSaveButtonClickHandler(){
    this.onNewPostSave.emit();
  }
}
