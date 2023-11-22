import { Component, OnInit } from '@angular/core';
import { Board } from '../../models/Board';
import { Router } from '@angular/router';
import { ForumDataService } from '../../services/forum-data.service';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html',
  styleUrls: ['./boards.component.css']
})
export class BoardsComponent implements OnInit{
  boards: Board[];

  constructor(private router: Router, private dataService: ForumDataService) {}

  ngOnInit(): void {
    this.dataService.getBoards().subscribe((boards) => {
      this.boards = boards;
    }) 
  }
}
