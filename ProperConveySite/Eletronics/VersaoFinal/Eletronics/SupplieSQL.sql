/* procedures das tabelas de type simente select*/
/*C-RUD-> CREATE UPDATE READ DELETE*/ 


create table Supplier (NM_CorporateName varchar(40),CD_CNPJ varchar(14),NR_SupplierTypeID int)
create table SupplierType (NR_SupplierTypeID int identity primary key,DS_SupplierTypeDescription varchar(200))
alter table Supplier add constraint FK_SupplierTypePK_SupplierFK foreign key(NR_SupplierTypeID) references SupplierType(NR_SupplierTypeID) 



create function FN_GetSupplierTypeDescription (@SupplierTypeID int) --READ SupplierType --OK
RETURNS VARCHAR(200)
AS 
BEGIN	
	DECLARE @DESCRIPTION VARCHAR(200)	
	SET @DESCRIPTION = (SELECT TOP 1 DS_SupplierTypeDescription FROM SupplierType where NR_SupplierTypeID=@SupplierTypeID)
	RETURN @DESCRIPTION
END




CREATE FUNCTION FN_GetAllSupplierType() --READ 1 SUPPLIER --OK
RETURNS @SupplierType TABLE
   (
    SupplierTypeID int,
	DS_SupplierTypeDescription varchar(200)  
   )
AS
BEGIN
   INSERT @SupplierType
   select *from SupplierType
   RETURN 
END

CREATE PROCEDURE PR_SUPPLIER_INSERT @NM_CorporateName varchar(40),@CD_CNPJ varchar(14),@NR_SupplierTypeID int --CREATE Supplier --OK
AS
	insert into Supplier (NM_CorporateName,CD_CNPJ,NR_SupplierTypeID) values (@NM_CorporateName,@CD_CNPJ,@NR_SupplierTypeID)



CREATE FUNCTION FN_SupplierSingleSelect (@CD_CNPJ varchar(14)) --READ 1 SUPPLIER //OK
RETURNS @Supplier TABLE
   (
    NM_CorporateName varchar(40),
    CD_CNPJ varchar(14),
    NR_SupplierTypeID int
   )
AS
BEGIN
   INSERT @Supplier Select * from Supplier where 
   CD_CNPJ = @CD_CNPJ
   RETURN 
END 

CREATE FUNCTION FN_SupplierSelectAll () --READ 2 SUPPLIER --READ 2 //OK
RETURNS @Supplier TABLE
   (
    NM_CorporateName varchar(40),
    CD_CNPJ varchar(14),
    NR_SupplierTypeID int
   )
AS
BEGIN
   INSERT @Supplier Select * from Supplier 
   RETURN 
END 

CREATE PROCEDURE PR_SUPPLIER_UPDATE @CD_CNPJ varchar(14),@NM_CorporateName varchar(40),@NR_SupplierTypeID int --OK
AS
	update Supplier set NM_CorporateName = @NM_CorporateName ,NR_SupplierTypeID = @NR_SupplierTypeID where CD_CNPJ=@CD_CNPJ

CREATE PROCEDURE PR_SUPPLIER_DELETE @CD_CNPJ varchar(14)--DELETE
AS
	delete from Supplier where CD_CNPJ = @CD_CNPJ




