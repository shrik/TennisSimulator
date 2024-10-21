using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Tennis ball prefab (drag and drop your tennis ball prefab in Unity)
    public GameObject tennisPrefab;
    
    // Force applied to the tennis ball to simulate a kick
    public Vector3 kickForce = new Vector3(-0.5f, 0.26f, 0.1f);

    bool useManualKick = false;
    
    // Reference to the current tennis ball instance
    private GameObject currentTennisBall;

    // Rigidbody component of the current tennis ball
    private Rigidbody currentRigidbody;

    

    void Start()
    {
        // Start the loop to generate and kick the tennis ball
        StartCoroutine(GenerateAndKickTennisBall());
    }

    IEnumerator GenerateAndKickTennisBall()
    {
        while (true)
        {
            if (!useManualKick)
            {
                float forceRange = Random.Range(3f, 4f);
                // Randomize the kick force
                float xForce = Random.Range(-0.1f, -0.5f);
                float zForce = Random.Range(-0.1f, 0.1f);

                kickForce = new Vector3(xForce * forceRange, 0.26f, zForce * forceRange);
            }

            // Generate a new tennis ball
            currentTennisBall = Instantiate(tennisPrefab, new Vector3(11, 1, 0), Quaternion.identity);

            // Add the TennisBall script to the instantiated ball
            TennisBall tennisBallScript = currentTennisBall.AddComponent<TennisBall>();

            SphereCollider currentCollider = currentTennisBall.GetComponent<SphereCollider>();
            // currentCollider.isTrigger = true;
            
            currentRigidbody = currentTennisBall.GetComponent<Rigidbody>();

            // Apply the kick force to the tennis ball
            currentRigidbody.AddForce(kickForce, ForceMode.Impulse);

            yield return new WaitForSeconds(0.2f);
            
            // Wait until the ball becomes nearly static
            yield return new WaitUntil(() => 
                currentRigidbody.linearVelocity.magnitude < 0.4f
                || currentRigidbody.position.y < -0.2f
                || Mathf.Abs(currentRigidbody.position.x) > 18f
                || Mathf.Abs(currentRigidbody.position.z) > 10f
            );

            // Debug.Log("Ball Velocity: " + currentRigidbody.linearVelocity.magnitude);
            // Destroy the tennis ball when it becomes static
            Destroy(currentTennisBall);

            // Wait a moment before generating a new tennis ball
            yield return new WaitForSeconds(0.2f);
        }
    }
}
