using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame.  Use transform position of Room to get the center of the new room
    void Update()
    {
        if (target != null)
        {
           // transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x -3, target.position.y -.07F , transform.position.z -10.0F), moveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(!bigMapActive)
            {
                ActivateBigMap();
            } else
            {
                DeactivateBigMap();
            }
        }

    }
    public void ChangeTarget(Transform newTarget)
        {
            target = newTarget;
        }

    public void ActivateBigMap()
    {
        bigMapActive = true;
        bigMapCamera.enabled = true;
        mainCamera.enabled = false;
        PlayerController.instance.canMove = false;
        Time.timeScale = 0.0F;
        UIController.instance.mapDisplay.SetActive(false);
        UIController.instance.bigMapText.SetActive(true);
    }

    public void DeactivateBigMap()
    {
        bigMapActive = false;
        bigMapCamera.enabled = false;
        mainCamera.enabled = true;
        PlayerController.instance.canMove = true;
        Time.timeScale = 1.0F;
        UIController.instance.mapDisplay.SetActive(true);
        UIController.instance.bigMapText.SetActive(false);
    }

    }

