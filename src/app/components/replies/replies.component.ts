import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../../services/database.service';
import { Post } from '../../models/Post';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-replies',
  templateUrl: './replies.component.html',
  styleUrls: ['./replies.component.css']
})
export class RepliesComponent implements OnInit {
  topic: Post;

  constructor(private dbService: DatabaseService, private route: ActivatedRoute){}

  ngOnInit(): void {
  }
}
