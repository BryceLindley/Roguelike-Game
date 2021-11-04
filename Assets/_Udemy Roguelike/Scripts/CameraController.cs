using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x -3, target.position.y -.07F , transform.position.z -10.0F), moveSpeed * Time.deltaTime);
        }

    }
        public void ChangeTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }

