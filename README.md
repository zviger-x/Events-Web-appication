# Events Management Application

## Инструкции по запуску

1. **Соберите всё решение**
   - Откройте проект `EventsManagement.sln` через Visual Studio.
   - Соберите все проекты в решении.

2. **Опубликуйте базу данных**
   - В проекте найдите решение базы данных `EventsManagement.Database`.
   - Опубликуйте базу данных с именем `EventsManagement.Database`.
   - Укажите "Доверять сертификату сервера" при настройке соединения.

3. **Настройка строки подключения**
   - Замените строки подключения на ту, что использовалась при публикации базы данных, в следующих файлах:
     - `appsettings.json` (проект: `EventsManagement.WebAPI.Server`)
     - `App.config` (проект: `EventsManagement.DataAccess`)
   - Строка подключения обязательно должна содержать: `Initial Catalog=EventsManagement.Database`.

4. **Установка Node.js**
   - Установите [Node.js](https://nodejs.org/) и все необходимые зависимости для проекта React.

5. **Разрешите браузеру небезопасное подключение к localhost**
   - В браузере Google Chrome:
     - Перейдите на страницу `chrome://flags/#allow-insecure-localhost`.
     - Включите параметр "Allow invalid certificates for resources loaded from localhost".
   - Это разрешит работу с самоподписанными сертификатами на `localhost`.

6. **Настройка запуска проекта**
   - Назначьте в Visual Studio в качестве запускаемого проекта `EventsManagement.WebAPI.Server` и выберите нужный профиль:
     - `EventsManagement.WebAPI.Server` — запускает Swagger.
     - `EventsManagement.WebAPI.React` — запускает сайт.
   - Вы также можете вручную перейти на соответствующие страницы:
     - Swagger: [https://localhost:7162/swagger/index.html](https://localhost:7162/swagger/index.html)
     - Сайт: [https://localhost:7162](https://localhost:7162) (сайту требуется немного времени для инициализации).

## Данные в базе данных

После публикации базы данных автоматически генерируются **`тестовые`** пользователи и события.

### Список пользователей и их роли:
| Имя       | Email                     | Роль  |
|-----------|---------------------------|-------|
| Admin     | useradmin@email.com        | Admin |
| User 1    | userdefault@email.com      | User  |
| User 2    | userdefault2@email.com     | User  |
| User 3    | userdefault3@email.com     | User  |
| User 4    | userdefault4@email.com     | User  |
| User 5    | userdefault5@email.com     | User  |
| User 6    | userdefault6@email.com     | User  |
| User 7    | userdefault7@email.com     | User  |
| User 8    | userdefault8@email.com     | User  |
| User 9    | userdefault9@email.com     | User  |
| User 10   | userdefault10@email.com    | User  |

## Функционал сайта

- **Регистрация и отписка от событий** — происходит в окне конкретного события.
- **Удаление и редактирование событий** доступно только администратору, также через окно события.
- **Добавление новых событий** разрешено администратору на главной странице.
- **Фильтрация событий по критериям** осуществляется через панель сортировки:
  - Поле "Имя" является уникальным и требует точного совпадения.
  - Остальные поля допускают частичное совпадение (без случайных символов внутри).

## Используемые технологии

- .NET 6.0
- Entity Framework Core
- MS SQL
- AutoMapper
- Fluent Validation
- Аутентификация через JWT (IdentityServer4)
- Swagger
- EF Fluent API
- React
- nUnit
- Clean Architecture
