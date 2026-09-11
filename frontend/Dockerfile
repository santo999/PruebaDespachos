# ---------- Etapa 1: build de Angular ----------
FROM node:22-alpine AS build
WORKDIR /app


COPY package.json package-lock.json ./
RUN npm ci

COPY . .


ARG API_BASE_URL=http://localhost:8080/api
RUN sed -i "s|apiBaseUrl: '.*'|apiBaseUrl: '${API_BASE_URL}'|" src/environments/environment.ts


RUN npm run build

# ---------- Etapa 2: servir con nginx ----------
FROM nginx:alpine AS final
COPY nginx.conf /etc/nginx/conf.d/default.conf


COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html

EXPOSE 80
