import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Producto } from '../producto';
import { ProductoService } from '../producto.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductoComponent implements OnInit{

  productos!: Producto[];

  constructor(private servicioProducto: ProductoService){

  }
  ngOnInit(): void {
    this.getProductos()
  }

  getProductos() {
    this.servicioProducto.getProductos().subscribe(productos => this.productos = productos)
  }

  add(nombre: string, precio: number, stock: number){
      if(!nombre || !precio || !stock){return}
      this.servicioProducto.addProducto({nombre, precio, stock} as Producto).subscribe(producto => this.productos.push(producto))
    }

  delete(producto: Producto){
    this.productos = this.productos.filter(p => p != producto)
    this.servicioProducto.deleteProducto(producto).subscribe()
  }
  
}
