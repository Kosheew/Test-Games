using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Movement : HealthController
{
    public static Movement instance = null;
    [SerializeField] private Text textUlt;
    [SerializeField] private Slider sliderUlt;

    [Header("Objects Move")]
    [SerializeField] private Joystick joystickLeft;
    [SerializeField] private Joystick joystickRight;
    [SerializeField] private int numberOfRays = 60;
    [SerializeField] private float visionRange = 2f;
    [SerializeField] private float rotationSpeed = 50f;
    public int powerUlt { get; set; }
    public bool teleportate { get; set; }
    private Rigidbody rb;
    private float speed = 2.5f;

    private void Awake()
    {
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
    void Start()
    {
        Initial();
        powerUlt = 50;
        UpdateTextUlt();      
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
      //  RotateCharacter();
      //  MoveCharacter();
    }

    public void UpdateTextUlt()
    {
        
        sliderUlt.value = powerUlt;
        textUlt.text = $"{powerUlt} / 100";
    } 
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the character based on A and D keys
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // Calculate the movement direction after rotation
        Vector3 moveDirection = transform.forward * verticalInput;

        // Apply velocity to the Rigidbody
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
            // Генерація випадкового кута у радіанах
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            // Обчислення координат за полярними координатами
            float randomX = radius * Mathf.Cos(randomAngle);
            float randomZ = radius * Mathf.Sin(randomAngle);

            // Для Y можна вибрати конкретне значення або залишити його як є, залежно від ваших потреб
            float yValue = 0.2f; // Наприклад, висота позиції

            Vector3 randomPosition = new Vector3(randomX, yValue, randomZ);
            transform.position = randomPosition;
        }
        
    }

}

