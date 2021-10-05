import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddAssetComponent } from './add-asset/add-asset.component';
import { AddUserComponent } from './add-user/add-user.component';
import { AssetListComponent } from './asset-list/asset-list.component';
import { LogOperationListComponent } from './log-operation-list/log-operation-list.component';
import { LoginComponent } from './login/login.component';
import { UptAssetComponent } from './upt-asset/upt-asset.component';
import { UptUserComponent } from './upt-user/upt-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';


const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'tasinmazList', component: AssetListComponent},
  {path: 'tasinmazAdd', component: AddAssetComponent},
  {path: 'kullaniciList', component: UserListComponent},
  {path: 'kullaniciAdd', component: AddUserComponent},
  {path: 'kullaniciUpt', component: UptUserComponent},
  {path: 'tasinmazUpt', component: UptAssetComponent},
  {path: 'logList', component: LogOperationListComponent},
  {path: 'userProfile', component: UserProfileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [AssetListComponent, LoginComponent, AddAssetComponent, 
  UserListComponent, AddUserComponent, UptUserComponent, UptAssetComponent, LogOperationListComponent, UserProfileComponent]
