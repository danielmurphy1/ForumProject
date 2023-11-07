import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ForumDataService } from '../../services/forum-data.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent implements OnInit{
  username: string;
  email: string;
  password: string;
  @Output() onSignupSave: EventEmitter<Event> = new EventEmitter();
  errorMessage: string;
  constructor(private dataservice: ForumDataService) {}

  ngOnInit(): void {
    
  }
  saveButtonClickHandler(): void{
    const newUser = {
      username: this.username,
      password: this.password,
      email: this.email,
      createdAt: new Date()
    };
    
    this.dataservice.addNewUser(newUser).subscribe(() => {
      this.onSignupSave.emit();
    }, error => {
      this.errorMessage = error.error.message;
    });
  }
}
