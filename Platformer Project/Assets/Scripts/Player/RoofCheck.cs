using UnityEngine;

public class RoofCheck : MonoBehaviour
{
    public CustomRigidbody pRb;

    private bool roofed;

    private void Update()
    {
        if (pRb.RoofUp && !roofed)
        {
            pRb.Velocity = new Vector2(pRb.Velocity.x, 0.0f);
            roofed = true;
        }
        if (!pRb.RoofUp)
        {
            roofed = false;
        }
    }
}
