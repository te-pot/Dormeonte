using UnityEngine;

public class ArrowSwing : MonoBehaviour
{
    public float swingSpeed = 60f;   // degrees per second
    public float maxAngle = 45f;     // max swing from center
    private float angle = 0f;
    private bool swingingRight = true;

    void Update()
    {
        if (swingingRight)
        {
            angle += swingSpeed * Time.deltaTime;
            if (angle >= maxAngle)
            {
                angle = maxAngle;
                swingingRight = false;
            }
        }
        else
        {
            angle -= swingSpeed * Time.deltaTime;
            if (angle <= -maxAngle)
            {
                angle = -maxAngle;
                swingingRight = true;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
