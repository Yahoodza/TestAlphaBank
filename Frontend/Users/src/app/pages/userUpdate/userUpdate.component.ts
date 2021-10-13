import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { IUserInterface } from 'src/app/interfaces/iuserInterface';

@Component({
  selector: 'app-userUpdate',
  templateUrl: './userUpdate.component.html',
  styleUrls: ['./userUpdate.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserUpdateComponent {
  @Input() isVisibleUpdate = false;
  @Input() user!: IUserInterface;
  @Output() onClickShowUpdate = new EventEmitter()
  @Output() onClickUpdate = new EventEmitter()


  constructor() { }

  handleOk(): void {
    this.isVisibleUpdate = false;
    this.onClickShowUpdate.emit(this.isVisibleUpdate);
    this.onClickUpdate.emit(this.user);
  }

  handleCancel(): void {
    this.isVisibleUpdate = false;
    this.onClickShowUpdate.emit(this.isVisibleUpdate);
  }
}
