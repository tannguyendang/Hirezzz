CREATE DATABASE Hirezzz;
GO
USE Hirezzz;
--DROP TABLE Category;
GO
CREATE TABLE Category(
	CategoryId TINYINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CategoryName NVARCHAR(64) NOT NULL,
	ParentId TINYINT NULL REFERENCES Category(CategoryId),
);
GO
--DROP PROC AddCategory
CREATE PROC AddCategory(
	@CategoryName NVARCHAR(64),
	@ParentId TINYINT
)
AS 
	INSERT INTO Category (CategoryName, ParentId) VALUES (@CategoryName, @ParentId)
GO
INSERT INTO Category (CategoryName, ParentId) VALUES
(N'Thể Loại', NULL)
,(N'Quốc Gia', NULL)
,(N'Video', NULL)
,(N'EDM', 1)
,(N'Rock', 1)
,(N'Pop', 1)
,(N'Instrumental', 1)
,(N'Legendary', 1)
,(N'Việt Nam', 2)
,(N'Âu Mỹ',2)
,(N'Nhật Bản',2)
,(N'MV',3)
,(N'Movie',3)
GO
--DROP TABLE Banner
CREATE TABLE Banner(
    BannerId TINYINT IDENTITY(1,1) NOT NULL,
    BannerName NVARCHAR(32) NOT NULL,
	ImageUrl VARCHAR(16) NOT NULL,
	BannerType TINYINT NOT NULL
	CONSTRAINT PK_Banner_BannerId PRIMARY KEY (BannerId)
);
GO
--DROP PROC AddBanner
CREATE PROC AddBanner(
	@BannerName NVARCHAR(32),
	@ImageUrl VARCHAR(16),
	@BannerType TINYINT
)
AS 
	INSERT INTO Banner(BannerName, ImageUrl, BannerType) VALUES (@BannerName, @ImageUrl, @BannerType)
GO
--DROP PROC EditBanner
CREATE PROC EditBanner(
    @BannerId TINYINT,
	@BannerName NVARCHAR(32),
	@ImageUrl VARCHAR(16),
	@BannerType TINYINT
)
AS 
	UPDATE Banner SET BannerName = @BannerName, ImageUrl = @ImageUrl, BannerType = @BannerType WHERE BannerId = @BannerId
GO
INSERT INTO Banner(BannerName, ImageUrl, BannerType) VALUES
	('slide1', 'slide1.jpg', 0),
	('slide2', 'slide2.jpg', 0),
	('slide3', 'slide3.jpg', 0),
	('slide4', 'slide4.jpg', 0),
	('Google Ads', 'quangcao.jpg', 1),
	('Shopee', 'quangcao1.jpg', 1),
	('Gamble', 'quangcao2.jpg', 1),
	('Shopee', 'quangcao1.jpg', 2),
	('Gamble', 'quangcao2.jpg', 2)
GO
--DROP TABLE Member;
CREATE TABLE Member(
	MemberId CHAR(32) NOT NULL PRIMARY KEY,
	Username VARCHAR(16) NOT NULL UNIQUE,
	Email VARCHAR(64) NOT NULL,
	Fullname NVARCHAR(64) NOT NULL,
	Gender BIT NOT NULL
);
GO
--select * from member
--DROP TABLE MemberPassword;
CREATE TABLE MemberPassword(
	MemberId CHAR(32) NOT NULL REFERENCES Member(MemberId),
	Password BINARY(64) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0,
);
GO
--DROP TABLE MemberStringPassword;
CREATE TABLE MemberStringPassword(
	MemberId CHAR(32) NOT NULL REFERENCES Member(MemberId),
	Password VARCHAR(256) NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
	IsDeleted BIT NOT NULL DEFAULT 0
	PRIMARY KEY(MemberId, Password)
);
GO
--DROP TABLE Role
CREATE TABLE Role(
	RoleId INT NOT NULL PRIMARY KEY,
	RoleName VARCHAR(32) NOT NULL UNIQUE
);
GO
INSERT INTO Role(RoleId, RoleName) VALUES 
	(ROUND(RAND() * 10000000, 0), 'user'),
	(ROUND(RAND() * 10000000, 0), 'vip')
GO
--DROP TABLE MemberInRole
CREATE TABLE MemberInRole(
	MemberId CHAR(32) not null references Member(MemberId),
	RoleId int not null references Role(RoleId),
	IsDeleted bit not null default 0
);
GO
DECLARE @RoleId INT = ROUND(RAND() * 10000000, 0);
INSERT INTO Role(RoleId, RoleName) VALUES 
	(@RoleId, 'Admin');
DECLARE @Id VARCHAR(32)= REPLACE(NEWID(), '-', '');
INSERT INTO Member (MemberId, Username, Email,Fullname, Gender) 
	VALUES (@Id, 'admin', 'tan.nguyendang.37@gmail.com', N'Nguyễn Đặng Hoàng Tân', 0);
INSERT INTO MemberPassword(MemberId, Password) 
	VALUES (@Id, HASHBYTES('SHA2_512', '123'));
INSERT INTO MemberStringPassword(MemberId, Password) 
	VALUES (@Id, CONVERT(VARCHAR(256), HASHBYTES('SHA2_512', '123'), 2));
GO
--DROP PROC LoginMember
CREATE PROC LoginMember(
@Usr VARCHAR(32),
@Pwd BINARY(64)
)
AS 
SELECT Member.*, IsDeleted, UpdatedDate FROM Member
JOIN MemberPassword ON Member.MemberId = MemberPassword.MemberId	
WHERE Username = @Usr AND Password = @Pwd;
GO
--DROP PROC RegisterMember
CREATE PROC RegisterMember(
@Id VARCHAR(32),
@Usr VARCHAR(32),
@Pwd BINARY(64),
@Eml VARCHAR(64),
@Fullname NVARCHAR(64),
@Gen BIT
)
AS 
BEGIN 
	IF EXISTS(SELECT Member.*, Password FROM Member
	JOIN MemberPassword ON Member.MemberId = MemberPassword.MemberId)
	BEGIN
		INSERT INTO Member(MemberId, Username, Email, Fullname, Gender) VALUES (@Id, @Usr, @Eml, @Fullname, @Gen)
		INSERT INTO MemberPassword(Password) VALUES (@Pwd)
	END
END
GO
-- DROP PROC AddMemberInRole
CREATE PROC AddMemberInRole(
	@MemberId CHAR(32),
	@RoleId INT
)
AS
BEGIN
	IF EXISTS(SELECT * FROM MemberInRole WHERE MemberId = @MemberId AND RoleId = @RoleId)
		UPDATE MemberInRole SET IsDeleted = ~ IsDeleted WHERE MemberId = @MemberId AND RoleId = @RoleId
	ELSE
		INSERT INTO MemberInRole(MemberId, RoleId) VALUES(@MemberId, @RoleId)
END
GO
--select * from Role LEFT JOIN MemberInRole ON Role.RoleId =  MemberInRole.RoleId
--	and IsDeleted = 0 and MemberId = '3929839D9BD7429AA934A55A4CBE443C'
--DROP PROC GetRolesByMember
CREATE PROC GetRolesByMember(
@Id char(32))
AS
	SELECT Role.*, CAST(IIF(MemberId IS NULL, 0, 1)AS BIT) Checked FROM Role LEFT JOIN MemberInRole ON Role.RoleId =  MemberInRole.RoleId
		AND IsDeleted = 0 AND MemberId = @Id;
GO
--DROP PROC DeleteMember
CREATE PROC DeleteMember(
	@MemberId CHAR(32)
)
AS 
	DELETE FROM MemberInRole WHERE MemberId = @MemberId
	DELETE FROM MemberStringPassword WHERE MemberId = @MemberId
	DELETE FROM MemberPassword WHERE MemberId = @MemberId
	DELETE FROM Member WHERE MemberId = @MemberId
GO
--DROP PROC ChangePassword
CREATE PROC ChangePassword(
	@Id VARCHAR(32),
	@OldPwd BINARY(64),
	@NewPwd BINARY(64)
)
as
begin
	if exists(select * from MemberPassword where MemberId = @Id and Password = @OldPwd)
	begin
		update MemberPassword set UpdatedDate = GETDATE(), IsDeleted = 1 where MemberId = @Id and Password = @OldPwd;
		insert into MemberPassword (MemberId, Password) values (@Id, @NewPwd);
	end
end
GO
--DROP TABLE Product
CREATE TABLE Product
(
	ProductId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryId TINYINT  NOT NULL REFERENCES Category(CategoryId),
	ProductName NVARCHAR(128) NOT NULL,
	ProductTypeId TINYINT NOT NULL REFERENCES ProductType(ProductTypeId), 
	ProductUrl VARCHAR(32) NOT NULL,
	Singer NVARCHAR(64) NOT NULL,
	CountryId TINYINT NOT NULL REFERENCES Country(CountryId),
	ImageUrl varchar(32)
)
GO
--DROP TABLE Country
CREATE TABLE Country
(
	CountryId TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CountryName VARCHAR(32) NOT NULL
)
GO
--DROP PROC GetCountry
CREATE PROC GetCountry(
	@CountryId TINYINT
)
AS 
	SELECT CountryName FROM Country WHERE CountryId = @CountryId
GO
INSERT INTO Country(CountryName) VALUES (N'Việt Nam'), (N'Âu Mỹ'), (N'Nhật Bản')

--DROP TABLE ProductType
CREATE TABLE ProductType
(
	ProductTypeId TINYINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ProductTypeName VARCHAR(32) NOT NULL
)
GO
--DROP PROC GetProductType
CREATE PROC GetProductType(
	@ProductTypeId TINYINT
)
AS 
	SELECT ProductTypeName FROM ProductType WHERE ProductTypeId = @ProductTypeId
GO
INSERT INTO ProductType(ProductTypeName) VALUES (N'Music'), (N'Video')

INSERT INTO Product(CategoryId, ProductName, ProductTypeId, ProductUrl, Singer, CountryId, ImageUrl) VALUES
	(4 , N'video', 2, 'Earth.webm', N'video', 2, N's2.jpg'),
	(4 , N'Level', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'X You', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Waiting For Love', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Fades Away', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Without You', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'True Believe', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Touch Love', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Touch Me', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),
	(4 , N'Sunset Jeus', 1, 'CoChangTraiVietLenCay.flac', N'Avicii', 2, N's2.jpg'),

	(5 , N'The Phoenix', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),
	(5 , N'Centuries', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),
	(5 , N'Immortals', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),
	(5 , N'Champion', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),
	(5 , N'Uma Thurman', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),
	(5 , N'Alone Together', 1, 'CoChangTraiVietLenCay.flac', N'Fall Out Boy', 2, N's2.jpg'),

	(6 , N'Có Chàng Trai Viết Lên Cây', 1, 'CoChangTraiVietLenCay.flac', N'Phan Mạnh Quỳnh', 1, N's2.jpg'),

	(7 , N'The Legend of Ashitaka', 1, 'CoChangTraiVietLenCay.flac', N'Joe Hisaishi', 3, N's2.jpg'),

	(8 , N'All Out of Love', 1, 'CoChangTraiVietLenCay.flac', N' Air Supply', 2, N's2.jpg'),

	(9 , N'Có Chàng Trai Viết Lên Cây', 1, 'CoChangTraiVietLenCay.flac', N'Phan Mạnh Quỳnh', 1, N's2.jpg'),

	(10 , N'All Out of Love', 1, 'CoChangTraiVietLenCay.flac', N'Air Supply', 2, N's2.jpg'),

	(11 , N'The Legend of Ashitaka', 1, 'CoChangTraiVietLenCay.flac', N'Joe Hisaishi', 3, N's2.jpg'),

	(12 , N'The Legend of Ashitaka', 2, 'CoChangTraiVietLenCay.flac', N'Joe Hisaishi', 3, N's2.jpg'),

	(13 , N'The Legend of Ashitaka', 2, 'CoChangTraiVietLenCay.flac', N'Joe Hisaishi', 3, N's2.jpg')
GO
--DROP PROC GetSongs
CREATE PROC GetSongs(
	@ProductId INT
)
AS
	SELECT * FROM Product WHERE @ProductId = ProductId
GO
--DROP TABLE Library
CREATE TABLE Library
(
	LibId VARCHAR(32) NOT NULL PRIMARY KEY,
	MemberId CHAR(32) NOT NULL,
	ProductId INT NOT NULL,
)
GO
--DROP PROC GetLibraries
CREATE PROC GetLibraries(
	@MemberId CHAR(32)
)
AS 
	SELECT Library.*, ImageUrl, ProductName, Singer, ProductUrl
	FROM Library JOIN Product ON Library.ProductId = Product.ProductId AND @MemberId = MemberId;
GO
--DROP PROC GetSongsInLibrary
CREATE PROC GetSongsInLibrary
AS
	SELECT ImageUrl, ProductName, Singer, ProductUrl
	FROM Library JOIN Product ON Library.ProductId = Product.ProductId
GO
--DROP PROC AddLibrary
CREATE PROC AddLibrary(
	@LibId VARCHAR(32),
	@MemberId CHAR(32),
	@ProductId INT
	)
AS
	BEGIN
		--IF EXISTS (SELECT * FROM Library WHERE @MemberId = MemberId)
			--UPDATE Library SET @LibId = LibId WHERE @MemberId = MemberId;
		IF EXISTS (SELECT * FROM Library WHERE @ProductId = ProductId AND @MemberId = MemberId)
			UPDATE Library SET @ProductId = ProductId WHERE @ProductId = ProductId AND @MemberId = MemberId;
		ELSE
			INSERT INTO Library(LibId, MemberId, ProductId) VALUES (@LibId, @MemberId, @ProductId)
	END
GO
--DROP PROC DeleteLibrary
CREATE PROC DeleteLibrary(
	@LibId VARCHAR(32)
)
AS
	DELETE FROM Library WHERE LibId = @LibId
GO