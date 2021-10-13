import { IUserInterface } from 'src/app/interfaces/iuserInterface';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-userAdd',
  templateUrl: './userAdd.component.html',
  styleUrls: ['./userAdd.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserAddComponent {
  @Input() isVisibleAdd = false;
  @Output() onClickShowAdd = new EventEmitter()
  @Output() onClickAdd = new EventEmitter()

  user: IUserInterface = {id: "", fio: null, userLogin: null, dateAddUser: null};

  constructor() { }

  handleOk(): void {
    this.isVisibleAdd = false;
    this.onClickShowAdd.emit(this.isVisibleAdd);
    this.user.dateAddUser = new Date();
    this.onClickAdd.emit(this.user);

    this.user = {id: "", fio: null, userLogin: null, dateAddUser: null};
  }

  handleCancel(): void {
    this.isVisibleAdd = false;
    this.onClickShowAdd.emit(this.isVisibleAdd);

    this.user = {id: "", fio: null, userLogin: null, dateAddUser: null};
  }
}
