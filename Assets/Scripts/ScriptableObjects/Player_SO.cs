using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerStats",menuName ="PlayerStats",order =3)]
public class Player_SO : ScriptableObject
{
    public int BaseHealth = 500;

    public int Health;
    
    public int mana;
    
}
