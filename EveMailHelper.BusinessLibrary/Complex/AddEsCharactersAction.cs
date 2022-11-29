using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.BusinessDataAccess.Utilities;
using System.Linq;

//using EVEStandard.Models;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsCharactersAction : IBizActionAsync<ICollection<int>, IDictionary<int, Character>>
    {
        private readonly CharacterDbAccess _dbAccess;
        private readonly CorporationDbAccess _corpDbAccess;
        private readonly AllianceDbAccess _allianceDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();
        DateTime _notOlderThan;

        public AddEsCharactersAction(
            CharacterDbAccess dbAccess,
            CorporationDbAccess corporationDbAccess,
            AllianceDbAccess allianceDbAccess,
            EVEStandardAPI esiClient,
            DateTime? notOlderThan = null
            )
        {
            _dbAccess = dbAccess;
            _corpDbAccess= corporationDbAccess;
            _allianceDbAccess= allianceDbAccess;
            _esiClient = esiClient;
            _notOlderThan = notOlderThan ?? DateTime.UtcNow - new TimeSpan(24, 0, 0);
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        EveDownloaderData<EVEStandard.Models.CharacterInfo, Character> characterDD;

        //IDictionary<int, Character> characters = new Dictionary<int, Character>();
        IDictionary<int, Corporation> corporations = new Dictionary<int, Corporation>(); 
        IDictionary<int, Alliance> alliances = new Dictionary<int, Alliance>();

        //HashSet<int> characterEveIds = null!;
        //HashSet<int> corporationEveIds = null!;
        //HashSet<int> allianceEveIds = null!;

        public async Task<IDictionary<int, Character>> ActionAsync(
            ICollection<int> peveCharacterIds, ICollection<int> peveCorporationIds, ICollection<int> peveAllianceIds
            )
        {
            characterDD = new(peveCharacterIds);

            //IDictionary<int, EVEStandard.Models.CharacterInfo> eveCharacterInfos = new Dictionary<int, EVEStandard.Models.CharacterInfo>();
            IDictionary<int, EVEStandard.Models.CorporationInfo> eveCorporationInfos = new Dictionary<int, EVEStandard.Models.CorporationInfo>();
            IDictionary<int, EVEStandard.Models.Alliance> eveAllianceInfos = new Dictionary<int, EVEStandard.Models.Alliance>();

            //HashSet<int> characterEveIds = peveCharacterIds.ToHashSet();
            HashSet<int> corporationEveIds = peveCorporationIds.ToHashSet();
            HashSet<int> allianceEveIds = peveAllianceIds.ToHashSet();

            while (characterDD.HasEveIds() || corporationEveIds.Count > 0 || allianceEveIds.Count > 0)
            {
                var charDto = await LoadAndStoreCharacters();
                // Merge Lists to general lists
                characters = characters.Merge(charDto.Characters);
                eveCharacterInfos = eveCharacterInfos.Merge(charDto.EveCharacterInfos);
                corporationEveIds.UnionWith(charDto.CorporationIds);
                allianceEveIds.UnionWith(charDto.AllianceIds);

                var corpDto = await LoadAndStoreCorporation(corporationEveIds);
                corporations = corporations.Merge(corpDto.Corporations);
                eveCorporationInfos = eveCorporationInfos.Merge(corpDto.EveCorporationInfos);
                characterEveIds.UnionWith(corpDto.CharacterIds);
                allianceEveIds.UnionWith(corpDto.AllianceIds);

                var allianceDto = await LoadAndStoreAlliances(allianceEveIds);
                alliances = alliances.Merge(allianceDto.Alliances);
                eveAllianceInfos = eveAllianceInfos.Merge(allianceDto.EveAllianceInfos);
                characterEveIds.UnionWith(allianceDto.CharacterIds);
                corporationEveIds.UnionWith(allianceDto.CorporationIds);
            }
            // finally use the eve infos to correct the dependencies by setting the objects
            FinalizeCharacters(characters, corporations, alliances, eveCharacterInfos);
            FinalizeCorporations(characters, corporations, alliances, eveCorporationInfos);
            FinalizeAlliances(characters, corporations, alliances, eveAllianceInfos);

            return characters;
        }

        private void FinalizeAlliances(
            IDictionary<int, Character> pcharacters,
            IDictionary<int, Corporation> pcorporations,
            IDictionary<int, Alliance> palliances,
            IDictionary<int, EVEStandard.Models.Alliance> eveAllianceInfos)
        {
            foreach (var eveKvp in eveAllianceInfos)
            {
                if (!palliances.ContainsKey(eveKvp.Key))
                    throw new Exception($"missing alliance key: {eveKvp.Key}");
                palliances[eveKvp.Key].CreatorCorporation = pcorporations[eveKvp.Value.CreatorCorporationId];
                if (eveKvp.Value.ExecutorCorporationId != null)
                    palliances[eveKvp.Key].ExecutorCorporation = pcorporations[eveKvp.Value.ExecutorCorporationId.Value];
                else
                    palliances[eveKvp.Key].ExecutorCorporation = null;
                palliances[eveKvp.Key].Creator = pcharacters[eveKvp.Value.CreatorId];

                _allianceDbAccess.Update(palliances[eveKvp.Key]);
            }
        }

        private void FinalizeCorporations(
            IDictionary<int, Character> pcharacters,
            IDictionary<int, Corporation> pcorporations,
            IDictionary<int, Alliance> palliances,
            IDictionary<int, EVEStandard.Models.CorporationInfo> eveCorporationInfos)
        {
            foreach (var eveKvp in eveCorporationInfos)
            {
                if (!pcorporations.ContainsKey(eveKvp.Key))
                    throw new Exception($"missing corp key: {eveKvp.Key}");
                pcorporations[eveKvp.Key].Ceo = pcharacters[eveKvp.Value.CeoId];
                pcorporations[eveKvp.Key].Creator = pcharacters[eveKvp.Value.CreatorId];
                if (eveKvp.Value.AllianceId != null)
                    pcorporations[eveKvp.Key].Alliance = palliances[eveKvp.Value.AllianceId.Value];
                else
                    pcorporations[eveKvp.Key].Alliance = null;
                _corpDbAccess.Update(pcorporations[eveKvp.Key]);
            }
        }

        private void FinalizeCharacters(
            IDictionary<int, Character> pcharacters,
            IDictionary<int, Corporation> pcorporations,
            IDictionary<int, Alliance> palliances,
            IDictionary<int, EVEStandard.Models.CharacterInfo> eveCharacterInfos
            )
        {
            foreach (var eveKvp in eveCharacterInfos)
            {
                if (!pcharacters.ContainsKey(eveKvp.Key))
                    throw new Exception($"missing character key: {eveKvp.Key}");
                pcharacters[eveKvp.Key].Corporation = pcorporations[eveKvp.Value.CorporationId];
                _dbAccess.Update(pcharacters[eveKvp.Key]);
            }
        }



        private class CharResultDTO
        {
            EveDownloaderData<EVEStandard.Models.CharacterInfo, Character> characterDD = new();
            public HashSet<int> CorporationIds = new();
            public HashSet<int> AllianceIds = new();
        }

        private async Task LoadAndStoreCharacters()
        {
            var workEveIds = characterDD.GetHashEveIds();
            var characters = _dbAccess.GetByEveIds(workEveIds).ToEveIdDictionary();

            // this caching is a pain
            foreach (var eveId in workEveIds)
            {
                Character character = new()
                {
                    EveId = eveId,
                };
                // continue if it's a new character or an old one that needs to be updated
                if (characters.ContainsKey(eveId))
                {
                    character = characters[eveId];
                    if (characters[eveId].EveLastUpdated >= _notOlderThan)
                        continue;
                }
                try
                {
                    var charInfo = (await _esiClient.Character.GetCharacterPublicInfoV5Async(eveId)).Model;
                    characterDD.Add(eveId, charInfo);

                    result.EveCharacterInfos.Add(eveId, charInfo);
                    result.CorporationIds.Add(charInfo.CorporationId);
                    if (charInfo.AllianceId != null)
                        result.AllianceIds.Add(charInfo.AllianceId.Value);
                    character.CopyShallow(charInfo);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unhandled error: {\"error\":\"Character has been deleted!\"}")
                    {
                        // this eveId will stay unknown forever and ever
                        if (!characters.ContainsKey(eveId)) // neuer Eintrag, daher kopieren
                            character.CopyShallow(new EVEStandard.Models.CharacterInfo()
                            {
                                Name = "Deleted",
                                Title = "Deleted",
                            });
                        character.EveDeletedInGame = true;
                    }
                }

                if (characters.ContainsKey(eveId))
                {
                    characters[eveId] = character;
                }
                else
                { 
                    characters.Add(eveId, character);
                }
                //_dbAccess.Update(character);
                //Characters.Add(character.EveId, character);
            }

            characterDD.MergeModels(characters);
            characterDD.ClearEveIds();
        }

        private class CorpResultDTO
        {
            public IDictionary<int, Corporation> Corporations = new Dictionary<int, Corporation>();
            public Dictionary<int, EVEStandard.Models.CorporationInfo> EveCorporationInfos = new();
            public HashSet<int> CharacterIds = new();
            public HashSet<int> AllianceIds = new();
        }

        private async Task<CorpResultDTO> LoadAndStoreCorporation(ICollection<int> workEveIds)
        {
            CorpResultDTO result = new();
            result.Corporations = _corpDbAccess.GetByEveIds(workEveIds).ToEveIdDictionary();

            // this caching is a pain
            foreach (var eveId in workEveIds)
            {
                Corporation corp = new()
                {
                    EveId = eveId,
                };
                // fortsetze nur wenn neuer Char oder alter Char der laenger als ... gecached ist
                if (result.Corporations.ContainsKey(eveId))
                {
                    corp = result.Corporations[eveId];
                    if (result.Corporations[eveId].EveLastUpdated >= _notOlderThan)
                        continue;
                }
                try
                {
                    var corpInfo = (await _esiClient.Corporation.GetCorporationInfoV5Async(eveId)).Model;
                    result.EveCorporationInfos.Add(eveId, corpInfo);
                    result.CharacterIds.Add(corpInfo.CeoId);
                    result.CharacterIds.Add(corpInfo.CreatorId);
                    if (corpInfo.AllianceId != null)
                        result.AllianceIds.Add(corpInfo.AllianceId.Value);
                    corp.CopyShallow(eveId, corpInfo);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unhandled error: {\"error\":\"Character has been deleted!\"}")
                    {
                        // this eveId will stay unknown forever and ever
                        if (!result.Corporations.ContainsKey(eveId)) // neuer Eintrag, daher kopieren
                            corp.CopyShallow(eveId, new EVEStandard.Models.CorporationInfo()
                            {
                                Name = "Deleted",
                                Description = "Deleted",
                            });
                        corp.EveDeletedInGame = true;
                    }
                }

                _corpDbAccess.Update(corp);
                result.Corporations.Add(corp.EveId, corp);
            }

            return result;
        }

        private class AllianceResultDTO
        {
            public IDictionary<int, Alliance> Alliances = new Dictionary<int, Alliance>();
            public Dictionary<int, EVEStandard.Models.Alliance> EveAllianceInfos = new();
            public HashSet<int> CharacterIds = new();
            public HashSet<int> CorporationIds = new();
        }

        private async Task<AllianceResultDTO> LoadAndStoreAlliances(ICollection<int> workEveIds)
        {
            AllianceResultDTO result = new();
            result.Alliances = _allianceDbAccess.GetByEveIds(workEveIds).ToEveIdDictionary();

            // this caching is a pain
            foreach (var eveId in workEveIds)
            {
                Alliance alliance = new()
                {
                    EveId = eveId,
                };
                // fortsetze nur wenn neuer Char oder alter Char der laenger als ... gecached ist
                if (result.Alliances.ContainsKey(eveId))
                {
                    alliance = result.Alliances[eveId];
                    if (result.Alliances[eveId].EveLastUpdated >= _notOlderThan)
                        continue;
                }
                try
                {
                    var allianceInfo = (await _esiClient.Alliance.GetAllianceInfoV3Async(eveId)).Model;
                    result.EveAllianceInfos.Add(eveId, allianceInfo);
                    result.CharacterIds.Add(allianceInfo.CreatorId);
                    result.CorporationIds.Add(allianceInfo.CreatorCorporationId);
                    if (allianceInfo.ExecutorCorporationId != null)
                        result.CorporationIds.Add(allianceInfo.ExecutorCorporationId.Value);
                    alliance.CopyShallow(eveId, allianceInfo);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unhandled error: {\"error\":\"Character has been deleted!\"}")
                    {
                        // this eveId will stay unknown forever and ever
                        if (!result.Alliances.ContainsKey(eveId)) // neuer Eintrag, daher kopieren
                            alliance.CopyShallow(eveId, new EVEStandard.Models.Alliance()
                            {
                                Name = "Deleted",
                                Ticker = "Deleted",
                            });
                        alliance.EveDeletedInGame = true;
                    }
                }

                _allianceDbAccess.Update(alliance);
                result.Alliances.Add(alliance.EveId, alliance);
            }

            return result;
        }
    }
}
