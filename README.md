# PRODUCTOS COMPRAS FULL STACK
Bienvenidos a mi proyecto que realiza un CRUD de una serie de productos y permite realizar órdenes de compra. Para ello se ha utilizado Docker, con contenedores para .NET, Angular y PostgreSQL.
## Dockerfile y docker-compose.yaml
Tenemos 2 ficheros DockerFile:
- **FrontEnd**: Empleamos las imágenes de *Node.js* y *Nginx*, copiando nuestra carpeta dentro del frontend a un directorio llamado app dentro de la imagen. Acto seguido, instalamos *Dependencias*, ponemos la app en producción y copiamos el directorio donde se crea la aplicación (browser) en la carpeta que sirve Nginx.
- Además, tenemos un archivo nginx.conf, que permitirá conectarnos con el backend.
- [Dockerfile Frontend](FRONTEND/Dockerfile) 
- **BackEnd**: Utilizo *Dotnet.sdk* con el runtime *Aspnet*. Copiamos la solución al directorio de la imagen app y el proyecto. Restauramos las dependencias, publicamos e iniciamos el runtime.
- [Dockerfile Backend](BACKEND/Dockerfile)
- **Adicional**: Necesitamos la base de datos que respalda el backend, para ello, he usado *postgres* como imagen.
- **Persistencia y entorno**: Se ha creado un volumen para guardar lo que hay en la base de datos, además de todos los bind mounts para poder editar en Angular directamente desde el anfitrión. Se han aportado como variables de entorno:
la base de datos junto al usuario y contraseña, además de tener la conexión del backend y el modo Development para permitir detectar ciertos errores. Mencionar que todos los contenedores conectan en una red.
- [Docker-Compose](docker-compose.yaml)
## Codificación en el Backend
- **Program**: Principalmente habilitamos CORS para poder comunicarnos desde el frontend con el backend. Además, habilitamos NewtonSoftJson. [Program](BACKEND/PRODUCTOS_COMPRAS_FULL_STACK/Program.cs)
- **Models**: Esta carpeta contiene el contexto de la base de datos, los modelos y las configuraciones de entidades para realizar migraciones a la base de datos en PostgreSQL. [Models](BACKEND/PRODUCTOS_COMPRAS_FULL_STACK/Models/)
-  **DAO**: Aquí tenemos las clases de acceso a datos, poniendo así una capa intermedia entre controlador y datos. [DAO](BACKEND/PRODUCTOS_COMPRAS_FULL_STACK/DAO/) Destacar que en [ProductoOrdenDao](BACKEND/PRODUCTOS_COMPRAS_FULL_STACK/DAO/productoOrdenDAO.cs) se ha utilizado una transacción, ya que no interesa si no se ha insertado correctamente la relación entre Orden y Producto que permanezca una orden de compra sin relación.
-  **Controllers**: El directorio del proyecto que contendrá los controladores para comunicación con la base de datos a través de los diferentes métodos HTTP. [Controllers](BACKEND/PRODUCTOS_COMPRAS_FULL_STACK/Controllers/)
-  *Existen carpetas adicionales, pero las principales para realizar la actividad son éstas.*
## Codificación en el Frontend
- **Main**: Aquí arrancamos la aplicación, creando las diferentes rutas para los distintos componentes. [Main](FRONTEND/productos-compras-full-stack/src/main.ts)
- **Interfaces** [Producto](FRONTEND/productos-compras-full-stack/src/app/producto.ts) [Orden](FRONTEND/productos-compras-full-stack/src/app/orden.ts) [ProductoOrden](FRONTEND/productos-compras-full-stack/src/app/productoOrden.ts)
- **Servicio de productos**: Realizamos las diferentes operaciones CRUD de productos que comunicarán con la base de datos. [Servicio de productos](FRONTEND/productos-compras-full-stack/src/app/producto.service.ts)
- **Servicio de órdenes**: De aquí realmente solo se utilizan los métodos para listar y crear órdenes relacionándolas con los productos. [Servicio de órdenes](FRONTEND/productos-compras-full-stack/src/app/orden.service.ts)
- **Componente orden**: Podremos a través de este componente ver y realizar órdenes de compra de productos. [Orden](FRONTEND/productos-compras-full-stack/src/app/orden/)
- **Componente producto**: Será la interfaz de usuario que permite ver todos los productos y  borrarlos, junto a su formulario de inserción [Producto](FRONTEND/productos-compras-full-stack/src/app/producto/)
- **Componente para detallar el producto**: Permite la actualización del producto seleccionado principalmente. [Detalles del producto](FRONTEND/productos-compras-full-stack/src/app/producto-detalles/)
- *Adicionalmente, se puede implementar como futuras mejoras si la aplicación se vuelve más grande un dashboard componente para mensajes*
## Levantando el proyecto
- Sitúate en el [directorio del docker-compose.yaml](/) (la raíz del proyecto), ejecutando el comando **docker-compose up --build**. En VSCode también tienes la opción de abrir el proyecto también desde la raíz y con el plugin de Docker instalado puedes ejecutar todos los servicios directamente desde el código sin necesidad de escribir comandos a mano, pulsando en Run all services.
- Para acceder a la aplicación realizada en Angular, escribe la URL localhost:4200, la cual te llevará a los productos que existen en la base de datos.
- Puedes probar además la API en PostMan con localhost:8080, que es el puerto al que se redirecciona el backend en el anfitrión.
