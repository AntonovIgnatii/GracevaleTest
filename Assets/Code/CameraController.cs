using Code.Data;
using UnityEngine;

namespace Code
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform characterA; // Первый персонаж
        [SerializeField] private Transform characterB; // Второй персонаж
        [SerializeField] private Camera camera;
    
        private CameraModel _cameraModel;

        private float _roundTimer = 0f;
        private float _roamingTimer = 0f;
        private float _fovTimer = 0f;
        private float _currentFov;
        private float _targetFov;
        private Vector3 _roamingOffset;

        public void Initialize(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
            _currentFov = cameraModel.fovMin;
            _targetFov = cameraModel.fovMax;
            _roamingOffset = Vector3.zero;
        }

        private void Update()
        {
            if (characterA == null || characterB == null || camera == null) return;

            var center = (characterA.position + characterB.position) * 0.5f;

            // Круговое движение камеры
            _roundTimer += Time.deltaTime;
            var roundPhase = (_roundTimer % _cameraModel.roundDuration) / _cameraModel.roundDuration;
            var angle = roundPhase * Mathf.PI * 2f;
            var radius = _cameraModel.roundRadius + Mathf.Sin(_roundTimer * 0.8f) * _cameraModel.roamingRadius * 0.2f; // Небольшое изменение радиуса

            var height = _cameraModel.height + Mathf.Sin(_roundTimer * 0.75f) * _cameraModel.roamingRadius * 0.1f; // Небольшое покачивание по высоте

            // Гладко меняем радиус (имитация приближения)
            var distance = radius + Mathf.Sin(_roundTimer * 0.44f) * _cameraModel.roamingRadius * 0.5f;

            // Обновление roamingOffset для дрейфа
            _roamingTimer += Time.deltaTime;
            var roamingPhase = (_roamingTimer % _cameraModel.roamingDuration) / _cameraModel.roamingDuration;
        
            _roamingOffset = new Vector3(
                Mathf.PerlinNoise(roamingPhase, 0.1f) - 0.5f,
                Mathf.PerlinNoise(roamingPhase, 0.8f) - 0.5f,
                Mathf.PerlinNoise(0.9f, roamingPhase) - 0.5f
            ) * (_cameraModel.roamingRadius * 0.18f);

            // Положение камеры на окружности-шара вокруг центра
            var offset = new Vector3(
                Mathf.Cos(angle) * distance,
                height,
                Mathf.Sin(angle) * distance
            );

            var finalPos = center + offset + _roamingOffset;

            // Обновление FOV (зум вперед-назад)
            _fovTimer += Time.deltaTime;
            if (_fovTimer >= _cameraModel.fovDelay + _cameraModel.fovDuration)
            {
                _fovTimer = 0;
                (_targetFov, _currentFov) = (_currentFov, _targetFov);
            }
            var fovLerpPhase = Mathf.Clamp01((_fovTimer - _cameraModel.fovDelay) / _cameraModel.fovDuration);
            var fov = Mathf.Lerp(_currentFov, _targetFov, fovLerpPhase);

            // Плавное движение камеры
            camera.transform.position = Vector3.Lerp(camera.transform.position, finalPos, 0.08f);
        
            // Куда смотрит камера
            var lookAtPos = center + Vector3.up * _cameraModel.lookAtHeight;

            // Плавно крутим камеру на lookAtPos
            var targetRot = Quaternion.LookRotation(lookAtPos - camera.transform.position, Vector3.up);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetRot, 0.13f);

            // Установка FOV (если это камера)
            camera.fieldOfView = fov;
        }
    }
}