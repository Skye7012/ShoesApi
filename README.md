<p>
    <h1 align="center">ShoeApi</h1>
</p>

<p align="center">
    API для проекта Интернет-магазин обуви
</p>

<p align="center">
  <img src="https://img.shields.io/badge/ASP.NET-purple?style=flat-square"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=Entity-Framework&style=flat-square&color=blueviolet"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=PosgreSql&style=flat-square&color=1A5276"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=MediatR&style=flat-square&color=blue"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=JWT&style=flat-square&color=BDB76B"
      height="40">
   <img src="https://img.shields.io/static/v1?label=&message=MinIo&style=flat-square&color=FF0000"
      height="40">
</p>

<div align="center">
  <a href="https://github.com/Skye7012/ShoesApi/issues">
    <img src="https://img.shields.io/github/issues-raw/Skye7012/ShoesApi" alt="Open Issues"/>
  </a>
  <a href="https://github.com/Skye7012/ShoesApi/issues?q=is%3Aissue+is%3Aclosed">
    <img src="https://img.shields.io/github/issues-closed-raw/Skye7012/ShoesApi" alt="Closed Issues"/>
  </a>
</div>

<br>

# Table Of Contents

* [Общее описание](#общее-описание)
* [Бизнес-логика](#бизнес-логика)
* [Локальный запуск](#локальный-запуск)
* [Related repositories](#related-repositories)

<br>

# Общее описание
API для проекта Интернет-магазин обуви на `ASP.NET`  
API задокументирован с помощью `Swagger`  
Проект структурирован по принципам `clean architecture`  
Реализован `CQRS` с помощью [`MediatR`](https://github.com/jbogard/MediatR)  
В качестве ORM используется `Entity Framework`, в качестве БД `PostgreSql`  
Для хранения изображений обуви используется [`MinIo`](https://min.io/)  
Аутентификация реализована через [`JWT`](https://jwt.io/) токены  
Есть модульные и интеграционные тесты  
Тесты написаны с помощью `xUnit` и [`FluentAsserions`](https://github.com/fluentassertions/fluentassertions)  
Интеграционные тесты реализованы с помощью [`testcontainters`](https://github.com/testcontainers/testcontainers-dotnet) (и [`respawn`](https://github.com/jbogard/Respawn)) (поэтому нужен докер для их прогонки)  

<br>

# Бизнес-логика
Пока реализован минимум:
- Просмотр каталога обуви сортировкой, пагинацией, поиском и **фильтрами (брэнды, назначения, сезоны, размеры)**
- Создание заказа
- Личный кабинет (регистрация, аутентификация, изменение данных, удаление профиля, история заказов)

Фильтры для обуви пока автоматически инициализируются в базе через миграции,  
**а создать обувь пока можно только через api**, предварительно загрузив файл изображения обуви через api  
*Планируется создать отдельный сайт для админа*

Схема БД:
![image](https://user-images.githubusercontent.com/86796337/233123377-350b4c6c-8d38-4c2f-99e0-bc1d7e6b0dcf.png)


# Локальный запуск
- `git clone https://github.com/Skye7012/ShoesApi.git`

- `cd ShoesApi`

- `docker-compose build`

- `docker-compose up`

- **Апи**: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

- **Фронт**: [http://localhost:8081](http://localhost:8081)

- **MinIo**: [http://localhost:9003/login](http://localhost:9003/login)

Volumes для БД и MinIO создадутся на уровень выше корневой директории  
Для того, чтобы полноценно пользоваться фронтом, нужно создать обувь через API [(см. бизнес-логика)](#бизнес-логика)


<br>

# Related repositories

- **Front**:
  
  - [Фронт на `Vue`](https://github.com/Skye7012/shoes-front)

- **Other Api's**:
  
  - [**[deprecated]** ~~API на django rest framework~~](https://github.com/Skye7012/shoes-django-api)
  
  - [**[deprecated]** ~~API на php slim framework~~](https://github.com/Skye7012/shoes-api-slim)




