using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ArrowShooter arrowShooterScript;
    public UIManager uiManager;
    public int howmanyTimesPowerupTook;
    private void Awake()
    {
        Instance = this;
    }
}
