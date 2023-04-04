using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    //[SerializeField]
    //Camera _camera = null;

    //[SerializeField]
    //float _speed = 5.0f;

    //[SerializeField]
    //float _shotPerSecond = 8.0f;

    //[SerializeField]
    //float _bulletThrust = 10.0f;


    [SerializeField]
    PlayerInput _playerInput = null;

   
    public float MoveSpeed;
    public float RotateSpeed;

    private Rigidbody2D rb;

    private int MyCoin;
    public Text MyCoin_txt;

    private AudioSource au;

    public GameObject Bullet;
    public GameObject Gun;
    public float PowerShoot;
    private float LatShoot;


    InputAction _streengInput;
    InputAction _shootInput;

    

    float _shotDelay;
    float _lastShotTime;
    //    int _bulletEmitIndex;
    //Vector3 _spriteHalfSize;
    //Vector2 _cameraHalfSize;

    [Header("Sounds")]
    public AudioClip Shoot_sound;
    public AudioClip GetCoin_sound;
    public AudioClip Moving;

    public AudioSource GameMusic;
    void Start()
    {

        //_spriteHalfSize = GetComponent<SpriteRenderer>().sprite.bounds.extents;
        //_spriteHalfSize.Scale(transform.localScale);
        ////_cameraHalfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
        //_shotDelay = 1.0f / _shotPerSecond;
        //_lastShotTime = 0.0f;
        _streengInput = _playerInput.actions["SteerTank"];
        _shootInput = _playerInput.actions["Shoot"];
        rb = GetComponent<Rigidbody2D>();
        au = GetComponent<AudioSource>();
        MyCoin = 0;
        
    }

    // Update is called once per frame

    void Update()
    {
        Vector2 steering = _streengInput.ReadValue<Vector2>();

        Vector3 delta = (Vector2)transform.up * steering.y * MoveSpeed * Time.deltaTime;
        Vector2 newPosition = transform.position + delta;


        //if (Mathf.Abs(newPosition.x) > (_cameraHalfSize.x - _spriteHalfSize.x))
        //{
        //    newPosition.x = transform.position.x;

        //}

        //if (Mathf.Abs(newPosition.y) > (_cameraHalfSize.y - _spriteHalfSize.y))
        //{
        //    newPosition.y = transform.position.y;

        //}


       
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -steering.x * RotateSpeed * Time.deltaTime);
        transform.position = newPosition;
            //au.PlayOneShot(Moving);
       
        //if (steering.y < 0)

        //{
        //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 180));
        //    transform.position = newPosition;
        //    //au.PlayOneShot(Moving);
        //}
        //if (steering.x > 0)
        //{
        //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -90));
        //    transform.position = newPosition;
        //    //au.PlayOneShot(Moving);
        //}
        //if (steering.x < 0)

        //{
        //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 90));
        //    transform.position = newPosition;
        //    // au.PlayOneShot(Moving);

        //}





        if (_shootInput.ReadValue<float>() == 1.0f)
        {
            //float timeDelta = Time.time - _lastShotTime;
            //if (timeDelta >= _shotDelay)
            //{
            //Transform t = _bulletEmitPoint[_bulletEmitIndex];
            //Transform bullet = _bulletSpawner.Spawn(t.position, t.rotation);
            //Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
            //bulletBody.AddForce(transform.up * _bulletThrust);

            //_bulletEmitIndex = (_bulletEmitIndex + 1) % _bulletEmitPoint.Length;
            //    _lastShotTime = Time.time;

            //}
            if (Time.time > LatShoot + PowerShoot)
            {

                //    GameObject Bullet_ = Instantiate(Bullet, Gun.transform.position, Quaternion.identity) as GameObject;
                //    //Bullet_.transform.right = transform.up;
                //    LatShoot = Time.time;
                //}
                au.PlayOneShot(Shoot_sound);
                GameObject Bullet_ = Instantiate(Bullet, Gun.transform.position, Quaternion.identity) as GameObject;
                Bullet_.transform.right = transform.up;
                LatShoot = Time.time;
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {

            au.PlayOneShot(GetCoin_sound);
            MyCoin += 1;

            Destroy(collision.gameObject);
            MyCoin_txt.text = MyCoin.ToString();

        }

    }
}