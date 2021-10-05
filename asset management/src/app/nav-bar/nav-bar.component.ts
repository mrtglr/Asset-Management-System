import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../_shared/user.model';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public service: UserService, private router: Router) { }

  currentUserRole: string;
  currentUser: User;

  ngOnInit() {
    this.currentUserRole = this.allStorage().toString();
    this.getCurrentUSer();
  }

  getCurrentUSer(){
    this.service.getUser(+Object.keys(localStorage)).subscribe(
      res=>{
        this.currentUser = res;
      }
    )
  }

  allStorage() {
    var values = [],
    keys = Object.keys(localStorage),
    i = keys.length;

    while ( i-- ) {
        values.push( localStorage.getItem(keys[i]) );
    }
    return values;
  }

  //**Helpers**//

  getPath() {
    return window.location.pathname; 
  }

  userLogOut(id: number) {
    this.service.logout(id).subscribe(
      res => {
        this.router.navigate(['/']);
      },
      err => {
        debugger;
        console.log("err");
      }
    )
  }

  logout() {
    var key = Object.keys(localStorage);   
    if(confirm("Do you want to Log out ?")){
      this.userLogOut(+key);
    }
  }

}
