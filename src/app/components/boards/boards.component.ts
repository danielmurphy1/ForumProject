import { Component, OnInit } from '@angular/core';
import { Board } from '../../models/Board';
import { Router } from '@angular/router';
import { DatabaseService } from '../../services/database.service';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html',
  styleUrls: ['./boards.component.css']
})
export class BoardsComponent implements OnInit{
  boards: Board[];

  constructor(private router: Router, private dbService: DatabaseService) {}

  ngOnInit(): void {
    this.dbService.getBoards().subscribe((boards) => {
      this.boards = boards;
    }) 
  }
}
