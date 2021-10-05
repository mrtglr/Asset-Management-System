import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserPostService } from '../_shared/user-post.service';
import { UserPost } from '../_shared/user-post.model';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styles: []
})
export class AddUserComponent implements OnInit {

  constructor(public servicePost: UserPostService, public service: UserService, private router: Router) { }

  passwordInfo: string = "User password must be at least 8 characters \nand contain at least one letter, number and special character.  \nExample: abcd123*"
  show_button: Boolean = false;
  show_eye: Boolean = false;
 
  ngOnInit() {
  }

  onSubmit() {   
    this.insertUser();            
  }

  insertUser(){   
    var newUser: UserPost;
    this.servicePost.formDataUser.user_name = (<HTMLInputElement>document.getElementById("user_name")).value;
    this.servicePost.formDataUser.user_surname = (<HTMLInputElement>document.getElementById("user_surname")).value;
    this.servicePost.formDataUser.user_email = (<HTMLInputElement>document.getElementById("user_email")).value;
    this.servicePost.formDataUser.user_role = this.rolConverter((<HTMLSelectElement>document.getElementById("user_role")).value);
    this.servicePost.formDataUser.user_adress = (<HTMLInputElement>document.getElementById("user_adress")).value;
    this.servicePost.formDataUser.user_password = (<HTMLInputElement>document.getElementById("user_password")).value;  

    newUser = this.servicePost.formDataUser
    this.insertRecord(newUser);
  }

  insertRecord(newUser: UserPost) {
    if (confirm('Do you want add this user ?')) {
      this.servicePost.postUser(newUser).subscribe(
        res => {
          (<HTMLFormElement>document.getElementById("insrt_user")).reset();
          this.service.createList().subscribe(
            res=>{
              this.service.list_user = res;
            }
          );
          this.router.navigate(['/kullaniciList']);
        },
        err => {
          alert("User alreay exist!")
          console.log(err);
        }
      )
    }   
  }

  //**Helpers**//

  showPassword() {
    this.show_button = !this.show_button;
    this.show_eye = !this.show_eye;
  }

  backtoList() {
    if(confirm('Do you want to exit without adding new user ?')){
      this.router.navigate(['/kullaniciList']);
    }
  }

  rolConverter(condition: string) {
    if(condition.toLowerCase().match("true"))
      return true;
    else if(condition.toLowerCase().match("false"))  
      return false;
  }
}
