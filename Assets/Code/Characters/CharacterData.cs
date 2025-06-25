using System.Collections.Generic;
using Code.Data;

namespace Code.Characters
{
    public class CharacterData
    {
        public readonly List<Stat> Stats = new ();
        public readonly List<Buff> Buffs = new ();
    }
}
