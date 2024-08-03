import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { BooksComponent } from './components/books/books.component';
import { AboutComponent } from './components/about/about.component'; // Import about component
import { LoginComponent } from './components/login/login.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { SignupComponent } from './components/signup/signup.component';
import { IndividualBookComponent } from './components/individual-book/individual-book.component';
import { CartComponent } from './components/cart/cart.component';
import { PaymentComponent } from './components/payment/payment.component';
import { FileformComponent } from './components/fileform/fileform.component';
const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'home/books', component: BooksComponent }, // Route for books component
  { path: 'home/about', component: AboutComponent },
  { path: 'home/login', component: LoginComponent },
  {path:'home/inventory',component:InventoryComponent},
  {path:'home/signup',component:SignupComponent},
  { path: 'home/individualBook/:id', component: IndividualBookComponent },
  {path:'home/cart',component:CartComponent},
  {path:'home/payment',component:PaymentComponent},
  {path:'home/fileform/:id',component:FileformComponent},
  // Route for about component
  { path: '', redirectTo: '/home', pathMatch: 'full' } // Redirect to home component by default
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
