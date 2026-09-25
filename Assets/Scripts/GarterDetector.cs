using UnityEngine;

public class GarterDetector : MonoBehaviour
{
    private bool playerPassedFromBelow = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        if (playerRb != null && playerRb.linearVelocity.y > 0f)
        {
            playerPassedFromBelow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!playerPassedFromBelow)
            return;

        if (other.transform.position.y > transform.position.y)
        {
            playerPassedFromBelow = false;

            Debug.Log("Player cleared the Garter!");

            GameManager.Instance.LevelCleared();
        }
    }

    public void ResetDetector()
    {
        playerPassedFromBelow = false;
    }
}