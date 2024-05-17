import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { Observable } from 'rxjs';
import { UserService } from '../user.service';
import { ToastrService } from 'ngx-toastr';
import { FileUploadService } from '../file-upload.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  selectedIndex: number = 0;
  accept: string = ".xlsx, .xls, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 

  fileControl: FormControl;

  public files: any;

  constructor(private userService: UserService, private fileUploadService: FileUploadService, private toastr: ToastrService) {
    this.fileControl = new FormControl(this.files);
  }

  ngOnInit(): void {
    this.fileControl.valueChanges.subscribe((files: any) => {
      if (!Array.isArray(files)) {
        this.files = [files];
      } else {
        this.files = files;
      }
    })
  }

  public dropped(files: NgxFileDropEntry[]) {
    this.files = files;
  }

  public fileOver(event: any) {
    //console.log(event);
  }

  public fileLeave(event: any) {
    //console.log(event);
  }

  addNewUser(userData: any): void {
    this.userService.addUser(userData).subscribe((user) => {
      console.log(`User added, ${user}`);
    },
      (error) => {
        console.error(error);
      });
  }

  uploadFiles(): void {
    this.toastr.info("Загрузка успешно начата, примерное время ожидания 15-20 секунд");

    for (const file of this.files) {

      // Is it a file?
      if (file.fileEntry.isFile) {
        const fileEntry = file.fileEntry as FileSystemFileEntry;

        fileEntry.file((fileToUpload: File) => {

          // Here you can access the real file
          console.log(file.relativePath, fileToUpload);


          // You could upload it like this:
          const formData = new FormData()
          formData.append('excelData', fileToUpload, fileToUpload.name)

          //// Headers
          //const headers = new HttpHeaders({
          //  'security-token': 'mytoken'
          //})

          //this.http.post('https://mybackend.com/api/upload/sanitize-and-save-logo', formData, { headers: headers, responseType: 'blob' })
          //.subscribe(data => {
          //  // Sanitized logo returned from backend
          //})

          this.fileUploadService.uploadFile(formData).subscribe(
            (response) => {
              console.log(response);
              this.toastr.success(`Файл успешно загружен ${response}`)              
            },
            (error) => {
              console.log(error);
              this.toastr.error(`Ошибка загрузки файла ${error}`)
            }
          );

        });
      } else {
        // It was a directory (empty directories are added, otherwise only files)
        const fileEntry = file.fileEntry as FileSystemDirectoryEntry;
        console.log(file.relativePath, fileEntry);
      }
    }
  }
}
