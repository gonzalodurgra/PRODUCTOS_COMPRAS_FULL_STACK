import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { ProductoComponent } from './app/producto/producto.component';
import { provideRouter } from '@angular/router';
import { Routes } from '@angular/router';
import { ProductoDetallesComponent } from './app/producto-detalles/producto-detalles.component';
import { OrdenComponent } from './app/orden/orden.component';

const routes: Routes =  [
  { path: 'productos', component: ProductoComponent },
  { path: "productos/:id", component: ProductoDetallesComponent},
  { path: '', redirectTo: 'productos', pathMatch: 'full'},
  { path: 'ordenes', component: OrdenComponent}
];

bootstrapApplication(AppComponent,{
  providers: [
    provideRouter(routes),
    provideHttpClient()
  ]
}).catch((err) => console.error(err));
