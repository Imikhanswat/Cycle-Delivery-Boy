using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
 
   public Transform target;
   
   public  Vector3 offset;

   void Start(){
       // transform.position = target.position;

    offset=target.position-transform.position;

   }

   private void FixedUpdate(){

    transform.position=target.position-offset;
   }
}
