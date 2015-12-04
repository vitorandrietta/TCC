create table PurchaseStatus(CD_StatusId int primary key, DS_PurchaseStatusDescription nvarchar(200) not null)



create table Product(CD_ProductId int identity primary key, NM_ProductName nvarchar(200) not null, DS_ProductDescription nvarchar(200) not null,QT_AvaiableQuantity int not null,CD_Supplier int not null)
--foreign key com outro banco de dados codigo de fornecedores para o produto ?




create table Purchase(CD_PurchaseID int identity,QT_ProductQuantity int not null,CD_ClientID int not null,CD_ProductId int not null,CD_PurchaseStatus int not null);
--foreign key com outra tabela, codigo de cliente de outra tabela


alter table Purchase add constraint FK_ProductPK_PurchaseFK_1 foreign key(CD_ProductId) references Product(CD_ProductId)
alter table Purchase add constraint FK_PurchaseStatusPK_PurchaseFK_2 foreign key(CD_PurchaseStatus) references PurchaseStatus(CD_statusId)
  
 
 --FK_TableNamePK_TableNameFK_N,


 create table UserLogin(CD_UserId int identity primary key,NM_Username nvarchar(50),CD_Password nvarchar(300),CD_ClientID int not null,DT_RegisterDate datetime not null)
 --foreign key com outra tabela, codigo de cliente de outra tabela-> TABELA CLIENT


 