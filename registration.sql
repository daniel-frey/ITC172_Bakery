--create stored procedure for adding new people
Create proc [dbo].[usp_Register]
@lastname nvarchar(255),
@firstname nvarchar(255)=null,
@email nvarchar(255),
@Phone nvarchar(13) = null
As
--check to make sure person doesn't exit
If not exists
   (Select PersonKey
      From Person
	  Where PersonLastName=@LastName
	  And PersonEmail = @Email)
Begin--begin the block of if not exists
--begin the transaction
Begin tran
--begin the try catch
Begin Try
--insert into tables
Insert into Person
(
   PersonLastName, 
   PersonFirstName, 
   PersonEmail, 
   PersonDateAdded, 
   PersonPrimaryPhone
)
Values
(
   @LastName,
   @FirstName,
   @Email,
   getDate(),
   @Phone
)
--get the current person key
Declare @PersonKey int
Set @PersonKey=IDENT_CURRENT('Person')

  Commit tran
End try
Begin Catch
Rollback tran
Return -1
End catch
End
Else
Begin
Return -1
End
