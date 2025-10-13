using UnityEngine;

public class EnemyApproaches : MonoBehaviour
{
    public int currentLayer = 1;
    public int maxLayer = 5;
    public float layerInterval = 3f; // seconds between moves

    private GameTimer gameTimer;
    private float timer;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        UpdateVisualLayer();

        // Find the GameTimer object in the scene
        gameTimer = FindFirstObjectByType<GameTimer>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= layerInterval)
        {
            MoveCloser();
            timer = 0f;
        }
    }

    void MoveCloser()
    {
        currentLayer++;


        // 🔹 Randomize horizontal position within camera view
        /*ector3 newPos = transform.position;
        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        newPos.x = Random.Range(-halfWidth + 0.5f, halfWidth - 0.5f); // 0.5f buffer so it doesn’t go off-screen
        transform.position = newPos;*/

        // Update visual depth
        UpdateVisualLayer();

        if (currentLayer >= maxLayer)
        {
            OnReachPlayer();
        }
    }

    void UpdateVisualLayer()
    {
        // Higher sortingOrder = drawn on top
        spriteRenderer.sortingOrder = currentLayer;
    }

    void OnReachPlayer()
    {
        // Deduct 1 second from the timer
        if (gameTimer != null)
        {
            gameTimer.Timer -= 1f;
            gameTimer.Timer = Mathf.Max(gameTimer.Timer, 0f);
        }

        Destroy(gameObject);
    }

}
