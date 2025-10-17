using UnityEngine;

public class EnemyApproaches : MonoBehaviour
{
    public int currentLayer = 1;
    public int maxLayer = 5;
    public float layerInterval = 3f;

    private float timer;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private DamageFlash damageFlash; // Reference to the flash script

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        UpdateVisualLayer();

        // Find the VFX parent with CanvasGroup
        GameObject vfxObj = GameObject.FindWithTag("VFX");
        if (vfxObj != null)
            damageFlash = vfxObj.GetComponent<DamageFlash>();
        else
            Debug.LogWarning("VFX object not found or inactive in hierarchy!");
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
        UpdateVisualLayer();

        if (currentLayer >= maxLayer)
        {
            OnReachPlayer();
        }
    }

    void UpdateVisualLayer()
    {
        spriteRenderer.sortingOrder = currentLayer;
    }

    void OnReachPlayer()
    {
        // Trigger the flash
        if (damageFlash != null)
            damageFlash.Flash();

        // Play SFX3 immediately
        AudioManager.Instance?.PlaySFX3();

        // Deduct 1 second from game timer
        GameTimer gameTimer = FindFirstObjectByType<GameTimer>();
        if (gameTimer != null)
        {
            gameTimer.Timer -= 1f;
            gameTimer.Timer = Mathf.Max(gameTimer.Timer, 0f);
        }

        Destroy(gameObject);
    }

}
