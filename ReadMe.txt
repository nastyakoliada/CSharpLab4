ѕри разработке решени€ дл€ лабоработрной работы номер 4 использовались
материалы лобораторной работы номер 3. 

“ак как контекст и модель данных, используемых дл€ работы с SQLite, теперь используетс€ 
в нескольких проектах, то они вынесены в отдельные проекты

–ешение содержит следующие проекты

1. MusicCatalog.Context - контекст подлкючени€ к бд SQLite
2. MusicCatalog.EntityModel - модели данных дл€ взаимодействи€ с контекстом бд
3. MusicCatalog.WebService - реализаци€ REST сервиса взаимодействи€ с контекстом MusicCatalog.Context, 
	использу€ модель данных MusicCatalog.EntityModel.
	ќписание сервиса - https://localhost:5001/swagger/
4. MusicCatalog.Laba4 - реализаци€ взаимодействи€ с пользователем дл€ работы с музыкальным каталогом
	и сериализацией через консоль
	- в файлы json, xml, 
	- в бд SQLite (используетс€ контекст MusicCatalog.Context и модель MusicCatalog.EntityModel),
	- через REST Service MusicCatalog.WebService
5. MusicCatalog.Test.XUnit - расширены тесты из Ћ–3. 
	ƒобавлен интегрированный тест музыкального каталога в реализации 
	через WebService (MusicCatalogLaba4Testing.TestWebServiceSerialization). ѕри выполнении теста
	WebService должен быть запущен.
	ј также модульные тесты работы контроллера сервиса - RestApiTesting не требующие запуска сервиса