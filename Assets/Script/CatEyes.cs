using UnityEngine;

public class CatEyes : MonoBehaviour
{
    public GameObject rightEye;
    public GameObject leftEye;

    private Vector3 mousePosition;

    void Start()
    {
        rightEye.SetActive(false);
        leftEye.SetActive(false);
    }

    void Update()
    {
        // ✅ Update the current mouse position every frame
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Optionally ignore Z depth (for 2D)
        mousePosition.z = 0f;

        if (mousePosition.x >= -2.3f && mousePosition.x <= 2.3f)
        {
            rightEye.SetActive(false);
            leftEye.SetActive(false);
        }
        else if (mousePosition.x < -2.3f)
        {
            rightEye.SetActive(false);
            leftEye.SetActive(true);
        }
        else if (mousePosition.x > 2.3f)
        {
            rightEye.SetActive(true);
            leftEye.SetActive(false);
        }
    }
}
