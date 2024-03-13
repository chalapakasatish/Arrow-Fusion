using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject[] arrowPrefab;
    public GameObject fireArrowPrefab;
    public int poolSize = 2;

    public List<GameObject> arrowPool = new List<GameObject>();
    public List<GameObject> fireArrowPool = new List<GameObject>();

    void Start()
    {
        InitializeObjectPool();
        InitializeObjectPoolFireArrow();
    }

    void InitializeObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab[i]);
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
        GameObject newArrow = Instantiate(arrowPrefab[Random.Range(0,arrowPrefab.Length)]);
        newArrow.transform.position = position;
        newArrow.transform.rotation = rotation;
        arrowPool.Add(newArrow);
        return newArrow;
    }
    public void ReturnArrowToPool(GameObject arrow)
    {
        arrow.SetActive(false);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(transform.forward * 15, ForceMode.Impulse);
    }

    void InitializeObjectPoolFireArrow()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(fireArrowPrefab);
            arrow.SetActive(false);
            fireArrowPool.Add(arrow);
        }
    }

    public GameObject GetArrowFromPoolFireArrow(Vector3 position, Quaternion rotation)
    {
        foreach (var arrow in fireArrowPool)
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
        GameObject newArrow = Instantiate(fireArrowPrefab);
        newArrow.transform.position = position;
        newArrow.transform.rotation = rotation;
        fireArrowPool.Add(newArrow);
        return newArrow;
    }
    public void ReturnArrowToPoolFireArrow(GameObject arrow)
    {
        arrow.SetActive(false);
    }
}
