CREATE PROC PR_Product_ProductSelectByFilter @CD_ProductId int,@NM_ProductName nvarchar(200), 
								  @DS_ProductDescription nvarchar(200),@QT_AvaiableQuantity int,
								  @CD_Supplier int 
AS
	BEGIN
		declare @primeiro bit
		set @primeiro = 0
		declare @consulta nvarchar(300)

		if(NOT(@CD_ProductId is null) and NOT(@CD_ProductId=0))
			BEGIN
				set @primeiro =1
				set @consulta = 'Select*from Product where CD_ProductId =  @CD_ProductId';
			END 
		ELSE
			BEGIN
				if(NOT(@NM_ProductName is null) and NOT(@NM_ProductName=''))
					BEGIN
						set @consulta = 'Select*from Product where NM_ProductName=@NM_ProductName';
					    set @primeiro = 1
					END
               if(NOT(@DS_ProductDescription is null) and NOT(@DS_ProductDescription=''))
					BEGIN
						if(@primeiro = 0)
							BEGIN
								set @consulta = 'SELECT*FROM PRODUCT WHERE (CHARINDEX(@DS_ProductDescription,DS_ProductDescription))>0'
							END
						else
							BEGIN
								set @consulta = @consulta+' and (CHARINDEX(@DS_ProductDescription,DS_ProductDescription))>0'
							END	
						set @primeiro=1
					END
				
				if(NOT(@CD_Supplier is null) and NOT(@CD_Supplier = 0))
					BEGIN
						if(@primeiro = 0)
							BEGIN
								set @consulta = 'SELECT*FROM PRODUCT WHERE CD_Supplier=@CD_Supplier'
							END
						else
							BEGIN
								set @consulta = @consulta+' and CD_Supplier=@CD_Supplier'
							END	
						set @primeiro=1
					END
				
					if(NOT(@QT_AvaiableQuantity is null) and NOT(@QT_AvaiableQuantity = 0))
					BEGIN
						if(@primeiro = 0)
							BEGIN
								set @consulta = 'SELECT*FROM PRODUCT WHERE QT_AvaiableQuantity = @QT_AvaiableQuantity'
							END
						else
							BEGIN
								set @consulta = @consulta+' and QT_AvaiableQuantity = @QT_AvaiableQuantity'
							END	
						set @primeiro=1
					END

			END
	
		if(@primeiro = 0 )
		BEGIN
			set @Consulta = 'SELECT*FROM PRODUCT'
		END
		
		EXEC(@Consulta)
	
	END

	
CREATE PROC PR_ClientSelectByFilter @NM_FirstName varchar(200),	@NM_LastName varchar(200),@CD_CPF varchar(11),
									@CD_RG varchar(25),@NR_ClientTypeID int
AS
	BEGIN
		declare @primeiro bit
		set @primeiro = 0
		declare @consulta varchar(300)

		if(NOT(@CD_CPF is null) and NOT(@CD_CPF =''))
			BEGIN
				set @primeiro = 1 ;	
				set @consulta = 'select *from client where CD_CPF = @CD_CPF';
			END 		
		ELSE
		  BEGIN
			  if(NOT(@NM_FirstName is null) and NOT(@NM_FirstName =''))		
				BEGIN
					set @primeiro = 1;
					set @consulta = 'SELECT*FROM CLIENT WHERE (CHARINDEX(@NM_FirstName,NM_FirstName+NM_LastName))>0'				
				END
				
				  if(NOT(@NM_LastName is null) and NOT(@NM_LastName =''))		
					BEGIN
					
						--set @consulta = 'SELECT*FROM CLIENT WHERE (CHARINDEX(@NM_FirstName,NM_FirstName+NM_LastName))>0'				
						if(@primeiro = 0)
							BEGIN
								set @consulta = 'SELECT*FROM CLIENT WHERE (CHARINDEX(@NM_LastName,NM_FirstName+NM_LastName))>0'
							END

						else

							BEGIN
							   set @consulta =  @consulta + ' and (CHARINDEX(@NM_LastName,NM_FirstName+NM_LastName))>0'
							END	
						set @primeiro = 1;
					END
				if(NOT(@CD_RG is null) and NOT(@CD_RG =''))	
					BEGIN
						if(@primeiro = 0)			
							BEGIN
								set @consulta = 'SELECT FROM CLIENT WHERE CD_RG=@CD_RG'							
							END
						else
							BEGIN
								set @consulta = @consulta+ ' and CD_RG = @CD_RG'
							END
						set @primeiro = 1;
					END
				
				  if(NOT(@NR_ClientTypeID is null) and NOT(@NR_ClientTypeID=0)) 
					BEGIN
						if(@primeiro =0)
							BEGIN
								set @consulta = 'SELECT FROM CLIENT WHERE NR_ClientTypeID = @NR_ClientTypeID'
							END
						else
							BEGIN
								set @consulta = @consulta + ' and NR_ClientTypeID = @NR_ClientTypeID'
							END
						set @primeiro = 1;
					END
			  
		  END
	
	if(@primeiro = 0 )
		BEGIN
			set @Consulta = 'SELECT*FROM CLIENT'
		END
		
		EXEC(@Consulta)

	END


	
CREATE PROC PR_SupplierSelectByFilter 
@NM_CorporateName varchar(40),
@CD_CNPJ varchar(14),
@NR_SupplierTypeID int
 --READ 1 SUPPLIER


AS
BEGIN
   --if (@value is null or @value = '')
   declare @Consulta varchar(300)
   declare @primeiro bit
   set @primeiro = 0;

   if( NOT(@CD_CNPJ is null) and NOT(@CD_CNPJ ='')) 
	BEGIN
			set @Consulta = 'Select * from Supplier where CD_CNPJ=@CDCNPJ';      
			set @primeiro = 1;		
	END
   
   Else
		BEGIN

			if(NOT(@NM_CorporateName is null) and NOT(@NM_CorporateName='')) 
			BEGIN
				set @Consulta = 'Select * from Supplier where NM_CorporateName = @NM_CorporateName';
				set @primeiro = 1;		
			END
			
			if(NOT(@NR_SupplierTypeID is null) and NOT(@NR_SupplierTypeID=0)) 
			BEGIN
			     if(@primeiro = 1)
					BEGIN
						set @Consulta = @Consulta+' and NR_SupplierTypeID=@NR_SupplierTypeID';
					END
				 else
					 BEGIN
						set @Consulta = 'Select * from Supplier where NR_SupplierTypeID = @NR_SupplierTypeID'
					 END
				set @primeiro = 1;		
			END
		
		END
		
		if(@primeiro = 0 )
		BEGIN
			set @Consulta = 'SELECT*FROM SUPPLIER'
		END
		
		EXEC(@Consulta)

    RETURN 
END

