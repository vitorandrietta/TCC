--Create,Retrieve,update,delete








create Proc PR_INSERT_PurchaseStatus @CD_StatusId int,@DS_PurchaseStatusDescription varchar(200) AS --C
BEGIN
	insert into PurchaseStatus (CD_StatusId,DS_PurchaseStatusDescription) values (@CD_StatusId,@DS_PurchaseStatusDescription);
END

CREATE FUNCTION FN_PurchaseStatus_Select () --R1
RETURNS @PurchaseStatus TABLE
   (
    CD_StatusId int,
	DS_PurchaseStatusDescription varchar(200)
   )
AS
BEGIN
   INSERT @PurchaseStatus Select * from PurchaseStatus
   RETURN 
END



CREATE FUNCTION FN_PurchaseStatus_SelectSingle (@CD_StatusId int) --R2
RETURNS @PurchaseStatus TABLE
   (
    CD_StatusId int,
	DS_PurchaseStatusDescription varchar(200)
   )
AS
BEGIN
   INSERT @PurchaseStatus Select * from PurchaseStatus where CD_StatusId = @CD_StatusId
   RETURN 
END


Create proc PR_Update_PurchaseStatus @CD_StatusID int,@DS_PurchaseStatusDescription nvarchar(200)
AS
 Begin
	 update PurchaseStatus set DS_PurchaseStatusDescription = @DS_PurchaseStatusDescription where
	CD_StatusId = @CD_StatusID
 End

Create proc PR_Delete_PurchaseStatus @CD_StatusID int
AS
 Begin
	delete from PurchaseStatus where CD_StatusId = @CD_StatusID
 End;



create table PurchaseStatus(CD_StatusId int primary key, DS_PurchaseStatusDescription nvarchar(200) not null)


create table Product(CD_ProductId int identity primary key, NM_ProductName nvarchar(200) not null, DS_ProductDescription nvarchar(200) not null,QT_AvaiableQuantity int not null,CD_Supplier int not null)
--foreign key com outro banco de dados codigo de fornecedores para o produto ?



--CREATE
create proc PR_INSERT_Product @NM_ProductName nvarchar(200),@DS_ProductDescription nvarchar(200),@QT_AvaiableQuantity int,@CD_Supplier int 
AS
	Begin
		insert into Product (NM_ProductName,DS_ProductDescription,QT_AvaiableQuantity,CD_Supplier) values(@NM_ProductName,@DS_ProductDescription,@QT_AvaiableQuantity,@CD_Supplier)			
	End;



--READ1
CREATE FUNCTION FN_Product_Select () --R1
RETURNS @Product TABLE
   (
		CD_ProductId int primary key, 
		NM_ProductName nvarchar(200) not null, 
		DS_ProductDescription nvarchar(200) not null,
		QT_AvaiableQuantity int not null,CD_Supplier int not null
   )
AS
BEGIN
   INSERT @Product Select * from Product
   RETURN 
END

--READ 2 
CREATE FUNCTION FN_Product_SelectSingle (@CD_ProductId int) --
RETURNS @Product TABLE
   (
		CD_ProductId int primary key, 
		NM_ProductName nvarchar(200) not null, 
		DS_ProductDescription nvarchar(200) not null,
		QT_AvaiableQuantity int not null,CD_Supplier int not null
   )
AS
BEGIN
   INSERT @Product Select * from Product where CD_ProductId = @CD_ProductId
   RETURN 
END

--UPDATE

create proc PR_Update_Product @CD_ProductId int ,@NM_ProductName nvarchar(200) , @DS_ProductDescription nvarchar(200),@QT_AvaiableQuantity int ,@CD_Supplier int
AS
BEGIN
	update Product set NM_ProductName = @NM_ProductName,DS_ProductDescription = @DS_ProductDescription,@QT_AvaiableQuantity = @QT_AvaiableQuantity ,CD_Supplier = 
	@CD_Supplier where CD_Supplier = @CD_Supplier
END


--DELETE

create proc PR_DELETE_PRODUCT @CD_ProductId int 
AS
	BEGIN
		Delete from Product where CD_ProductId = @CD_ProductId
	END;


create table Purchase(CD_PurchaseID int identity,QT_ProductQuantity int not null,CD_ClientID int not null,CD_ProductId int not null,CD_PurchaseStatus int not null);
--foreign key com outra tabela, codigo de cliente de outra tabela


	create proc PR_PURCHASE_INSERT @QT_ProductQuantity int ,@CD_ClientID int,@CD_ProductId int,@CD_PurchaseStatus int--CREATE C
	AS
		BEGIN
		 Insert into Purchase (QT_ProductQuantity,CD_ClientID,CD_ProductId,CD_PurchaseStatus) values 
		 (@QT_ProductQuantity,@CD_ClientID,@CD_ProductId,@CD_PurchaseStatus)		
    	END

	

	CREATE FUNCTION FN_Purchase_Select () --R1
		RETURNS @Purchase TABLE
			(
				CD_PurchaseID int,
				QT_ProductQuantity int not null,
				CD_ClientID int not null,
				CD_ProductId int not null,
				CD_PurchaseStatus int not null
			)
    		AS
				BEGIN
					INSERT @Purchase Select * from Purchase
				   RETURN	
			    END


					CREATE FUNCTION FN_Purchase_SelectSingle (@CD_PurchaseId int) --R2
			RETURNS @Purchase TABLE
			(
				CD_PurchaseID int,
				QT_ProductQuantity int not null,
				CD_ClientID int not null,
				CD_ProductId int not null,
				CD_PurchaseStatus int not null
			)
    		AS
				BEGIN
					INSERT @Purchase Select * from Purchase where CD_PurchaseID = @CD_PurchaseId
				   RETURN	
			    END
			
			create proc PR_PURCHASE_UPDATE @CD_PurchaseID int,@QT_ProductQuantity int,@CD_ClientID int ,@CD_ProductId int ,@CD_PurchaseStatus int
			AS 
			  BEGIN
				update Purchase set QT_ProductQuantity = @QT_ProductQuantity, CD_ClientID = @CD_ClientID , CD_ProductId = @CD_ProductId  , CD_PurchaseStatus = @CD_PurchaseStatus  where CD_PurchaseID = @CD_PurchaseID 
			  END
			  
			  Create proc PR_PURCHASE_DELETE @CD_PurchaseId int AS
			  BEGIN
				delete from Purchase where CD_PurchaseId =@CD_PurchaseId
			  END
			  
alter table Purchase add constraint FK_ProductPK_PurchaseFK_1 foreign key(CD_ProductId) references Product(CD_ProductId)
alter table Purchase add constraint FK_PurchaseStatusPK_PurchaseFK_2 foreign key(CD_PurchaseStatus) references PurchaseStatus(CD_statusId)
  
 
 --FK_TableNamePK_TableNameFK_N,


 create table UserLogin(CD_UserId int identity primary key,NM_Username nvarchar(50),CD_Password nvarchar(300),CD_ClientID int not null,DT_RegisterDate datetime not null)
 --foreign key com outra tabela, codigo de cliente de outra tabela-> TABELA CLIENT


 create trigger TG_Purchase  on Purchase
 after insert
 AS
 Begin
	declare @productQuantity int;
	declare @productId int;
	set @productQuantity = (select QT_ProductQuantity from inserted);
	set @productId = (select CD_ProductId from inserted);
	update Product set QT_AvaiableQuantity = ((select QT_AvaiableQuantity from Product where CD_ProductId = @productId)-
	@productQuantity) where CD_ProductId = @productId 
  End



  create trigger TG_CancelPurchase  on Purchase
 after delete
 AS
 Begin
	declare @productQuantity int;
	declare @productId int;
	set @productQuantity = (select QT_ProductQuantity from inserted);
	set @productId = (select CD_ProductId from inserted);
	update Product set QT_AvaiableQuantity = ((select QT_AvaiableQuantity from Product where CD_ProductId = @productId)+
	@productQuantity) where CD_ProductId = @productId 
  End
  


 CREATE FUNCTION FN_ClientLogin(@username nvarchar(50), @password nvarchar(300))
RETURNS bit
	AS
		BEGIN
			declare @return bit
			if(EXISTS(SELECT*FROM UserLogin where CD_Password = @password and NM_Username=@username))
				BEGIN
					set @return = 1
				END
			else
				BEGIN
					set @return = 0
				END
			RETURN @return
		END

		