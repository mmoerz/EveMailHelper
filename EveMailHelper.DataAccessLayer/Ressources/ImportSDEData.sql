-- tool script for manual import from a fuzzyworks sde bacpac database

select Getdate()

insert into EveMailHelper.Sde.Icon
select IconId, iconfile, [description], '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.eveIcons

insert into EveMailHelper.Sde.Category
select categoryID, categoryName, iconID, published, '2024-02-06 10:28:28.233', 0  from EveSdeFuzzyworks.dbo.invCategories

/*
-- fast and ugly fix, simply provide a description
Update EveSdeFuzzyworks.dbo.chrRaces set [description] = 'The Sleepers are (or were?) a presumably extinct human race which lived thousands of years before the playable EVE races. Their remaining installations and automated defence systems can be found throughout W-Space.'
where raceId=64

Update EveSdeFuzzyworks.dbo.chrRaces set [description] = 'I simply could not find a good description'
where raceId=32
*/

insert into [Sde].[Race]
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

/*
-- delete mismatching keys (a single datarow clone bullshit)
delete EveSdeFuzzyworks.dbo.industryActivityMaterials
from EveSdeFuzzyworks.dbo.industryActivityMaterials iam
left outer join EveSdeFuzzyworks.dbo.invTypes t on (iam.materialTypeID = t.typeID)
where t.typeID is null
*/

insert into [Sde].[IndustryActivityMaterial]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityMaterials

insert into [Sde].[IndustryActivityProbability]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityProbabilities

/*
-- ok this are more "blueprints" without a resulting typeid
delete EveSdeFuzzyworks.dbo.industryActivityProducts
from EveSdeFuzzyworks.dbo.industryActivityProducts iam
left outer join EveSdeFuzzyworks.dbo.invTypes t on (iam.productTypeID = t.typeID)
join EveSdeFuzzyworks.dbo.invTypes t2 on (iam.typeID = t2.typeID)
where t.TypeID is  null 
*/

insert into [Sde].[IndustryActivityProduct]
select *, '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryActivityProducts

insert into [Sde].[IndustryBlueprint]
select * , '2024-02-06 10:28:28.233', 0 from EveSdeFuzzyworks.dbo.industryBlueprints

-- constellation
-- new db: eveid is first, then region id

-- solar system
-- new db: eveid is first, then regionid and constellation id

ALTER TABLE [Sde].[Faction] NOCHECK CONSTRAINT ALL

insert into [Sde].[Faction]
select factionID, factionName, [description], raceIDs, factionID, factionID, sizeFactor, factionID, iconID, '2024-02-06 10:28:28.233', 0 
from [evesdefuzzyworks].[dbo].[chrFactions]

insert into [Sde].[Region]
select * from [evesdefuzzyworks].[dbo].[mapRegions]

insert into [Sde].[Constellation]
select constellationid, regionID, constellationName, x, y, z, xMin, xMax, ymin, ymax, zmin, zmax, factionID, radius, '2024-02-06 10:28:28.233', 0 
from [evesdefuzzyworks].[dbo].[mapConstellations]

insert into [Sde].[SolarSystem]
select solarSystemID, regionID, constellationID, solarSystemName, x, y, z, xmin, xmax, ymin,ymax, zmin,zmax,
luminosity, border, fringe, corridor, hub, international, regional, [security], factionID, radius, sunTypeID, securityClass,
'2024-02-06 10:28:28.233', 0 
from [evesdefuzzyworks].[dbo].[mapSolarSystems]

insert into [Sde].[NpcCorporation]
select corporationID, size, extent, solarSystemID, friendID, enemyID, publicShares, initialPrice, minSecurity, factionID, [description], iconID, 
'2024-02-06 10:28:28.233', 0 
from [EveSdeFuzzyworks].[dbo].[crpNPCCorporations]

Update f
set f.SolarSystemId = chrf.solarSystemID,
 f.CorporationId = chrf.corporationID,
 f.MilitiaCorporationId = chrf.militiaCorporationID
from [Sde].[Faction] f
join [EveSdeFuzzyworks].[dbo].[chrFactions] chrf on (chrf.factionID = f.EveId)

ALTER TABLE [Sde].[Faction]  WITH CHECK CHECK CONSTRAINT ALL
