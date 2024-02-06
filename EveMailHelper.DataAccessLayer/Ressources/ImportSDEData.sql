-- tool script for manual import from a fuzzyworks sde bacpac database

select Getdate()

insert into EveMailHelper.Sde.Icon
select IconId, iconfile, [description], '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.eveIcons

insert into EveMailHelper.Sde.Category
select categoryID, categoryName, iconID, published, '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.invCategories

-- fast and ugly fix, simply provide a description
Update EveSdeFuzzyworks.dbo.chrRaces set [description] = 'The Sleepers are (or were?) a presumably extinct human race which lived thousands of years before the playable EVE races. Their remaining installations and automated defence systems can be found throughout W-Space.'
where raceId=64

Update EveSdeFuzzyworks.dbo.chrRaces set [description] = 'I simply could not find a good description'
where raceId=32

insert into [Sde].[CharacterRace]
select raceId, raceName, [description], iconID, shortDescription, '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.chrRaces

insert into [Sde].[Group]
select groupId, categoryID, groupName, iconID, useBasePrice, anchored, anchorable, fittableNonSingleton, published, '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.invGroups

insert into [Sde].[MarketGroup]
select marketGroupId, parentGroupID, marketGroupName, [description], iconID, hasTypes, '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.invMarketGroups

insert into [Sde].[Graphic]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.eveGraphics

insert into [Sde].[EveType]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.invTypes

insert into [Sde].[IndustryActivity]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivity

-- delete mismatching keys (a single datarow clone bullshit)
delete EveSdeFuzzyworks.dbo.industryActivityMaterials
from EveSdeFuzzyworks.dbo.industryActivityMaterials iam
left outer join EveSdeFuzzyworks.dbo.invTypes t on (iam.materialTypeID = t.typeID)
where t.typeID is null

insert into [Sde].[IndustryActivityMaterial]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityMaterials

insert into [Sde].[IndustryActivityProbability]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityProbabilities

-- ok this are more "blueprints" without a resulting typeid
delete EveSdeFuzzyworks.dbo.industryActivityProducts
from EveSdeFuzzyworks.dbo.industryActivityProducts iam
left outer join EveSdeFuzzyworks.dbo.invTypes t on (iam.productTypeID = t.typeID)
join EveSdeFuzzyworks.dbo.invTypes t2 on (iam.typeID = t2.typeID)
where t.TypeID is  null 

insert into [Sde].[IndustryActivityProduct]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityProducts

insert into [Sde].[IndustryBlueprint]
select * , '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryBlueprints


