drop table feeds;
drop table messages;
drop table deals;
drop table ads;
drop table users;

CREATE TABLE [dbo].[users] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
	[name]     NVARCHAR (50) NOT NULL,
	[picture]  NVARCHAR (255) NULL,
    [login]    NVARCHAR (50) NOT NULL UNIQUE,
    [password] NVARCHAR (50) NOT NULL,
    [moderator] BIT NOT NULL DEFAULT 0,
    [admin]     BIT NOT NULL DEFAULT 0,
    [disabled]  BIT NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[ads] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [uid]       INT FOREIGN KEY REFERENCES [dbo].[users]([id]) NOT NULL,
	[title]     NVARCHAR (50) NOT NULL,
    [text]      NVARCHAR (255) NULL,
    [picture]   NVARCHAR (255) NULL,
    [value]     FLOAT NULL,
    [is_arenda] BIT NOT NULL DEFAULT 1,
    [is_daily]  BIT NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([id] ASC)
    --CONSTRAINT [FK_Table_ToTable] FOREIGN KEY ([Column]) REFERENCES [ToTable]([ToTableColumn])

);


CREATE TABLE [dbo].[deals] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [uid_buyer]  INT FOREIGN KEY REFERENCES [dbo].[users]([id]) NOT NULL,
    [ad_id]        INT FOREIGN KEY REFERENCES [dbo].[ads]([id]) NOT NULL,

	[date_in]     DATE NOT NULL,
    [date_out]    DATE  NULL,
    [is_confimed] BIT NOT NULL DEFAULT 0,
    [is_paid]     BIT NOT NULL DEFAULT 0,
    
    [cost]      MONEY NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[feeds] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [deal_id]    INT FOREIGN KEY REFERENCES [dbo].[deals]([id]) NOT NULL,
    [text]       NVARCHAR (255) NOT NULL,
    [moderated]  BIT NOT NULL DEFAULT 0,
    [mark]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[messages] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [uid_sender]    INT FOREIGN KEY REFERENCES [dbo].[users]([id]) NOT NULL,
    [uid_reciever]  INT FOREIGN KEY REFERENCES [dbo].[users]([id]) NOT NULL,
    [text]       NVARCHAR (255) NOT NULL,
	[date]     DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY CLUSTERED ([id] ASC)
);


-- Created tables

insert into users (name, login, password, moderator, admin) values 
    ('Admin', 'adm', 'admpass', 1, 1),
    ('Moder', 'mod', 'mod', 1, 0),
    (N'Яковский', 'yak', 'yak', 0, 0),
    (N'Иванов', 'i', 'i', 0, 0);
    
insert into ads (uid, title, text, value) values 
    ((select id from users where login='yak'), N'Домик на берегу финки', N'Комфорт, уют обеспечен', 2000),
    ((select id from users where login='i'), N'Отель в геленджике', N'С видами на море и всеми удобствами', 3000);

insert into messages ( uid_sender, uid_reciever, text) values 
( (select id from users where login='i'), (select id from users where login='yak'), 
        N'Здравствуйте! Можно будет снять на первую половину июня?' ),
( (select id from users where login='yak'), (select id from users where login='i'), 
        N'Здравствуйте! Конечно, без проблем, оформляйте заявку!' );

insert into deals (uid_buyer, ad_id, date_in, date_out, cost) values 
  ((select TOP 1 id from users where login='i'), (select TOP 1 id from ads where uid=(select id from users where login='yak')), '2024-06-01', '2024-06-20', 3000*20);

insert into feeds (deal_id, text, mark) values 
  ((select TOP 1 id from deals), N'Отпуск вышел потрясающим!  Собственнику спасибо за предоставленное жильё, пять звёзд!', 5);
select * from users;
select * from ads;
select * from deals;
select * from feeds;
select * from messages;

select * from ads where is_daily in (0,1) and title like N'%Дa%';
