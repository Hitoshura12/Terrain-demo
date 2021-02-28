using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public PlayerController targetToFollow;
    public Vector3 offset;
    private float offsetZoom = 2.5f;
    float smoothSpeed = 0.125f;
    Vector3 desiredPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
      //  cam.fieldOfView = 80.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SmoothFollowPlayer();
        if (Input.GetKeyDown(KeyCode.R))
        {
            desiredPosition = targetToFollow.transform.position + offset;

        }
    }

    public void SmoothFollowPlayer()
    {

        Vector3 targetVelocity = targetToFollow.playerAgent.desiredVelocity;
         desiredPosition = targetToFollow.transform.position - offset ; 
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref targetVelocity, smoothSpeed * Time.smoothDeltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(targetToFollow.transform, targetToFollow.transform.up);
    }
}
