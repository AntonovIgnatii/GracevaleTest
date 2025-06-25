using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Data
{
	[Serializable]
	public class DataParser
	{
		[field: SerializeField] public Data Data { get; private set; }
	
		public void Parse()
		{
			var textAsset = Resources.Load<TextAsset>("data");
			Data = JsonConvert.DeserializeObject<Data>(textAsset.text);
		}
	}
}
