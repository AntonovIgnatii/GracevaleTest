using System;
using Code.Characters;
using Code.GamePlays;
using UnityEngine;

namespace Code
{
	public class EventBus
	{
		public Action<CharacterTeam> OnCallPlayerAttack;
		public Action<int, float> OnChangeHealth;
		public Action<GameMode> OnGameModeSelected;
		public Action<Vector3, string, Color> OnCallSendMessage;
	}
}
