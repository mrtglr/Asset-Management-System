import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/_shared/user.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  constructor(public service : UserService, private router: Router) { }

  show_button: Boolean = false;
  show_eye: Boolean = false;
  isMatch: Boolean = false;

  ngOnInit() {
    localStorage.clear();
    this.service.createList().subscribe(
      res =>{
        this.service.list_user = res;
      }
    );
  }

  onSubmit() {
    this.service.createList().subscribe(
      res =>{
        this.service.list_user = res;
      }
    );
    var e_mail = (<HTMLInputElement>document.getElementById("email")).value;
    var password = (<HTMLInputElement>document.getElementById("password")).value;
    this.searchUser(e_mail,password);
  }

  searchUser(e_mail: string, password: string) {

    for(let i = 0; i < this.service.list_user.length; i++){

      this.service.tmp_user = Object.assign({}, this.service.list_user[i]);
      var tmpK = this.service.tmp_user;

      if((e_mail==tmpK.user_email && password.match(tmpK.user_password))){;
        localStorage.setItem(tmpK.user_id.toString(),String(tmpK.user_role));
        this.isMatch = true; break;
      }
      else {
        this.isMatch = false;
        (<HTMLFormElement>document.getElementById("login")).reset();
      }      
    }    
    this.login(this.isMatch);
  }

  login(isMatch: Boolean){
    if(isMatch){
      
      this.service.login(this.service.tmp_user.user_id).subscribe(
        res => {
          if(this.service.tmp_user.user_role){
            this.router.navigate(['/tasinmazList']);
          }
          else if(!this.service.tmp_user.user_role){
            localStorage.setItem(this.service.tmp_user.user_id.toString(),String(this.service.tmp_user.user_role));
            this.router.navigate(['/userProfile'])
          }          
        },
        err => {
          debugger;
          console.log("err");
        }
      )
    } else {
      alert("Wrong Password or E-Mail !");
    }
  }

  //**Helpers**//

  showPassword() {
    this.show_button = !this.show_button;
    this.show_eye = !this.show_eye;
  }

}
