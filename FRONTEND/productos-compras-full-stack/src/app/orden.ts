import { ProductoOrden } from "./productoOrden"

export interface Orden{
    id: number,
    fecha: Date,
    total: number,
    productoOrdenes: Array<ProductoOrden>
}