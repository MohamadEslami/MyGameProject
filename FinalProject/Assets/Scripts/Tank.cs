using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public int Health;
    private float MaxHealth;
    public Image HealthBar;

    public float MoveSpeed;
    public float RotateSpeed;


    public GameObject Bullet;
    public GameObject Gun;

    public float PowerShoot;
    private float LatShoot;

    private int MyCoin;
    public Text MyCoin_txt;

    public int Kill;
    public int KillMax;
    public Text Kill_txt;


    public GameObject WinPanel;
    public GameObject GameOverPanel;

    [Header("Sounds")]
    public AudioClip Shoot_sound;
    public AudioClip GetCoin_sound;
    //public AudioClip Moving;

    private Rigidbody2D rb;
    private AudioSource au;

    public AudioSource GameMusic;





    [SerializeField]
    PlayerInput _playerInput = null;

    InputAction _streengInput;
    InputAction _shootInput;

   
    

    private void Start()
    {

        _streengInput = _playerInput.actions["SteerTank"];
        _shootInput = _playerInput.actions["Shoot"];
        rb = GetComponent<Rigidbody2D>();
        au = GetComponent<AudioSource>();
        MyCoin = 0;
        Kill = 0;
        MaxHealth = Health;
        Time.timeScale = 1;

    }

 

    private void Update()
    {

        if (_shootInput.ReadValue<float>() == 1.0f)
        {

            Shoot();

        }
        HealthBar.fillAmount = Health / MaxHealth;

        Kill_txt.text = "Kill : " + Kill + " / " + KillMax;


        if (Kill >= KillMax)
        {
            WinPanel.SetActive(true);
            WinPanel.transform.Find("Box").Find("Coin_txt").GetComponent<Text>().text = MyCoin.ToString();
            //GameMusic.mute = true;
            Time.timeScale = 0;
        }

    }


    private void FixedUpdate()
    {
        Vector2 steering = _streengInput.ReadValue<Vector2>();

        Vector3 delta = (Vector2)transform.up * steering.y * MoveSpeed * Time.deltaTime;
        Vector2 newPosition = transform.position + delta;

        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -steering.x * RotateSpeed * Time.deltaTime);
        transform.position = newPosition;
        
    }

    private void Shoot()
    {
        if (Time.time > LatShoot + PowerShoot)
        {
            au.PlayOneShot(Shoot_sound);
            GameObject Bullet_ = Instantiate(Bullet, Gun.transform.position, Quaternion.identity) as GameObject;
            Bullet_.transform.right = transform.up;
            LatShoot = Time.time;
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

        if (collision.gameObject.tag == "b_enemy")
        {
            Health -= 1;
            if (Health <= 0)
            {
                GameOverPanel.SetActive(true);
                GameOverPanel.transform.Find("Box").Find("Coin_txt").GetComponent<Text>().text = MyCoin.ToString();
                GameMusic.mute = true;
                Time.timeScale = 0;
            }

        }

        if (collision.gameObject.tag == "Health")
        {
            au.PlayOneShot(GetCoin_sound);
            Health += 1;
            Destroy(collision.gameObject);

        }
    }
}