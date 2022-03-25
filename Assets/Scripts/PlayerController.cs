using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player { get; private set; }

    private const float MIN_SPEED = 2f;
    private const float SPEED_CHANGE_VALUE = 0.2f;
    private const float SIDE_MOVEMENT_VALUE = 8f;
    
    [SerializeField]
    private float _startSpeed = 20f;

    private float _speed;

    private GameObject _selectedCarPrefab;

    private bool _wasLeftKeyPressed;

    private void Start()
    {
        EventStreams.Game.Publish(new GameSceneLoadedEvent());
    }
    private void Update()
    {
        Player.transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        
        if (Input.GetKeyDown(KeyCode.A) && !_wasLeftKeyPressed)
        {
            Player.transform.Translate(Vector3.left * SIDE_MOVEMENT_VALUE);
            _wasLeftKeyPressed = true;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && _wasLeftKeyPressed)
        {
            Player.transform.Translate(Vector3.right * SIDE_MOVEMENT_VALUE);
            _wasLeftKeyPressed = false;
            return;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            if (_speed > MIN_SPEED)
            {
                _speed -= SPEED_CHANGE_VALUE;
            }
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            _speed += SPEED_CHANGE_VALUE;
        }
    }
    
    public void Initialize(GameObject selectedCarPrefab)
    {
        _selectedCarPrefab = selectedCarPrefab;
        _wasLeftKeyPressed = false;
        Player = FindAndInstantiateSelectedCar();
    }

    private GameObject FindAndInstantiateSelectedCar()
    {
        var instanсe = Instantiate(_selectedCarPrefab);
        _speed = _startSpeed;
        return instanсe;
    }
}
