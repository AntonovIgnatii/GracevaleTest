using System.Threading.Tasks;
using Code.Data;
using Code.GamePlays;
using Code.UIs;
using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GamePlayController gamePlayController;
        [SerializeField] private InterfaceController interfaceController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private TextEffectSpawner textEffectSpawner;
    
        [field: SerializeField] public GameMode GameMode { get; private set; }
        [field: SerializeField] public GameToken GameToken { get; private set; }
    
        private readonly GameParameterGenerator _gameParameterGenerator = new ();
        private readonly DataParser _parser = new ();
    
        private EventBus _eventBus;
    
        private void Awake() => Initialize();

        private void Initialize()
        {
            _parser.Parse();
            cameraController.Initialize(_parser.Data.cameraSettings);
            textEffectSpawner.Initialize();
        
            StartGame();
        }

        private async void StartGame()
        {
            await StartGameAsync();
        }
    
        private void UpdateMode(GameMode mode)
        {
            GameMode = mode;
            GameToken = GameToken.Restart;
        }

        private async Task StartGameAsync()
        {
            GameToken = GameToken.Progress;
            _eventBus = new EventBus();
        
            _eventBus.OnCallSendMessage -= textEffectSpawner.ShowText;
            _eventBus.OnCallSendMessage += textEffectSpawner.ShowText;
        
            var charactersData = _gameParameterGenerator.GeneratePlayers(_parser.Data, GameMode);
            interfaceController.Initialize(charactersData, _eventBus);
            gamePlayController.Initialize(charactersData, _eventBus);

            _eventBus.OnGameModeSelected -= UpdateMode;
            _eventBus.OnGameModeSelected += UpdateMode;

            while (GameToken != GameToken.Restart)
            {
                await Task.Delay(20);
            }
        
            StartGame();
        }
    }
}
