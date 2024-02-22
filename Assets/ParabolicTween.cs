using System.Collections;
using UnityEngine;

public class ParabolicTween : MonoBehaviour
{
    //public Transform startPoint;
    //public Vector3 endPoint;
    //public float duration = 1f;
    //public int segments = 10;
    //float yPos = -2.34f;
    //float zPos = -23.624f;
    //private void Start()
    //{
    //    startPoint = GetComponent<Transform>();
    //    endPoint = new Vector3(Random.Range(-5, 5), yPos,zPos);
    //    StartCoroutine(TweenParabola());
    //}

    //private IEnumerator TweenParabola()
    //{
    //    for (int i = 0; i <= segments; i++)
    //    {
    //        float t = i / (float)segments;
    //        Vector3 lerpedPoint = Vector3.Lerp(startPoint.position, endPoint, t);
    //        lerpedPoint.y += Mathf.Sin(Mathf.PI * t) * 3f; // Adjust the height of the parabola

    //        transform.position = lerpedPoint;
    //        yield return new WaitForSeconds(duration / segments);
    //    }
    //}
    //                                           2 
    //public Transform startPoint;
    //public Vector3 endPoint;
    //public float height = 2f;
    //public float duration = 1f;

    //private void Start()
    //{
    //    startPoint = GetComponent<Transform>();
    //    endPoint = new Vector3(Random.Range(-5, 5), -2.34f,-23.624f);
    //    Vector3[] path = new Vector3[3];
    //    path[0] = startPoint.position;
    //    path[1] = CalculateMidPoint(startPoint.position, endPoint, height);
    //    path[2] = endPoint;

    //    LeanTween.moveSpline(gameObject, path, duration).setEase(LeanTweenType.easeOutQuad);
    //}

    //private Vector3 CalculateMidPoint(Vector3 start, Vector3 end, float height)
    //{
    //    Vector3 midPoint = (start + end) / 2f;
    //    midPoint += Vector3.up * height;
    //    return midPoint;
    //}
    //                                               3
    public Transform startPoint;
    public Vector3 endPoint;
    public float height = 2f;
    public float duration = 2f;

    private float elapsedTime = 0f;

    private void Start()
    {
        startPoint = GetComponent<Transform>();
        endPoint = new Vector3(Random.Range(-5, 5), -2.34f, -25);
    }
    private void Update()
    {
        if (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 startPos = startPoint.position;
            Vector3 endPos = endPoint;
            Vector3 midPoint = new Vector3((startPos.x + endPos.x) / 2, startPos.y + height, (startPos.z + endPos.z) / 2);
            Vector3 currentPos = CalculateParabolicPoint(startPos, midPoint, endPos, t);
            transform.position = currentPos;
            elapsedTime += Time.deltaTime;
        }
        else
        {
            transform.position = endPoint;
        }
    }

    private Vector3 CalculateParabolicPoint(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        float mt = 1 - t;
        Vector3 result = mt * mt * start + 2 * mt * t * mid + t * t * end;
        return result;
    }
}
