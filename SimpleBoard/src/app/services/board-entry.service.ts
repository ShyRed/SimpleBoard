import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import BoardEntry from '../models/board-entry';

const url:string = "http://localhost:5000/api";

@Injectable({
  providedIn: 'root'
})
export class BoardEntryService {

  constructor(private http: HttpClient) { }

  getBoardEntryCount(): Observable<number> {
    return this.http.get<number>(`${url}/BoardEntry`);
  }

  getBoardEntries(page: number, pageSize: number): Observable<BoardEntry[]> {
    return this.http.get<BoardEntry[]>(`${url}/BoardEntry/${page}/${pageSize}`);
  }

  addBoardEntry(entry: BoardEntry) : Observable<BoardEntry> {
    return this.http.post<BoardEntry>(`${url}/BoardEntry`, entry);
  }
}
