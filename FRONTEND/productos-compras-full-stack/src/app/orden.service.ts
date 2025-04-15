import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs';

import { Orden } from './orden';

import { MensajesService } from './mensajes.service';
import { ProductoOrden } from './productoOrden';

@Injectable({
  providedIn: 'root'
})
export class OrdenService {

  private ordenesUrl = 'api/ordenes'
  private productosOrdenesUrl = 'api/productosordenes'
  httpOptions ={
    headers: new HttpHeaders({'Content-Type' : 'application/json'})
  }

  constructor(
    private http: HttpClient,
    private mensajes: MensajesService
  ) { }

  getOrdenes(): Observable<Orden[]>{
    return this.http.get<Orden[]>(this.ordenesUrl).pipe(tap(() => this.log('Orden recuperada')), catchError(this.handleError<Orden[]>("getOrdenes", [])))
  }

  getOrden(id: number){
    const url = `${this.ordenesUrl}/${id}`
    return this.http.get<Orden>(url).pipe(tap(() => this.log(`Orden recuperada id=${id}`)), catchError(this.handleError<Orden>(`getOrden id=${id}`)))
  }

  

  updateOrden(orden: Orden): Observable<Orden>{
    return this.http.put<Orden>(this.ordenesUrl, orden, this.httpOptions).pipe(tap(() => this.log(`Orden actualizada id=${orden.id}`)), catchError(this.handleError<Orden>(`updateOrden`)))
  }

  addOrden(orden: Orden){
    return this.http.post<Orden>(this.ordenesUrl, orden, this.httpOptions).pipe(tap((nuevaOrden: Orden) => this.log(`Orden creada id=${nuevaOrden.id}`)), catchError(this.handleError<Orden>(`addOrden`)))
  }

  addProductoOrden(productoOrden: ProductoOrden): Observable<ProductoOrden>{
    return this.http.post<ProductoOrden>(this.productosOrdenesUrl, productoOrden, this.httpOptions).pipe(tap((nuevoProductoOrden) => this.log(`Relación creada id=${nuevoProductoOrden.id}`)), catchError(this.handleError<ProductoOrden>("addProductoOrden")))
  }

  deleteOrden(orden: Orden | number): Observable<Orden>{
    const id = typeof orden === "number" ? orden : orden.id
    const url = `${this.ordenesUrl}/${id}`

    return this.http.delete<Orden>(url, this.httpOptions).pipe(tap(() => this.log(`Orden borrada id=${id}`)), catchError(this.handleError<Orden>(`deleteOrden`)))
  }

  private log(mensaje: string){
    this.mensajes.add(`Servicio de órdenes: ${mensaje}`)
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
