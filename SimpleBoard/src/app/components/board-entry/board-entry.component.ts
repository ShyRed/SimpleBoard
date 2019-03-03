import { Input, Component, OnInit } from '@angular/core';
import BoardEntry from '../../models/board-entry';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-board-entry',
  templateUrl: './board-entry.component.html',
  styles: []
})
export class BoardEntryComponent implements OnInit {

  @Input() boardEntry: BoardEntry;

  username: string;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getUser(this.boardEntry.createdBy).subscribe(x => {
      this.username = x.lastName + ", " + x.firstName + " <" + x.username + ">";
    });
  }
}
