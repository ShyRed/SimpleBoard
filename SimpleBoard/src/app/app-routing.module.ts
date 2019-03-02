import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuardService } from './services/auth-guard.service';
import { LoginComponent } from './views/login/login.component';
import { BoardComponent } from './views/board/board.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'board', component: BoardComponent, canActivate: [AuthGuardService] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
