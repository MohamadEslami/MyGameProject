using UnityEngine.InputSystem;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    Camera _camera = null;

    [SerializeField]
    PlayerInput _playerInput = null;

    [SerializeField]
    float _speed = 5.0f;

    [SerializeField]
    float _shotPerSecond = 10.0f;

    [SerializeField]
    float _bulletThrust = 10.0f;

    [SerializeField]
    Spawner _bulletSpawner = null;

    [SerializeField]
    Transform[] _bulletEmitPoint = null;

    InputAction _streengInput;
    InputAction _shootInput;

    float _shotDelay;
    float _lastShotTime;
    int _bulletEmitIndex;
    Vector3 _spriteHalfSize;     
    Vector2 _cameraHalfSize;      

    void Start()
    {
        _streengInput = _playerInput.actions["SteerTank"];
        _shootInput = _playerInput.actions["Shoot"];
        _spriteHalfSize = GetComponent<SpriteRenderer>().sprite.bounds.extents;     
        _spriteHalfSize.Scale(transform.localScale);                          
        _cameraHalfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);     //Method 3
        _shotDelay = 1.0f / _shotPerSecond;
        _lastShotTime = 0.0f;
        _bulletEmitIndex = 0;
    }

    // Update is called once per frame

    void Update()
    {
        Vector2 steering = _streengInput.ReadValue<Vector2>();     


        Vector3 delta = _speed * steering * Time.deltaTime;
        Vector2 newPosition = transform.position + delta;


        if (Mathf.Abs(newPosition.x) > (_cameraHalfSize.x - _spriteHalfSize.x))   
        {
            newPosition.x = transform.position.x;
        }

        if (Mathf.Abs(newPosition.y) > (_cameraHalfSize.y - _spriteHalfSize.y))   
        {
            newPosition.y = transform.position.y;
        }


        if (steering.y > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            transform.position = newPosition;
        }
        if (steering.y < 0)

        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 180));
            transform.position = newPosition;
        }
        if (steering.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -90));
            transform.position = newPosition;
        }
        if (steering.x < 0)

        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 90));
            transform.position = newPosition;

        }



            if (_shootInput.ReadValue<float>() == 1.0f)
            {
                float timeDelta = Time.time - _lastShotTime;
                if (timeDelta >= _shotDelay)
                {
                Transform t = _bulletEmitPoint[_bulletEmitIndex];
                Transform bullet = _bulletSpawner.Spawn(t.position, t.rotation);
                Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
                bulletBody.AddForce(transform.up * _bulletThrust);

                _bulletEmitIndex = (_bulletEmitIndex + 1) % _bulletEmitPoint.Length;
                    _lastShotTime = Time.time;

                }
            }



        }
    }
