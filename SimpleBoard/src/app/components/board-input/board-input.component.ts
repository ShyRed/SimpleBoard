import { Output, EventEmitter, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-board-input',
  templateUrl: './board-input.component.html',
  styles: []
})
export class BoardInputComponent implements OnInit {

  boardInputForm: FormGroup;
  @Output() submitMessage: EventEmitter<string> = new EventEmitter<string>();

  get message() { return this.boardInputForm.get('message'); }

  constructor() { }

  ngOnInit() {
    this.boardInputForm = new FormGroup({
      "message": new FormControl("", [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(1024)
      ])
    });
  }

  onSubmit() {
    let message = this.message.value as string;
    this.submitMessage.emit(message);
    this.message.setValue("");
  }

}
