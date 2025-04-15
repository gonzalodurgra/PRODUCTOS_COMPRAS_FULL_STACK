import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MensajesService {

  mensajes: string[] = []

  add(m: string){
    this.mensajes.push(m)
  }

  clear(){
    this.mensajes = []
  }
}
