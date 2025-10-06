using _WebGLFramework.Services.Runners;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _instance;
    public static Main I => _instance;

    [SerializeField] private Girl _girl;
    [SerializeField] private Monster _monster;
    [SerializeField] private FadeScreenService _fadeScreenService;
    [SerializeField] private DialogService _dialogService;
    [SerializeField] private MonsterAttackRunner _monsterAttackRunner;
    
    private InputService _inputService;
    private ICoroutineRunner _coroutineRunner;
    private Game _game;

    public InputService InputService => _inputService;
    public ICoroutineRunner CoroutineRunner => _coroutineRunner;
    public Game Game => _game;
    public Girl Girl => _girl;
    public Monster Monster => _monster;
    public FadeScreenService FadeScreenService => _fadeScreenService;
    public DialogService DialogService => _dialogService;
    public MonsterAttackRunner MonsterAttackRunner => _monsterAttackRunner;


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
        _game = new Game(_coroutineRunner);
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

    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _game.LoadRoom(_game.CurrentRoomView.RoomId);
        
        if (Input.GetKeyDown(KeyCode.F6))
            _game.QuickSave();
        
        if (Input.GetKeyDown(KeyCode.F9))
            _game.QuickLoad();
    }
    #endif
}
