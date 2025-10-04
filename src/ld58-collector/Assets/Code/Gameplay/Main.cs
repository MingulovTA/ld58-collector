using _WebGLFramework.Services.Runners;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main I => _instance; 
    
    private InputService _inputService;
    private ICoroutineRunner _coroutineRunner;
    private Game _game;

    public InputService InputService => _inputService;
    public ICoroutineRunner CoroutineRunner => _coroutineRunner;
    public Game Game => _game;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        Init();
    }

    private void Init()
    {
        _inputService = new InputService();
        _coroutineRunner = GetCoroutineRunner();
        _game = new Game();
        _game.Run();
    }
    
    private ICoroutineRunner GetCoroutineRunner()
    {
        GameObject go = new GameObject();
        DontDestroyOnLoad(go);
        go.name = "CoroutineRunner";
        CoroutineRunner cr = go.AddComponent<CoroutineRunner>();
        return cr;
    }
}
