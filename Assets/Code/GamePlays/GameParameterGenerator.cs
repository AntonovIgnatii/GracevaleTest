using System.Collections.Generic;
using Code.Characters;
using Code.Data;
using UnityEngine;

namespace Code.GamePlays
{
    public class GameParameterGenerator
    {
        public List<CharacterData> GeneratePlayers(Data.Data data, GameMode mode)
        {
            var players = new List<CharacterData>();
            
            for (int i = 0; i < data.settings.playersCount; i++)
            {
                var player = new CharacterData();
                
                // Добавляем базовые статы
                foreach (var stat in data.stats)
                {
                    player.Stats.Add(new Stat() { id = stat.id, title = stat.title, value = stat.value, icon = stat.icon });
                }

                // Добавляем бафы если выбран соответствующий режим
                if (mode == GameMode.Buffs && data.settings.buffCountMax > 0 && data.buffs.Length > 0)
                {
                    var buffCount = Random.Range(data.settings.buffCountMin, data.settings.buffCountMax + 1);
                    var availableBuffs = new List<Buff>(data.buffs);

                    for (int j = 0; j < buffCount; j++)
                    {
                        Buff chosenBuff;
                        if (data.settings.allowDuplicateBuffs)
                        {
                            chosenBuff = data.buffs[Random.Range(0, data.buffs.Length)];
                        }
                        else
                        {
                            if (availableBuffs.Count == 0) break;
                            var pick = Random.Range(0, availableBuffs.Count);
                            chosenBuff = availableBuffs[pick];
                            availableBuffs.RemoveAt(pick);
                        }
                        player.Buffs.Add(chosenBuff);

                        // Модифицируем статы игрока согласно бафу
                        foreach (var mod in chosenBuff.stats)
                        {
                            var stat = player.Stats.Find(s => s.id == mod.statId);
                            if (stat != null)
                                stat.value += mod.value;
                        }
                    }
                }

                players.Add(player);
            }
            return players;
        }
    }
}