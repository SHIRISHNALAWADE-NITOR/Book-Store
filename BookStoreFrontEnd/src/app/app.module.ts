import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { BooksComponent } from './components/books/books.component';
import { AboutComponent } from './components/about/about.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { FooterComponent } from './components/footer/footer.component'; 
import { CarouselComponent } from './components/carousel/carousel.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { SignupComponent } from './components/signup/signup.component';
import { IndividualBookComponent } from './components/individual-book/individual-book.component';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { CartComponent } from './components/cart/cart.component';
import { PaymentComponent } from './components/payment/payment.component';
import { FileformComponent } from './components/fileform/fileform.component';
import { BookFormDialogComponent } from './components/book-form-dialog/book-form-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from 'src/app/components/profile/profile.component';
import { MessageService } from 'primeng/api'; 
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { SlidingCardsComponent } from './components/sliding-cards/sliding-cards.component';
import { NewArrivalsComponent } from './components/new-arrivals/new-arrivals.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { PasswordchangeComponent } from './components/passwordchange/passwordchange.component';
import { PasswordChangeDialogComponent } from './components/password-change-dialog/password-change-dialog.component'; 
import { MatFormFieldModule } from '@angular/material/form-field';
import { ToastrModule } from 'ngx-toastr';
import { ToastComponent } from './components/toast/toast.component'; // Correct import path

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    BooksComponent,
    AboutComponent,
    SidebarComponent,
    LoginComponent,
    FooterComponent,
    CarouselComponent,
    InventoryComponent,
    SignupComponent,
    IndividualBookComponent,
    CartComponent,
    PaymentComponent,
    FileformComponent,
    BookFormDialogComponent,
    ProfileComponent,
    SlidingCardsComponent,
    NewArrivalsComponent,
    CategoriesComponent,
    PasswordchangeComponent,
    PasswordChangeDialogComponent,
    ToastComponent, // Ensure this is declared
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    NgbModule,
    DialogModule,
    ButtonModule,
    MatFormFieldModule,
  ],
  providers: [
    MessageService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
