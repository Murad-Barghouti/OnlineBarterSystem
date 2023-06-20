USE [OnlineBarterSystem]
GO

INSERT INTO [dbo].[Category]
           ([Name])
     VALUES
           ('Physical Goods'), ('Services'), ('Precious Metals'), ('Currencies'), ('Not Set'), ('Donations')
GO

Declare @physicalGoodsId as bigint;
Declare @servicesId as bigint;
Declare @preciousMetalsId as bigint;
Declare @currenciesId as bigint;
Declare @notSetId as bigint;
Declare @donationsId as bigint;


set @physicalGoodsId = (select Id from dbo.Category where Category.Name = 'Physical Goods');
set @servicesId = (select Id from dbo.Category where Category.Name = 'Services');
set @preciousMetalsId = (select Id from dbo.Category where Category.Name = 'Precious Metals');
set @currenciesId = (select Id from dbo.Category where Category.Name = 'Currencies');
set @notSetId = (select Id from dbo.Category where Category.Name = 'Not Set');
set @donationsId = (select Id from dbo.Category where Category.Name = 'Donations');

INSERT INTO [dbo].[SubCategory]
           ( [CategoryId], [Name])
     VALUES
           (@physicalGoodsId, 'Food'),
		   (@physicalGoodsId,'Cooked meal'),
		   (@physicalGoodsId,'Clothes'),
		   (@physicalGoodsId,'Electronics'),
		   (@physicalGoodsId,'Work Equipment'),
		   (@physicalGoodsId,'Sport Equipment'),
		   (@physicalGoodsId, 'Building Materials'),
		   (@physicalGoodsId, 'Miscellaneous'),
		   (@servicesId, 'Translation'),
		   (@servicesId, 'Copywriting'),
		   (@servicesId,'Software Development'),
		   (@servicesId,'Content creation/editing'),
		   (@servicesId,'Design'),
		   (@preciousMetalsId,'Gold'),
		   (@preciousMetalsId,'Silver'),
		   (@currenciesId,'Turkish Lira'),
		   (@currenciesId,'US Dollar'),
		   (@currenciesId, 'Euro'),
		   (@notSetId, 'Not Set'),
		   (@donationsId,'Donations')
GO

INSERT INTO [dbo].[City]
           ([Name])
     VALUES
           ('Mecca'), ('Madina'),('Jerusalem'),('Ankara'), ('Istanbul'),('Izmir'),('Trabzon'),('New york'),('Moscow'),('Berlin')
GO

INSERT INTO [dbo].[BarterState]
           ([Name])
     VALUES
           ('Active'), ('Pending Approval'),('Successful')
GO