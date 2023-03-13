using UnityEngine;

public class RegenerateSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with this object
        if (other.CompareTag("Player"))
        {
            // Get the RegenerateScene script from the active scene's camera
            RegenerateScene regenerateScript = Camera.main.GetComponent<RegenerateScene>();

            // Trigger the RegenerateScene script to regenerate the solar system
            if (regenerateScript != null)
            {
                regenerateScript.Start();
            }
        }
    }
}
