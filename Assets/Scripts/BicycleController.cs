using UnityEngine;
using System.Collections;

public class BicycleController : MonoBehaviour
{
    public float speed = 10f;
    public WayPoints[] levelWaypoints; // Array to hold waypoints for each level
    private Transform currentWayPoint;
    public float distanceThreshold = 0.1f;
    private bool isInputActive = false;
    public static bool isMoving = false;
    private Quaternion initialHandleRotation;
    public Animator Padel, Cycling;
    public GameObject MoneyTray, MoneyHit;
    public GameObject FailedPanel;
    private WayPoints waypoints; // Store the waypoints for the current level
    public Transform handle;


    public static bool IfAnyPanelActive=false;
    
    void Start()
    {
        initialHandleRotation = handle.localRotation;
        // Assuming you have waypoints for different levels named "WayPoints_Level1", "WayPoints_Level2", etc.
        int levelIndex = GetCurrentLevelIndex(); // Implement this method to get the current level index
        waypoints = levelWaypoints[levelIndex-1]; // Assign the waypoints for the current level
        currentWayPoint = waypoints.GetNextWayPoint(null);
        transform.position = currentWayPoint.position;
        FailedPanel.SetActive(false);
    }

    void Update()
    {
        
        if (isInputActive)
        {
            if (Vector3.Distance(transform.position, currentWayPoint.position) < distanceThreshold)
            {
                currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);

                waypoints.UpdateFillAmount(currentWayPoint);
            }

            RotateTowardsWaypoint();

           
           
        }
        else
        {
           
          
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                UpdateInput(true);
                if(!FinishPoint.GameFinished){

                Padel.SetBool("Padel",true);

                Cycling.SetBool("Cycling",true);
                }
                
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                UpdateInput(false);
                Padel.SetBool("Padel",false);
                 Cycling.SetBool("Cycling",false);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            UpdateInput(true);
            if(!FinishPoint.GameFinished){

                Padel.SetBool("Padel",true);

                Cycling.SetBool("Cycling",true);
                }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            UpdateInput(false);
            Padel.SetBool("Padel",false);
             Cycling.SetBool("Cycling",false);
        }
    }

    void FixedUpdate()
    {
        if (isInputActive && !FinishPoint.GameFinished)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        Vector3 targetDirection = (currentWayPoint.position - transform.position).normalized;
        transform.position += targetDirection * speed * Time.fixedDeltaTime;
    }

   
    void RotateTowardsWaypoint()
    {
        Vector3 targetDirection = (currentWayPoint.position - transform.position).normalized;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 80f * Time.deltaTime);

            // Calculate the target handle rotation on the z-axis only
            // Quaternion targetHandleRotation = initialHandleRotation * Quaternion.Euler(0f, 0f, Quaternion.Angle(transform.rotation, targetRotation) * 1f);

            // Apply the target handle rotation
            // handle.localRotation = targetHandleRotation;
            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

            if (handle != null)
            {
                // Calculate the target handle rotation on the z-axis only
                Quaternion targetHandleRotation = initialHandleRotation * Quaternion.Euler(0f, angleDifference * 1f,0f);

                // Apply the target handle rotation
                handle.localRotation = targetHandleRotation;

                // Reset handle to its original rotation
                handle.localRotation = initialHandleRotation;
            }
        }
    }

   public void UpdateInput(bool isActive)
    {
        if (IfAnyPanelActive)
        {
            isInputActive = false;
            isMoving = false;
           
        }
        else {
            isInputActive = isActive;
            isMoving = isActive;
        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollidingObject"))
        {
            // Disable SmoothMovement script on all game objects with the tag "CollidingObject"
            GameObject[] collidingObjects = GameObject.FindGameObjectsWithTag("CollidingObject");
            foreach (GameObject obj in collidingObjects)
            {
                SmoothMovement smoothMovement = obj.GetComponent<SmoothMovement>();
                if (smoothMovement != null)
                {
                    smoothMovement.enabled = false;
                }
            }

            // Rest of your code...
            MoneyTray.SetActive(false);
           
           
            this.gameObject.GetComponent<BicycleController>().enabled = false;
            UpdateInput(false);
           Padel.Update(0f);
            Padel.SetFloat("Speed", 10f);
            Cycling.SetFloat("Speed", 10f);

            Cycling.Update(0f);
            

            Padel.SetBool("Padel", false);
            Cycling.SetBool("Cycling", false);


            Padel.SetBool("CycleDie", true);
            Cycling.SetBool("CharacterDie", true);
            MoneyHit.SetActive(true);
            Invoke("showFailedPanel", 2f);
        }
    }
    public void showFailedPanel() {

        FailedPanel.SetActive(true);
    }


    private int GetCurrentLevelIndex()
    {
       // PlayerPrefs.SetInt("LevelNumber", 5); ///////remove after testing levl 2
        int level = PlayerPrefs.GetInt("LevelNumber", 1);
        return level;
    }
}
