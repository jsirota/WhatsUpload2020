This solution contains 6 projects.

Project WhatsUpload.FE, is a complete, functioning, self-contained Razor Pages project.

The other 5 projects encompas a multi-tiered project architecture with separated REST API, Data, Service, UI, and Test projects.
These projects are incomplete, not quite functional yet, and need the following:
- Remove code from upload and download controllers, put in Core Service layer
- Move EF Migrations to Data project
- Write Tests in Test project


To run either application, you'll need 1 database table; create script here:
USE [WhatsUpload]
GO

/****** Object:  Table [dbo].[Upload]    Script Date: 6/4/2020 11:15:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Upload](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nchar](100) NULL,
	[FilePath] [nchar](200) NULL,
	[Name] [nchar](100) NULL,
	[Description] [nchar](1000) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_Upload] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


==========================
Future Considerations
- Host on cloud; store uploads in cloud storage (S3, blob)
- Add user login, authentication
- Improve UI with drag-n-drop file uploading
