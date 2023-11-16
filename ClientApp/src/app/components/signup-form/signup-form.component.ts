import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ForumDataService } from '../../services/forum-data.service';
import { NgForm } from '@angular/forms';

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
  isLoading: boolean = false;

  constructor(private dataservice: ForumDataService) {}

  ngOnInit(): void {
    
  }
  saveButtonClickHandler(form: NgForm): void{
    this.isLoading = true;
    form.value.createdAt = new Date();
    
    this.dataservice.addNewUser(form.value)
    .subscribe(
      { 
        next: () => {
          this.isLoading = false;
          this.onSignupSave.emit();
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage = error.error.message;
        }
      }
    );
  }
}
