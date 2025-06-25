using System.Globalization;
using Code.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UIs
{
	public class StatPanel : MonoBehaviour
	{
		[SerializeField] private Image icon;
		[SerializeField] private TextMeshProUGUI text;

		private int _team;
		
		public void SetTeam(int team) => _team = team;
		
		public void UpdateByStat(Stat stat)
		{
			icon.sprite = Resources.Load<Sprite>($"Icons/{stat.icon}");
			text.text = stat.value.ToString(CultureInfo.InvariantCulture);
		}

		public void UpdateByBuff(Buff buff)
		{
			icon.sprite = Resources.Load<Sprite>($"Icons/{buff.icon}");
			text.text = buff.title;
		}

		public void UpdateByChangeValue(int team, float value)
		{
			if (_team != team) return;
			
			text.text = value.ToString(CultureInfo.InvariantCulture);
		}
	}
}
