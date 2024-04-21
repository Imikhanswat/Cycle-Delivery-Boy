using UnityEngine;

public class BicycleTireRotation : MonoBehaviour
{
    public Transform tireTransform; // Reference to the tire transform
    public Vector3 rotationDirection = Vector3.up; // Direction in which the tire should rotate
    public float rotationSpeed = 100f; // Speed of rotation



    void Update()
    {
        // Rotate the tire based on the provided direction and speed
        if(BicycleController.isMoving && !FinishPoint.GameFinished){

  float rotationAmount = rotationSpeed * Time.deltaTime;
        tireTransform.Rotate(rotationDirection, rotationAmount);
        }else{
float rotationAmount = 0 * Time.deltaTime;
        tireTransform.Rotate(rotationDirection, rotationAmount);

        }
      
    }
}
