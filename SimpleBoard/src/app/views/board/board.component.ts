import { Component, OnInit } from '@angular/core';
import { BoardEntryService } from '../../services/board-entry.service';
import BoardEntry from 'src/app/models/board-entry';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styles: []
})
export class BoardComponent implements OnInit {

  boardEntries: BoardEntry[] = [];
  boardEntriesCount: number = 0;
  page: number = 1;
  pageSize: number = 5;
  

  constructor(private boardEntryService: BoardEntryService) { }

  ngOnInit() {
    this.fetchBoardEntries();
  }

  fetchBoardEntries(): void {
    this.boardEntryService.getBoardEntryCount().subscribe(x => this.boardEntriesCount = x);
    this.boardEntryService.getBoardEntries(this.page - 1, this.pageSize).subscribe(x => this.boardEntries = x);
  }

  onPageChange()
  {
    this.fetchBoardEntries();
  }

  onSubmitMessage(message: string): void {
    this.boardEntryService.addBoardEntry({
      id: 0,
      content: message,
      createdAt: new Date().toJSON(),
      createdBy: 0
    }).subscribe(x => this.fetchBoardEntries());
  }
}
