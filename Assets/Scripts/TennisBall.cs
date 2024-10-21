using UnityEngine;

public class TennisBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // call super collision function first 
        // base.OnCollisionEnter(collision);
        // record time of collision, and frame count , time should be in milliseconds, position should be in centimeters    
        int collisionTime = (int)(Time.time * 1000);
        Vector3 collisionPosition = transform.position * 100;
        Debug.Log("{\"time\": " + collisionTime + ", \"position\": " + collisionPosition + ", \"collision\": " + collision.gameObject.name + "}");
        // If you want to check for specific collisions (e.g., with the ground):
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //     Debug.Log("Ball hit the ground at position: " + transform.position);
        // }
    }
}