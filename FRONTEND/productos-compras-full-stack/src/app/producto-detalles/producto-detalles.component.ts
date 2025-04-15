import { Component, input, Input, OnInit } from '@angular/core';
import { Producto } from '../producto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-producto-detalles',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './producto-detalles.component.html',
  styleUrl: './producto-detalles.component.css'
})
export class ProductoDetallesComponent {
  @Input() producto!: Producto

  constructor(
    private route: ActivatedRoute,
    private productoService: ProductoService,
    private location: Location
  ){}

  ngOnInit(): void{
    this.getProducto();
  }

  getProducto(): void{
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.productoService.getProducto(id).subscribe({
      next: (producto) =>{
        if(producto){
          this.producto = producto
        }
        else{
          console.error(`Producto con id ${id} no encontrado`)
        }
      },
      error: (err) => {
        console.error("Error al recuperar producto", err)
      }
    })
  }

  save(): void{
    this.productoService.updateProducto(this.producto).subscribe(() => this.goBack())
  }

  goBack(): void{
    this.location.back()
  }
}
