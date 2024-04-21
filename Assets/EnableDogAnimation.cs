using UnityEngine;

public class EnableDogAnimation : MonoBehaviour
{
   
    public Animation dogAnimation;

    private void Start()
    {
        

        // Ensure the Animation component is stopped initially
        if (dogAnimation != null)
        {
            dogAnimation.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            // Play the animation
            if (dogAnimation != null)
            {
                dogAnimation.Play();
            }
        }
    }
}
