using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public enum PowerUpType
{
    None,
    Pushback,
    Rockets
}
