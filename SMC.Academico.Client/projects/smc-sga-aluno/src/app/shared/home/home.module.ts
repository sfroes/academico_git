import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "../components/header/header.component";
import { FooterComponent } from "../components/footer/footer.component";
import { MenuComponent } from "../components/menu/menu.component";
import { HomeComponent } from "./home.component";

@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [CommonModule],
  exports: [HomeComponent],
})
export class HomeModule {}
