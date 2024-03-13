using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ArrowShooter;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    public int LevelNumber { get => levelNumber; set => levelNumber = value; }

    public GameObject[] levelGameObjects;
    public ArrowShooter arrowShooter;
    // Start is called before the first frame update
    void Start()
    {
        //GetLevels();
    }
    public void GetLevels()
    {
        for (int i = 0; i < levelGameObjects.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("LevelNumber"))
            {
                levelGameObjects[i].SetActive(true);
            }
            else
            {
                levelGameObjects[i].SetActive(false);
            }
        }
    }
    public void NextLevelButton()
    {
        LevelNumber++;
        PlayerPrefs.SetInt("LevelNumber", LevelNumber);
        GetLevels();
        
        arrowShooter.powerUpsEnum = (PowerUpsEnum)Random.Range(0, 1);
        arrowShooter.powerUpAction?.Invoke(arrowShooter.powerUpsEnum);
        GameManager.Instance.howmanyTimesPowerupTook = 1;
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }
}
