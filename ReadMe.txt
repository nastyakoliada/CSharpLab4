��� ���������� ������� ��� ������������� ������ ����� 4 ��������������
��������� ������������ ������ ����� 3. 

��� ��� �������� � ������ ������, ������������ ��� ������ � SQLite, ������ ������������ 
� ���������� ��������, �� ��� �������� � ��������� �������

������� �������� ��������� �������

1. MusicCatalog.Context - �������� ����������� � �� SQLite
2. MusicCatalog.EntityModel - ������ ������ ��� �������������� � ���������� ��
3. MusicCatalog.WebService - ���������� REST ������� �������������� � ���������� MusicCatalog.Context, 
	��������� ������ ������ MusicCatalog.EntityModel.
	�������� ������� - https://localhost:5001/swagger/
4. MusicCatalog.Laba4 - ���������� �������������� � ������������� ��� ������ � ����������� ���������
	� ������������� ����� �������
	- � ����� json, xml, 
	- � �� SQLite (������������ �������� MusicCatalog.Context � ������ MusicCatalog.EntityModel),
	- ����� REST Service MusicCatalog.WebService
5. MusicCatalog.Test.XUnit - ��������� ����� �� ��3. 
	�������� ��������������� ���� ������������ �������� � ���������� 
	����� WebService (MusicCatalogLaba4Testing.TestWebServiceSerialization). ��� ���������� �����
	WebService ������ ���� �������.
	� ����� ��������� ����� ������ ����������� ������� - RestApiTesting �� ��������� ������� �������