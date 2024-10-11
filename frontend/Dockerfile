FROM node:20 AS build
WORKDIR /app

COPY . /app

RUN npm install --legacy-peer-deps

RUN npx nx build host --prod
RUN npx nx build institutions --prod

FROM nginx:alpine

RUN mkdir -p /usr/share/nginx/html/host /usr/share/nginx/html/institutions

COPY --from=build /app/dist/apps/host /usr/share/nginx/html/host
COPY --from=build /app/dist/apps/institutions /usr/share/nginx/html/institutions

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 4200 4201

CMD ["nginx", "-g", "daemon off;"]

