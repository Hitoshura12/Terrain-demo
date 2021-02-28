using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public PowerUp_SO powerUp;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(PickUp(other));

        }
    }

    IEnumerator PickUp(Collider player)
    {
        
        player.GetComponent<PlayerController>().playerStats.Health += powerUp.powerUpValue;
        MeshRenderer[] childMesh = GetComponentsInChildren<MeshRenderer>();
        Collider[] childColliders = GetComponentsInChildren<Collider>();
        foreach (var baseMesh in childMesh)
        {
            baseMesh.enabled = false;
            foreach (var childcol in childColliders)
            {
                childcol.enabled = false;
            }
        }
      

        player.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        SkinnedMeshRenderer[] mesh = player.GetComponentsInChildren<SkinnedMeshRenderer>();
       
        foreach ( var item in mesh)
        {
           
                if (item.name == "shirts_sode")
                {
                    Color currentMaterial = item.material.color;
                    //  Color nextMaterial = new Color(114f, 245f, 1f, 245f);
                    Color nextMaterial = Color.green;

                    float time = 3f;
                    float inversedTime = 1 / time;
                    for (float step = 0; step < 1.0; step += Time.deltaTime * inversedTime)
                    {
                        item.material.SetColor("_Color", Color.LerpUnclamped(currentMaterial, nextMaterial, step));
                    
                        yield return null;
                    }
                    yield return new WaitForSeconds(3f);
                    item.material.color = currentMaterial;
                    // RGBA(57.500, 123.000, 1.000, 123.000
                    Debug.Log(currentMaterial);
                    Debug.Log(nextMaterial);
                    Debug.Log(item.material.color);
                    player.gameObject.transform.localScale = Vector3.one;
                Destroy(gameObject);
            }
            
            
        }
        
        
       
    }

}
