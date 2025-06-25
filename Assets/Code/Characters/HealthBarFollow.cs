using UnityEngine;

namespace Code.Characters
{
	public class HealthBarFollow : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Vector3 offset;
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private Camera mainCamera;
		[SerializeField] private float referenceFOV = 60f;

		void Update()
		{
			var worldPosition = target.position + offset;
			var screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
			rectTransform.position = screenPosition;
			
			var scale = Mathf.Tan(Mathf.Deg2Rad * referenceFOV * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView * 0.5f);
			rectTransform.localScale = Vector3.one * scale;
		}
	}
}