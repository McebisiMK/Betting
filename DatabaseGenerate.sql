------------------- CREATING DATABASE ------------------------------------

CREATE DATABASE OT_Assessment_DB

------------------- CREATING PROVIDER TABLE -------------------------------

CREATE TABLE [dbo].[Provider] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED ([Id] ASC)
);


------------------- CREATING GAME TABLE -----------------------------------------------

CREATE TABLE [dbo].[Game] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [ProviderId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       VARCHAR (100)    NOT NULL,
    [Theme]      VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Game_Provider] FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[Provider] ([Id])
);


------------------- CREATING ACCOUNT (PLAYER) TABLE -----------------------------------------------

CREATE TABLE [dbo].[Account] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Username] VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC)
);


------------------ CREATING CASINO WAGER TABLE ------------------------------------------------------

CREATE TABLE [dbo].[CasinoWager] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [AccountId]           UNIQUEIDENTIFIER NOT NULL,
    [GameId]              UNIQUEIDENTIFIER NOT NULL,
    [Amount]              DECIMAL (30, 15) NOT NULL,
    [CreatedDateTime]     DATETIME         NOT NULL,
    [TransactionId]       UNIQUEIDENTIFIER NOT NULL,
    [BrandId]             UNIQUEIDENTIFIER NOT NULL,
    [ExternalReferenceId] UNIQUEIDENTIFIER NOT NULL,
    [NumberOfBets]        INT              NOT NULL,
    [CountryCode]         VARCHAR (50)     NOT NULL,
    [SessionData]         VARCHAR (MAX)    NOT NULL,
    [Duration]            BIGINT           NOT NULL,
    [TransactionTypeId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CasinoWager] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CasinoWager_Game] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game] ([Id]),
    CONSTRAINT [FK_CasinoWager_Account] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblCasinoWager_AccountId]
    ON [dbo].[CasinoWager]([AccountId] ASC);


