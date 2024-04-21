using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    public GameObject imagePrefab; // The prefab of the image you want to animate
    public Transform dollarTransform; // The transform of the dollar UI element
    public float animationDuration = 1f; // Duration of the animation in seconds
    public int NumberOfImges=10;
    public AudioSource audioSource; // Reference to the AudioSource component

    // Function to instantiate images as children of the script's GameObject and animate them towards the dollar UI element
    void Start(){

        AnimateImages(NumberOfImges);
    }
    public void AnimateImages(int numberOfImages)
    {
        StartCoroutine(SpawnAndAnimateImages(numberOfImages));
    }

    IEnumerator SpawnAndAnimateImages(int numberOfImages)
    {
        audioSource.Play();
        for (int i = 0; i < numberOfImages; i++)
        {
            GameObject newImage = Instantiate(imagePrefab, transform);
            StartCoroutine(MoveImageTowardsDollar(newImage.transform));
            yield return new WaitForSeconds(0.1f); // Delay between instantiating images
        }
    }

    IEnumerator MoveImageTowardsDollar(Transform imageTransform)
    {
        Vector3 startPosition = imageTransform.position;
        Vector3 endPosition = dollarTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            imageTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // When animation completes, you can add the cash to the earn value here
        // For example, if you have a Text component for earn value:
        // earnValueText.text = (int.Parse(earnValueText.text) + cashAmount).ToString();

        Destroy(imageTransform.gameObject); // Destroy the animated image
    }
}
