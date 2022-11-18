import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

//ReactiveFormModulu manuel eklemeyi unutma !!

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:'', component:RegisterComponent}
    ]),
    ReactiveFormsModule,
    MatInputModule
  ]
})
export class RegisterModule { }
