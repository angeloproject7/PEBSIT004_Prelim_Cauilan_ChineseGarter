using UnityEngine;

public class GarterClearZone : MonoBehaviour
{
    public float raiseAmount = 0.5f;
    private bool cleared = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cleared)
        {
            cleared = true;

            Transform garter = transform.parent;

            Vector3 newPosition = garter.position;
            newPosition.y += raiseAmount;
            garter.position = newPosition;

            Debug.Log("Successful clear! Garter raised!");
        }
    }
}