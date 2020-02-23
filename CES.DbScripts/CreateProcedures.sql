USE CES
Go

CREATE PROCEDURE Approve
	@UserId UniqueIdentifier,
	@AppId  UniqueIdentifier
AS
	BEGIN TRY
	BEGIN TRANSACTION
		Update UserApplicationAccess
		Set
		Granted = 1
		Where
		UserId=@UserId
		And
		ApplicationId=@AppId;
			
	COMMIT;		
	END TRY
	BEGIN CATCH
	 ROLLBACK;
	 Throw;
	END CATCH
Go

CREATE PROCEDURE AuthenticateUser
	@Username varchar(100),
	@Password varchar(500)
AS
	Select 
	u.Id,
	u.Username,
	u.RefreshToken,
	u.Role
	From 
	Users As u
	Where
	u.Username = @Username
	And
	u.Password = @Password;
Go

Create PROCEDURE ChangePassword
	@Username Varchar(100),
	@Password Varchar(100)
AS
	BEGIN TRY
		BEGIN TRANSACTION
			Update Users
			Set Password = @Password
			Where
			Username = @Username;
		COMMIT;
		END TRY
	BEGIN CATCH
	 ROLLBACK;
	 Throw;
	END CATCH
Go

Create PROCEDURE ForgotPassword
	@Username Varchar(100)
AS
	Select 
	u.Password	
	From	Users	As u
	Where
	u.Username = @Username;
Go

CREATE PROCEDURE GetAllUsers
AS
	Select
	u.Id,
	u.RefreshToken,
	u.Username	
	From
	Users u
	Where
	u.Role = 'User';
Go

CREATE PROCEDURE GetAppRequest
	@UserId UniqueIdentifier,
	@AppId  UniqueIdentifier
AS
	Select
	count(uaa.Id)
	From
	UserApplicationAccess As uaa
	Where
	uaa.UserId = @UserId
	And
	uaa.ApplicationId = @AppId;
Go

CREATE PROCEDURE GetUser
@UserId		UniqueIdentifier
AS
	Select
	u.Id,
	u.RefreshToken,
	u.Username	
	From
	Users u
	Where
	u.Id = @UserId;
Go

CREATE PROCEDURE GetUserAppReqDetails
	@Username Varchar(100)
AS
	Select 
	a.Name			As AppName,
	uaa.AccessType	As AccessType
	From	Applications			As a
	Join	UserApplicationAccess	As uaa	On a.Id = uaa.ApplicationId
	Join	Users					As u	On uaa.UserId = u.Id
	Where
	u.Username = @Username;
Go

CREATE PROCEDURE GetUserDetails
	@Username Varchar(100)
AS
	Select 
	u.Id,
	u.Username,
	u.RefreshToken,
	u.Password,
	u.Role
	From	Users	As u
	Where
	u.Username = @Username;
Go

CREATE PROCEDURE GetUserId
	@Username		Varchar(100),
	@RefreshToken	Varchar(100)
AS
	Select
	u.Id
	From Users u
	Where
	u.Username = @Username
	And
	u.RefreshToken = @RefreshToken;
Go

CREATE PROCEDURE GetUserIdByUsername
	@Username		Varchar(100)
AS
	Select 
	u.Id
	From
	Users u
	Where
	u.Username = @Username
Go

CREATE PROCEDURE Reject
	@UserId UniqueIdentifier,
	@AppId  UniqueIdentifier
AS
	BEGIN TRY
	BEGIN TRANSACTION
		Update UserApplicationAccess
		Set
		Granted = 0
		Where
		UserId=@UserId
		And
		ApplicationId=@AppId;
			
	COMMIT;		
	END TRY
	BEGIN CATCH
	 ROLLBACK;
	 Throw;
	END CATCH
Go

CREATE PROCEDURE RemoveRefreshToken
	@UserId		UniqueIdentifier
AS
	BEGIN TRY
			BEGIN TRANSACTION
					Update Users
					Set
					RefreshToken = null
					Where
					Id=@UserId
					;
			
			COMMIT;
			
			Select 
			u.Id,
			u.Username,
			u.RefreshToken,
			u.Role
			From 
			Users As u
			Where
			u.Id=@UserId
	
		END TRY
		BEGIN CATCH
		 ROLLBACK;
		 Throw;
		END CATCH
Go

CREATE PROCEDURE SaveApplication
	@ApplicationName	Varchar(100)
AS
BEGIN TRY
			BEGIN TRANSACTION
					Declare @AppId UniqueIdentifier;
					Set @AppId = NEWID();

					Insert 
					Into 
					Applications
					(
					Id,
					Name
					)
					Values(@AppId,@ApplicationName);
			COMMIT;

			Select 
			app.Id,
			app.Name
			From 
			Applications As app
			Where
			app.Id = @AppId;
		END TRY
		BEGIN CATCH
		 ROLLBACK;
		 Throw;
		END CATCH
Go

CREATE PROCEDURE SaveAppRequest
	@UserId		UniqueIdentifier,
	@AppId		UniqueIdentifier,
	@AccessType Varchar(10)
AS
	BEGIN TRY
			BEGIN TRANSACTION
					Insert Into 
					UserApplicationAccess
					(
					 Id,
					 ApplicationId,
					 UserId,
					 AccessType
					)
					Values
					(
					NewId(),
					@AppId,
					@UserId,
					@AccessType
					);
			COMMIT;
			
		END TRY
		BEGIN CATCH
		 ROLLBACK;
		 Throw;
		END CATCH
Go


CREATE Procedure SaveRefreshToken
	@UserID			UniqueIdentifier,		
	@RefreshToken	varchar(100)
	As 
	
		BEGIN TRY
			BEGIN TRANSACTION
					Update Users
					Set
					RefreshToken = @RefreshToken
					Where
					Id = @UserID;
			COMMIT;
			Select 
			u.Id,
			u.Username,
			u.RefreshToken,
			u.Role
			From 
			Users As u
			Where
			Id = @UserID;
		END TRY
		BEGIN CATCH
		 ROLLBACK;
		 Throw;
		END CATCH
Go

