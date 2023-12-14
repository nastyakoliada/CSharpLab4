При разработке решения для лабоработрной работы номер 4 использовались
материалы лобораторной работы номер 3. 

Так как контекст и модель данных, используемых для работы с SQLite, теперь используется 
в нескольких проектах, то они вынесены в отдельные проекты

Решение содержит следующие проекты

1. MusicCatalog.Context - контекст подлкючения к бд SQLite
2. MusicCatalog.EntityModel - модели данных для взаимодействия с контекстом бд
3. MusicCatalog.WebService - реализация REST сервиса взаимодействия с контекстом MusicCatalog.Context, 
	используя модель данных MusicCatalog.EntityModel.
	Описание сервиса - https://localhost:5001/swagger/
4. MusicCatalog.Laba4 - реализация взаимодействия с пользователем для работы с музыкальным каталогом
	и сериализацией через консоль
	- в файлы json, xml, 
	- в бд SQLite (используется контекст MusicCatalog.Context и модель MusicCatalog.EntityModel),
	- через REST Service MusicCatalog.WebService
5. MusicCatalog.Test.XUnit - расширены тесты из ЛР3. 
	Добавлен интегрированный тест музыкального каталога в реализации 
	через WebService (MusicCatalogLaba4Testing.TestWebServiceSerialization). При выполнении теста
	WebService должен быть запущен.
	А также модульные тесты работы контроллера сервиса - RestApiTesting не требующие запуска сервиса


После исправлений
1. Интерфейсы ISerializer и IMusicCatalog содержат асинхронные методы
2. Класс работы с музыкальным каталогом, которые реализовывали IMusicCatalog и ISerializer, это - MusicCatalog, классы сериализации MCSerializerXml и MCSerializerJSon, 
классы MusicСatalogRestClient, MusicCatalogSQLite изменены для поддержки асинхронной работы методов этих интерфейсов
3. Переписаны модульные тесты для тестирования асинхронных вызовов.