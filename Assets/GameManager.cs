using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ArrowShooter arrowShooterScript;
    private void Awake()
    {
        Instance = this;
    }
}
