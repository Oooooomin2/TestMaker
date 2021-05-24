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
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'test/setsettings', component: SettingsTestComponent },
      { path: 'test/create', component: CreateTestComponent },
      { path: 'test/detail/:id', component: DetailTestComponent },
      { path: 'test/edit/:id', component: EditTestComponent },
      { path: 'test/delete-confirm/:id', component: DeleteTestComponent },
    ])
  ],
  providers: [
    TestService,
    HomeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
