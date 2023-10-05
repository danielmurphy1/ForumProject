import { Component, EventEmitter, OnInit, Output } from '@angular/core';

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
  constructor() {}

  ngOnInit(): void {
    
  }
  saveButtonClickHandler(): void{
    console.log("username", this.username, "email", this.email, "password", this.password);
    this.onSignupSave.emit();
  }
}
