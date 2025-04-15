# Fase de build
FROM node:18 AS build
WORKDIR /app

# Copiar los archivos de la aplicación Angular
COPY ./productos-compras-full-stack ./
# Instalar dependencias
RUN npm install -g @angular/cli
RUN npm install

# Crear el build de producción
RUN ng build

# Fase de Nginx para servir la app
FROM nginx:alpine

# Copiar los archivos generados a Nginx
COPY --from=build /app/dist/productos-compras-full-stack/browser /usr/share/nginx/html

COPY ./nginx.conf/nginx.conf /etc/nginx/conf.d/default.conf

# Exponer el puerto 80
EXPOSE 80
