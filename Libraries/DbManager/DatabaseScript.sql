USE [master]
GO
/****** Object:  Database [ImagesDatabase]    Script Date: 31/05/2019 09:09:01 a. m. ******/
CREATE DATABASE [ImagesDatabase]
GO
USE [ImagesDatabase]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 31/05/2019 09:09:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Filename] [varchar](200) NOT NULL,
	[ImageContent] [image] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertImage]    Script Date: 31/05/2019 09:09:02 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertImage] (
	@fileName VARCHAR(200),
	@content IMAGE,
	@id INT OUTPUT
)
AS
BEGIN
	INSERT INTO Images ([Filename], [ImageContent])
	VALUES (@fileName, @content);

	SELECT @id = @@IDENTITY;
END
GO
USE [master]
GO
ALTER DATABASE [ImagesDatabase] SET  READ_WRITE 
GO
