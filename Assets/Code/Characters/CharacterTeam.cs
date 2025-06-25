using System;
using UnityEngine;

namespace Code.Characters
{
	[Serializable]
	public class CharacterTeam
	{
		[field: SerializeField] public int AllyId { get; private set; }
		[field: SerializeField] public int EnemyId { get; private set; }
	}
}