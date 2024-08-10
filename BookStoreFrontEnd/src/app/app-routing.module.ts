import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BooksComponent } from './components/books/books.component';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { SignupComponent } from './components/signup/signup.component';
import { IndividualBookComponent } from './components/individual-book/individual-book.component';
import { CartComponent } from './components/cart/cart.component';
import { PaymentComponent } from './components/payment/payment.component';
import { FileformComponent } from './components/fileform/fileform.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from 'src/app/services/auth.guard'; // Import AuthGuard
import { CategoriesComponent } from './components/categories/categories.component';
import { PasswordchangeComponent } from './components/passwordchange/passwordchange.component';
import { PasswordChangeDialogComponent } from './components/password-change-dialog/password-change-dialog.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'home/books', component: BooksComponent },
  { path: 'home/about', component: AboutComponent },
  { path: 'home/login', component: LoginComponent },
  { path: 'home/inventory', component: InventoryComponent },
  { path: 'home/forgotpassword', component: PasswordchangeComponent },
  { path: 'home/signup', component: SignupComponent },
  { path: 'home/individualBook/:id', component: IndividualBookComponent },
  { path: 'home/cart', component: CartComponent },
  { path: 'home/payment', component: PaymentComponent },
  { path: 'home/fileform/:id', component: FileformComponent },
  { path: 'home/profile', component: ProfileComponent },
  { path: 'home/categories', component: CategoriesComponent },
  {path:'home/password-reset',component:PasswordChangeDialogComponent},
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
