using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "Enemy",menuName ="Enemies",order =1)]
public class Enemy_SO : ScriptableObject
{

    public string enemyName;
    public GameObject enemyModel;
    public float enemyHealth = 200f;
    public float enemyMana = 50f;
    public int attackPower= 5;

   
}
