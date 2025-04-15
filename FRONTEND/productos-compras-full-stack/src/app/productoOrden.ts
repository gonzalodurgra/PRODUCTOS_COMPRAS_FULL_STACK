import { Producto } from "./producto";

export interface ProductoOrden {
    id: number;
    productoId: number;
    ordenId: number;
}