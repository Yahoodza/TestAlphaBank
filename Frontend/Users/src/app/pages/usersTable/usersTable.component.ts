import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { IUserInterface } from 'src/app/interfaces/iuserInterface';
import { Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { saveAs } from 'file-saver';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-usersTable',
  templateUrl: './usersTable.component.html',
  styleUrls: ['./usersTable.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class UsersTableComponent implements OnInit {
  userList$!: Observable<IUserInterface[]>;
  datepipe: DatePipe = new DatePipe('ru_RU')
  visibleAdd!: boolean;
  visibleUpdate!: boolean;
  userUpdateValue!: IUserInterface;

  constructor(private httpService: HttpService,
              private modal: NzModalService) { }

  getUsers(): void {
    this.userList$ = this.httpService.get('User/UserGet');
    //console.log(this.userList$);
  }

  userAdd(userAdd: IUserInterface): void {
    this.httpService.post('User/UserPost', userAdd)
      .subscribe({
        next: (result) => {
          this.info(result.value);
        },
        error: (error) => console.error(error),
        complete: () => { this.getUsers(); }
      });
  }

  userUpdate(userUpdate: IUserInterface): void {
    this.httpService.put('User/UserPut', userUpdate)
      .subscribe({
        next: (result) => {
          this.info(result.value);
        },
        error: (error) => console.error(error),
        complete: () => { this.getUsers(); }
      });
  }

  deleteUser(id: string): void {
    this.httpService.delete('User/UserDelete', id)
      .subscribe({
        next: (result) => {
          this.info(result.value);
        },
        error: (error) => console.error(error),
        complete: () => { this.getUsers(); }
      });
  }

  deleteAllUser(): void {
    this.httpService.deleteAll('User/UserDeleteAll')
      .subscribe({
        next: (result) => {
          this.info(result.value);
        },
        error: (error) => console.error(error),
        complete: () => { this.getUsers() }
      });
  }

  createExcel(): void {
    this.userList$.subscribe((value) => {
      this.httpService.postExcel('CreateExcel/CreateExcel', value)
      .subscribe({
        next: (response) => {
          console.log('response => ', response);
          saveAs(this.getBlob(response.fileContents, response.contentType), response.fileDownloadName);
        },
        error: (error) => console.error(error),
      });
    });
  }

  private getBlob(response: string, type: string) {
    const byteCharacters = atob(response);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type });
  }

  userValueUpdate(userUp: IUserInterface): void {
    this.userUpdateValue = JSON.parse(JSON.stringify(userUp));;
  }

  showModalAddUser(): void {
    this.visibleAdd = true;
  }

  isVisibleAddModal(vis: boolean): void {
    this.visibleAdd = vis;
  }

  showModalUpdateUser(): void {
    this.visibleUpdate = true;
  }

  isVisibleUpdateModal(vis: boolean): void {
    this.visibleUpdate = vis;
  }

  info(res: string): void {
    setTimeout( () => {this.modal.info({
      nzTitle: 'Информационное сообщение',
      nzContent: res
    });}, 400);
  }

  ngOnInit() {
    this.getUsers()
  }
}
