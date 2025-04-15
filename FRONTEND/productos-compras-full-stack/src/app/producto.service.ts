import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs';

import { Producto } from './producto';

import { MensajesService } from './mensajes.service';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private productosUrl = '/api/productos'
  httpOptions ={
    headers: new HttpHeaders({'Content-Type' : 'application/json'})
  }
  constructor(
    private http: HttpClient,
    private mensajes: MensajesService
  ) { }

  getProductos(): Observable<Producto[]>{
    return this.http.get<Producto[]>(this.productosUrl).pipe(tap(() => this.log('Producto recuperado')), catchError(this.handleError<Producto[]>("getProductos", [])))
  }

  getProducto(id: number){
    const url = `${this.productosUrl}/${id}`
    return this.http.get<Producto>(url).pipe(tap(() => this.log(`Producto recuperado id=${id}`)), catchError(this.handleError<Producto>(`getProducto id=${id}`)))
  }

 

  updateProducto(producto: Producto): Observable<Producto>{
    const id = typeof producto === "number" ? producto : producto.id
    const url = `${this.productosUrl}/${id}`

    return this.http.put<Producto>(url, producto, this.httpOptions).pipe(tap(() => this.log(`Producto actualizado id=${producto.id}`)), catchError(this.handleError<Producto>(`updateProducto`)))
  }

  addProducto(producto: Producto){
    return this.http.post<Producto>(this.productosUrl, producto, this.httpOptions).pipe(tap((nuevoProducto: Producto) => this.log(`Producto actualizado id=${nuevoProducto.id}`)), catchError(this.handleError<Producto>(`addProducto`)))
  }

  deleteProducto(producto: Producto | number): Observable<Producto>{
    const id = typeof producto === "number" ? producto : producto.id
    const url = `${this.productosUrl}/${id}`

    return this.http.delete<Producto>(url, this.httpOptions).pipe(tap(() => this.log(`Producto borrado id=${id}`)), catchError(this.handleError<Producto>(`deleteProducto`)))
  }

  private log(mensaje: string){
    this.mensajes.add(`Servicio de productos: ${mensaje}`)
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
