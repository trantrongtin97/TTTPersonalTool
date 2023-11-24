--/*    
--|--------------------------------------------------------------------------|    
--| Name:     TTTMasterScript_Install_PersonalTool
--|    
--| Date:     10/20/2023
--| Version:  1.0
--|--------------------------------------------------------------------------|    
--| Purpose:  Set up database without Migrations for TTT.PersonalTool
--|                                                                                   
--|--------------------------------------------------------------------------|    
--| Modifications:     
--|     1.0 - 10/20/2023 ttt: Original ver
--|--------------------------------------------------------------------------|  

--*/
--/***************************************************************************
--/	1.	tblUser												**
--/	2.	tblRole												**
--/	3.	tblLog												**
--/	4.	tblItem												**
--/***************************************************************************/
--/*********************************************************************/
--/**********************************************************/
--/****************************************************/
--/******************************************/

--/************************************************************************* tblUser **************************
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE objects.object_id = OBJECT_ID(N'[dbo].[tblUser]')
          AND objects.type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[tblUser]
    (
        [Id] INT IDENTITY(1, 1) NOT NULL,
		[Username] VARCHAR(50) NOT NULL,
		[Password] VARCHAR(MAX) NOT NULL,
		[FirstName] VARCHAR(200) NULL,
		[LastName] VARCHAR(200) NULL,
		[ProfilePictureUrl] VARCHAR(MAX) NULL,
		[DateOfBirth] DATETIME NULL,
		[Theme] INT NULL,
		[CreatedDate] DATETIME NULL,
		[Role] VARCHAR(500) NULL,
		[TenantCode] NVARCHAR(500) NULL,
        CONSTRAINT [PK_tblUser]
            PRIMARY KEY CLUSTERED ([ID] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                 ) ON [PRIMARY]
    ) ON [PRIMARY];
END;
GO

--/************************************************************************* tblTenant **************************
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE objects.object_id = OBJECT_ID(N'[dbo].[tblTenant]')
          AND objects.type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[tblTenant]
    (
        [Id] INT IDENTITY(1, 1) NOT NULL,
		[Code] NVARCHAR(500) NOT NULL UNIQUE,
		[UserID] INT NOT NULL,
        CONSTRAINT [PK_tblTenant]
            PRIMARY KEY CLUSTERED ([ID] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                 ) ON [PRIMARY]
    ) ON [PRIMARY];
END;
GO

--/************************************************************************* tblLog **************************
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE objects.object_id = OBJECT_ID(N'[dbo].[tblLog]')
          AND objects.type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[tblLog]
    (
        [Id] INT IDENTITY(1, 1) NOT NULL,
		[Level] NVARCHAR(200) NULL,
		[EventName] NVARCHAR(200) NULL,
		[Source] NVARCHAR(200) NULL,
		[ExceptionMessage] NVARCHAR(MAX) NULL,
		[StackTrace] NVARCHAR(MAX) NULL,
		[CreateDate] DATETIME NULL,
		[UserId] INT NULL,
        CONSTRAINT [PK_tblLog]
            PRIMARY KEY CLUSTERED ([ID] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                 ) ON [PRIMARY]
    ) ON [PRIMARY];
END;
GO

--/************************************************************************* tblItem **************************
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE objects.object_id = OBJECT_ID(N'[dbo].[tblItem]')
          AND objects.type IN ( N'U' )
)
BEGIN
    CREATE TABLE [dbo].[tblItem]
    (
        [Id] INT IDENTITY(1, 1) NOT NULL,
		[Name] NVARCHAR(100) NOT NULL,
		[Description] NVARCHAR(300) NULL,
		[Price] FLOAT NULL
        CONSTRAINT [PK_tblItem]
            PRIMARY KEY CLUSTERED ([ID] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                 ) ON [PRIMARY]
    ) ON [PRIMARY];
END;
GO

