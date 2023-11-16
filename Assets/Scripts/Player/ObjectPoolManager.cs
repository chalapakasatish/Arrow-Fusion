using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject arrowPrefab;
    public int poolSize = 10;

    public List<GameObject> arrowPool;

    void Start()
    {
        InitializeObjectPool();
    }

    void InitializeObjectPool()
    {
        arrowPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    public GameObject GetArrowFromPool(Vector3 position, Quaternion rotation)
    {
        foreach (var arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.transform.position = position;
                arrow.transform.rotation = rotation;
                arrow.SetActive(true);
                return arrow;
            }
        }

        // If no inactive arrows are found, expand the pool
        GameObject newArrow = Instantiate(arrowPrefab);
        newArrow.transform.position = position;
        newArrow.transform.rotation = rotation;
        arrowPool.Add(newArrow);
        return newArrow;
    }
    public void ReturnArrowToPool(GameObject arrow)
    {
        arrow.SetActive(false);
    }
    
}
