using UnityEngine;

public class BallController : MonoBehaviour
{
    public Color[] colors = new Color[4]; // массив из 4 цветов: красный, зелёный, жёлтый, синий
    private int currentColorIndex = 0;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 5f;
    public ScoreManager scoreManager;
    private float screenLeftLimit;
    private float screenRightLimit;
    private float screenTopLimit;
    private float screenBottomLimit;
    private bool isGameOver = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        screenLeftLimit = screenBottomLeft.x;
        screenRightLimit = screenTopRight.x;
        screenTopLimit = screenTopRight.y;
        screenBottomLimit = screenBottomLeft.y;
    }

    void Update()
    {
        if (!isGameOver)
        {
            HandleMovement();
            HandleColorChange();
        }
    }

    void HandleMovement()
    {
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionY = Input.GetAxis("Vertical"); 
        Vector3 move = new Vector3(moveDirectionX * moveSpeed * Time.deltaTime, moveDirectionY * moveSpeed * Time.deltaTime, 0);
        transform.position += move;

        // Ограничение позиции мяча в пределах экрана
        float clampedX = Mathf.Clamp(transform.position.x, screenLeftLimit, screenRightLimit);
        float clampedY = Mathf.Clamp(transform.position.y, screenBottomLimit, screenTopLimit);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void HandleColorChange()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeColor(0); // Красный
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeColor(1); // Синий
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor(2); // Зелёный
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeColor(3); // Жёлтый
        }
    }

    void ChangeColor(int colorIndex)
    {
        currentColorIndex = colorIndex;
        UpdateColor();
    }

    void UpdateColor()
    {
        spriteRenderer.color = colors[currentColorIndex];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Color obstacleColor = other.GetComponent<SpriteRenderer>().color;
            if (obstacleColor != spriteRenderer.color)
            {
                Time.timeScale = 0; // Остановить время
                isGameOver = true;
            }
        }
    }
}