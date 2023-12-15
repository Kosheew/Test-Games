using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : HealthController
{
    public static Movement instance = null;
    [SerializeField] private Text textUlt;
    [SerializeField] private Slider sliderUlt;

    [Header("Objects Move")]
    [SerializeField] private Joystick joystickLeft;
    [SerializeField] private Joystick joystickRight;
    private float rotationSpeed = 50f;

    public int powerUlt { get; set; }
    public bool teleportate { get; set; }
    private Rigidbody rb;
    private float speed = 2.5f;
    private int currentHealth;
    private int MaxHealth;
    private void Awake()
    {
        MaxHealth = health;
        Initial();
        powerUlt = 50;
        UpdateTextUlt();
        rb = GetComponent<Rigidbody>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeDependencies();
    }


    void Update()
    {
        Move();
      //  RotateCharacter();
      //  MoveCharacter();

    }

    public void UpdateTextUlt()
    {
        if (powerUlt <= 0) powerUlt = 0;
        else if(powerUlt >= 100) powerUlt = 100;
        if (powerUlt <= 100 || powerUlt >= 0)
        {
            sliderUlt.value = powerUlt;
            textUlt.text = $"{powerUlt} / 100";
        }

    } 
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        Vector3 moveDirection = transform.forward * verticalInput;
        rb.velocity = moveDirection * speed;
    }


    private void MoveCharacter()
    {
        float horizontalInput = joystickLeft.Horizontal;
        float verticalInput = joystickLeft.Vertical;
        
        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        if (inputDirection.magnitude >= 0.2f)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * inputDirection;
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        }
    }

    private void RotateCharacter()
    {
        float horizontalInput = joystickRight.Horizontal;
        float verticalInput = joystickRight.Vertical;

        if (horizontalInput != 0f || verticalInput != 0f)
        {
           
            float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Zone"))
        {
            teleportate = true;
            float radius = 4.75f;
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            float randomX = radius * Mathf.Cos(randomAngle);
            float randomZ = radius * Mathf.Sin(randomAngle);

            float yValue = 0.2f; 

            Vector3 randomPosition = new Vector3(randomX, yValue, randomZ);
            transform.position = randomPosition;
        }
        
    }
    public void ActivateUltimate()
    {
        if (powerUlt >= 100)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyRed");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy); 
            }
            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("EnemyBlue");
            foreach (GameObject enemy in enemies2)
            {
                Destroy(enemy); 
            }
            powerUlt = 0;
            UpdateTextUlt();         
        }
    }
    private void InitializeDependencies()
    {
        textUlt = GameObject.FindWithTag("TextUltTag").GetComponent<Text>();
        sliderUlt = GameObject.FindWithTag("SliderUltTag").GetComponent<Slider>();
        HealthNow = GameObject.FindWithTag("TextHealtTag").GetComponent<Text>();
        slidertHealth = GameObject.FindWithTag("SliderHealtTag").GetComponent<Slider>();
        joystickLeft = GameObject.FindWithTag("RightJoystic").GetComponent<Joystick>();
        joystickRight = GameObject.FindWithTag("LeftJoystic").GetComponent<Joystick>();

        UpdateTextUlt(); 
    }

    public void RestoreHalfHealth()
    {
        currentHealth += MaxHealth / 2;
        health += currentHealth;
        if (health >= 100) health = 100;
        UpdateText();
    }
    private void OnDestroy()
    {
        if (instance == this) instance = null;
        SceneManager.sceneLoaded -= OnSceneLoaded;   
    }
}

