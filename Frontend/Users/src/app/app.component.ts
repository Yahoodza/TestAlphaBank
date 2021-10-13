import { UsersTableComponent } from './pages/usersTable/usersTable.component';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isCollapsed = false;

  @ViewChild(UsersTableComponent) child!: UsersTableComponent;

  constructor() { }
}
