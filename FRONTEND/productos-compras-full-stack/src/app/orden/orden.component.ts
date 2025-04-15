import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Orden } from '../orden';
import { OrdenService } from '../orden.service';
import { Producto } from '../producto';
import { ProductoService } from '../producto.service';
import { ProductoOrden } from '../productoOrden';
import { switchMap, forkJoin, tap, catchError, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-orden',
  imports: [CommonModule, FormsModule, BrowserModule],
  templateUrl: './orden.component.html',
  styleUrl: './orden.component.css',
})
export class OrdenComponent implements OnInit{
  ordenes: Orden[] = []
  productosSeleccionados: Producto[] = [];
  productos: Producto[] = []

  constructor(private servicioOrdenes: OrdenService, private servicioProductos: ProductoService, private location: Location, private route: ActivatedRoute){

  }

  ngOnInit(): void {
    this.getOrdenes()
    this.getProductos()
  }

  getOrdenes(): void {
    this.servicioOrdenes.getOrdenes().subscribe({
      next: (ordenes) => {
        console.log(ordenes)
        this.ordenes = ordenes;
      },
      error: (err) => {
        console.error('Error al obtener órdenes', err);
      }
    })
  }

  getProductos() {
    this.servicioProductos.getProductos().subscribe(productos => this.productos = productos)
  }

  add(): void{
    if (!this.productosSeleccionados || this.productosSeleccionados.length === 0){
      console.log("No hay productos seleccionados")
      return
    } 

    const nuevaOrden: Orden = {
      id: 0,
      fecha: new Date(),
      total: this.productosSeleccionados.reduce((total, producto) => total + producto.precio, 0),
      productoOrdenes: this.productosSeleccionados.map(p => ({
        id: 0,
        productoId: p.id,
        ordenId: 0
      }))
    };

    this.servicioOrdenes.addOrden(nuevaOrden).pipe(
      switchMap((ordenCreada) => {
        console.log("Orden creada", ordenCreada)
        const productosOrden: ProductoOrden[] = this.productosSeleccionados.map(producto => ({
          id: 0,
          productoId: producto.id,
          ordenId: ordenCreada.id
        }));
        const peticiones = productosOrden.map(po => 
          this.servicioOrdenes.addProductoOrden(po)
        )

        return forkJoin(peticiones).pipe(
          tap((respuestas) => {
            console.log('ProductoOrden creados:', respuestas);
            this.ordenes.push(ordenCreada);
            this.productosSeleccionados = [];
          }),
          catchError(error => {
            console.error("Error durante la creación de la orden o productos:", error);
            return of()
          })
        )
      })
    ).subscribe()
  }
  

  onProductoSeleccionado(event: any): void {
    const idsSeleccionados = Array.from(event.target.selectedOptions).map((option: any) => option.value);
    this.productosSeleccionados = this.productos.filter(producto => idsSeleccionados.includes(producto.id.toString()));
    console.log(this.productosSeleccionados)
  }

  obtenerNombreProducto(productoId: number): string {
    const producto = this.productos.find(p => p.id === productoId);
    return producto ? producto.nombre : 'Producto no encontrado';
  }
  
  goBack(){
    this.location.back()
  }

}
