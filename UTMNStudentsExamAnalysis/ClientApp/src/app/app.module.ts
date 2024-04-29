import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TownTypeListComponent } from './town-type/town-type-list/town-type-list.component';
import { ReportGeneratorComponent } from './report-generator/report-generator.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TownTypeListComponent,
    ReportGeneratorComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },      
      { path: 'report', component: ReportGeneratorComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
