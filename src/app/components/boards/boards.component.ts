import { Component, OnInit } from '@angular/core';
import { Board } from '../../Board';

@Component({
  selector: 'app-boards',
  templateUrl: './boards.component.html',
  styleUrls: ['./boards.component.css']
})
export class BoardsComponent implements OnInit{
  boards: Board[] = [
    {
      title: "College Football", 
      description: "A Forum For Discussing All Things College Football.",
      topics: 1, 
      lastTitle: "Poor Pitt",
      lastUser: "Dan",
      lastDate: "Oct 5th"
    }, 
    {
      title: "Video Games",
      description: "A Forum For Discussing All Video Games.", 
      topics: 1,
      lastTitle: "Mortal Kombat 1 Is So Good",
      lastUser: "Dan",
      lastDate: "Oct 5th"
    }
  ]

  constructor() {}

  ngOnInit(): void {
    
  }
}
