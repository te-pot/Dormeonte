using UnityEngine;

public class EnemyApproaches : MonoBehaviour
{
    public int currentLayer = 1;
    public int maxLayer = 5;
    public float layerInterval = 3f;
    public Animator cameraAnimator;

    [Header("Floating Text Settings")]
    public GameObject floatingTimePrefab;
    //public Transform floatingTextParent;
    //public Vector2 floatingTextOffset = new Vector2(0f, 0f);



    private float timer;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private DamageFlash damageFlash; // Reference to the flash script
    private Vector2 floatingTextScreenPos = new Vector2(500f, -60f); // manually set in Canvas space


    public static EnemyApproaches Instance;

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
        if (damageFlash != null)
            damageFlash.Flash();

        AudioManager.Instance?.PlaySFX("ghostdamage");

        // 💬 Spawn floating text (-5)
        ShowFloatingText("-5");

        GameTimer gameTimer = FindFirstObjectByType<GameTimer>();
        if (gameTimer != null)
        {
            gameTimer.Timer -= 5f;
            gameTimer.Timer = Mathf.Max(gameTimer.Timer, 0f);
        }

        cameraAnimator.SetTrigger("hit");
        Destroy(gameObject);
    }




    private void ShowFloatingText(string text)
    {
        if (floatingTimePrefab == null)
        {
            Debug.LogWarning("⚠️ Floating text prefab not assigned.");
            return;
        }

        // Find any active Canvas in the scene
        Canvas mainCanvas = FindFirstObjectByType<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogWarning("⚠️ No Canvas found in scene — Floating text won't show.");
            return;
        }

        // Instantiate as a child of the Canvas
        GameObject floatingText = Instantiate(floatingTimePrefab, mainCanvas.transform);

        // Set anchored position manually (UI space)
        RectTransform floatingRect = floatingText.GetComponent<RectTransform>();
        if (floatingRect != null)
        {
            floatingRect.anchoredPosition = floatingTextScreenPos;
        }

        /*
        // ✅ Set the text content
        FloatingTime ftComponent = floatingText.GetComponent<FloatingTime>();
        if (ftComponent != null)
        {
            ftComponent.SetText(text);
        }
        */
    }
}
