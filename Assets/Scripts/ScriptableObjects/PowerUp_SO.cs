using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PowerUp",menuName ="Power UP",order =2)]
public class PowerUp_SO : ScriptableObject
{
    public string powerUpName;
    public GameObject powerUp;
    public int powerUpValue;
  
    //public AudioSource powerUpSound;
}
