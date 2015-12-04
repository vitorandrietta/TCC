






create table Client(NM_FirstName varchar(200),NM_LastName varchar(200),CD_CPF varchar(11),CD_RG varchar(25),NR_ClientTypeID int) //OK!
create table ClientType(NR_ClientTypeID int identity primary key, DS_ClientTypeDescription varchar(200)) //OK!
alter table Client add constraint FK_ClientTypePK_ClientFK foreign key(NR_ClientTypeID) references ClientType(NR_ClientTypeID) //OK!


/* procedures das tabelas de type simente select*/
/*C-RUD-> CREATE UPDATE READ DELETE*/ 

create function FN_GetClientDescription (@ClientID int) --READ SupplierType --> OK !
RETURNS VARCHAR(200)
AS 
BEGIN	
	DECLARE @DESCRIPTION VARCHAR(200)	
	SET @DESCRIPTION = (SELECT TOP 1 DS_ClientTypeDescription FROM ClientType where NR_ClientTypeID=@ClientID)
	RETURN @DESCRIPTION
END

 

CREATE FUNCTION FN_SelectAllClientType () --READ 1 SUPPLIER //OK
RETURNS @ClientType TABLE
   (
    NR_ClientTypeID int,
	DS_ClientTypeDescription varchar(200)  
   )
AS
BEGIN
   INSERT @ClientType  
   select *from ClientType
   RETURN 
END




CREATE PROCEDURE PR_CLIENTE_INSERT  @NM_FirstName varchar(200),@NM_LastName varchar(200),@CD_CPF varchar(11),@CD_RG varchar(25),@NR_ClientTypeID int  //OK!
AS
	insert into Client (NM_FirstName,NM_LastName,CD_CPF,CD_RG,NR_ClientTypeID) values (@NM_FirstName,@NM_LastName,@CD_CPF,@CD_RG,@NR_ClientTypeID)


	
CREATE FUNCTION FN_ClientSelectSingle (@CD_CPF varchar(11)) --OK
RETURNS @Client TABLE
   (
    NM_FirstName varchar(200),
	NM_LastName varchar(200),
	CD_CPF varchar(11),
	CD_RG varchar(25),
	NR_ClientTypeID int
   )
AS
BEGIN
   INSERT @Client Select * from Client where CD_CPF = @CD_CPF
   RETURN 
END

CREATE FUNCTION FN_Client_Select () --READ 2 SUPPLIER --READ 2
RETURNS @Client TABLE
   (
    NM_FirstName varchar(200),
	NM_LastName varchar(200),
	CD_CPF varchar(11),
	CD_RG varchar(25),
	NR_ClientTypeID int
   )
AS
BEGIN
   INSERT @Client Select * from Client
   RETURN 
END


CREATE PROCEDURE PR_CLIENT_UPDATE @CD_CPF varchar(11),@NM_FirstName varchar(200),@NM_LastName varchar(200),@CD_RG varchar(25),@NR_ClientTypeID int
AS
	update Client set  NM_FirstName = @NM_FirstName, NM_LastName=@NM_LastName ,CD_RG = @CD_RG ,NR_ClientTypeID =@NR_ClientTypeID  where CD_CPF = @CD_CPF

CREATE PROCEDURE PR_CLIENT_DELETE @CD_CPF varchar(11)--DELETE
AS
	delete from Client where CD_CPF = @CD_CPF




