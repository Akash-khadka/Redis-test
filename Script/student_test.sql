
CREATE TABLE [dbo].[student_test](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [varchar](30) NULL,
	[Address] [varchar](30) NULL,
	[PhoneNumber] [bigint] NULL,
	[Class] [int] NULL,
	[EmailId] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


