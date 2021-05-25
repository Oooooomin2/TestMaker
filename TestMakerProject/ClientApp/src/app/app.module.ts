import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SettingsTestComponent } from './test/settings-test/settings-test.component';
import { CreateTestComponent } from './test/create-test/create-test.component';
import { TestService } from './test/test.service';
import { HomeService } from './home/home.service';
import { DetailTestComponent } from './test/detail-test/detail-test.component';
import { EditTestComponent } from './test/edit-test/edit-test.component';
import { DeleteTestComponent } from './test/delete-test/delete-test.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { LoginService } from './login/login.service';
import { CreateUserComponent } from './user/create-user/create-user.component';
import { UserService } from './user/user.service';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { DeleteUserComponent } from './user/delete-user/delete-user.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SettingsTestComponent,
    CreateTestComponent,
    DetailTestComponent,
    EditTestComponent,
    DeleteTestComponent,
    LoginComponent,
    CreateUserComponent,
    EditUserComponent,
    DeleteUserComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'test/setsettings', component: SettingsTestComponent, canActivate: [AuthGuard] },
      { path: 'test/create', component: CreateTestComponent, canActivate: [AuthGuard] },
      { path: 'test/detail/:id', component: DetailTestComponent, canActivate: [AuthGuard] },
      { path: 'test/edit/:id', component: EditTestComponent, canActivate: [AuthGuard] },
      { path: 'test/delete-confirm/:id', component: DeleteTestComponent, canActivate: [AuthGuard] },
      { path: 'account/login', component: LoginComponent },
      { path: 'user/create', component: CreateUserComponent },
      { path: 'user/edit/:id', component: EditUserComponent, canActivate: [AuthGuard] },
      { path: 'user/delete-confirm/:id', component: DeleteUserComponent, canActivate: [AuthGuard] }
    ]),
  ],
  providers: [
    TestService,
    HomeService,
    LoginService,
    UserService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
