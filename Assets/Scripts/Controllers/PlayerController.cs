using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour, ITakeDamage
{
    #region Variables and properties
    [HideInInspector]
    public  NavMeshAgent playerAgent;
    Rigidbody playerRigidbody;
    Camera mainCamera;
    Animator playerAnim;
  
    bool isGrounded;
    Ray ray;

    public Player_SO playerStats;

   // public delegate void HitAction();
    public static event Action<int> OnPlayerHit;
    #endregion

    #region Startup
    private void Start()
    {
        mainCamera = Camera.main;

        playerAgent = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

    }
    #endregion

    #region Update Control
    private void Update()
    {
       
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(1) && !playerAgent.isStopped)
        {
           
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
               
                MoveToDestination(hitInfo.point);
                
               // Debug.DrawRay(playerAgent.transform.position, ray.direction, Color.red, Mathf.Infinity) ;

            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
           
            
            if (playerAgent.enabled)
            {
                
                // disable the agent
                playerAgent.updatePosition = false;
                playerAgent.updateRotation = false;
                playerAgent.isStopped = true;
   
                
            }

            playerRigidbody.isKinematic = false;
            playerRigidbody.useGravity = true;
            playerAnim.SetBool("Jump", true);
            playerRigidbody.AddForce(new Vector3(0f, playerAnim.GetFloat("JumpHeight"), 0f), ForceMode.Impulse);
        }
       else if (playerAgent.transform.position == playerAgent.destination)
        {
            isGrounded = true;

            playerAnim.SetFloat("Speed", 0f);
            playerAnim.SetBool("Jump", false);

        }

    }
    #endregion

    public void MoveToDestination(Vector3 point)
    {

        playerAgent.destination = point;
        playerAnim.SetFloat("Speed", 1f);
    }

    #region Collision Detector
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider!=null && collision.collider.CompareTag("Ground"))
        {
            
            if (!isGrounded)
            {
               
                if (playerAgent.enabled)
                {
                   // Debug.Log("NOT JUMPING AND AGENT ISSTOPPED: " + playerAgent.isStopped);
                
                    playerAgent.isStopped = false;
                    playerAgent.updatePosition = true;
                    playerAgent.updateRotation = true;
                    playerAnim.SetBool("Jump", false);
                }
     
                playerRigidbody.isKinematic = true;
                playerRigidbody.useGravity = false;
                isGrounded = true;
                playerAnim.SetBool("Jump", false);
            }
           
           
            
        }
    }


    #endregion

    #region Attack Impl
    public void TakeDamage(int dmgTaken)
    {
        if (OnPlayerHit != null)
        {
            OnPlayerHit(dmgTaken);
        }
    }
    #endregion

}
