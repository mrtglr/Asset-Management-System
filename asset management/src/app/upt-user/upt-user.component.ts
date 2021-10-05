import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../_shared/user.model';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-upt-user',
  templateUrl: './upt-user.component.html',
  styles: []
})
export class UptUserComponent implements OnInit {

  constructor(public service: UserService, private router: Router) { }

  passwordInfo: string = "User password must be at least 8 characters \nand contain at least one letter, number and special character.  \nExample: abcd123*"
  show_button: Boolean = false;
  show_eye: Boolean = false;

  showPassword() {
    this.show_button = !this.show_button;
    this.show_eye = !this.show_eye;
  }

  ngOnInit() {
    this.service.createList().subscribe(
      res=>{
        this.service.list_user = res;
      }
    );
  }

  onSubmit() {
    this.updateKullanici();
  }

  updateKullanici(){

    var newUser: User;
    this.service.formDataUser.user_id = this.service.formDataUser.user_id;
    this.service.formDataUser.user_name = (<HTMLInputElement>document.getElementById("user_name")).value;
    this.service.formDataUser.user_surname = (<HTMLInputElement>document.getElementById("user_surname")).value;
    this.service.formDataUser.user_email = (<HTMLInputElement>document.getElementById("user_email")).value;
    this.service.formDataUser.user_role = this.rolConverter((<HTMLSelectElement>document.getElementById("user_role")).value);
    this.service.formDataUser.isActive = true;
    this.service.formDataUser.user_adress = (<HTMLTextAreaElement>document.getElementById("user_adress")).value;
    this.service.formDataUser.user_password = (<HTMLInputElement>document.getElementById("user_password")).value;
    newUser = this.service.formDataUser
    this.updateRecord(newUser);
  }

  rolConverter(condition: string) {
    if(condition.toLowerCase().match("true"))
      return true;
    else if(condition.toLowerCase().match("false"))  
      return false;
  }

  updateRecord(user: User) {
    if (confirm('Do you want to update this user ?')) {
      this.service.putUser(user.user_id, user).subscribe
      (res => {
        this.service.createList().subscribe(
          res=>{
            this.service.list_user = res;
          }
        );
        this.service.formDataUser= {
          user_id: 0,
          user_name: '',
          user_surname: '',
          user_email: '',
          user_role: null,
          isActive: null,
          user_adress: '',
          user_password: '',
          totalAssetWorth: 0
        };
        this.router.navigate(['/kullaniciList']);
      },
      err => {
        alert("The updated user cannot be the same with old one !")
        console.log(err);
      })
    }
  }

  backtoList() {
    if(confirm('Do you want to exit without update user ?')){
      this.service.formDataUser= {
        user_id: 0,
        user_name: '',
        user_surname: '',
        user_email: '',
        user_role: null,
        isActive: null,
        user_adress: '',
        user_password: '',
        totalAssetWorth: 0
      };
      this.router.navigate(['/kullaniciList']);
    }
  }

}
