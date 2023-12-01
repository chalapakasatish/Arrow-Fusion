using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Coords of next target;
    /// </summary>
    public Vector3 NextNode;

    /// <summary>
    /// ID of ball. -1 if bonus.
    /// </summary>
    public int BallId { get; private set; }
    public int Count { get => count; set => count = value; }

    /// <summary>
    /// Path controller.
    /// </summary>
    public BezierPathController PathController;

    public bool MustBeDestroyed = true;
    int count;
    public TMP_Text[] countText;
    private void Start()
    {
        count = UnityEngine.Random.Range(2, 6);
        for (int i = 0; i < countText.Length; i++)
        {
            countText[i].text = Count.ToString();
        }
    }
    //private void Update()
    //{
    //    transform.localEulerAngles = Vector3.forward * 50f * Time.deltaTime;
    //}
    public void SetBallId(int id)
    {
        BallId = id;
        Debug.Log(BallId);
       transform.GetChild(0).GetComponent<MeshRenderer>().material = (id == -1) ? GameObject.FindGameObjectWithTag("BallsFactory").GetComponent<BallsFactory>().BonusMaterial : GameObject.FindGameObjectWithTag("BallsFactory").GetComponent<BallsFactory>().AvailableMaterials[id];
    }

    private IEnumerator SelfDestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (MustBeDestroyed) Destroy(gameObject);
    }

    /// <summary>
    /// Interaction between objects.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {

            Destroy(other.gameObject);
            ObjectPoolManager.Instance.arrowPool.Clear();
            //ObjectPoolManager.Instance.ReturnArrowToPool(other.gameObject);
            //other.transform.position = Vector3.zero;
            //other.transform.rotation = Quaternion.identity;
            Count--;
            for (int i = 0; i < countText.Length; i++)
            {
                countText[i].text = Count.ToString();
            }
            if (Count <= 0)
            {
                GameManager.Instance.arrowShooterScript.isShooting = false;
                for (int i = 0; i < countText.Length; i++)
                {
                    countText[i].text = Count.ToString();
                }
                //PathController.Correction();
                //PathController.DestroyBall(this);
                //if (PathController.BallSequence.Contains(this))
                //{
                //    //PathController.StopSequence();
                //    PathController.StartCoroutine(PathController.DelayedCheckEqualBalls(this,0f));
                //}
                if (Count <= 0)
                {
                    //PathController.DestroyBall(this);
                    PathController.InsertBallInSequence(this, this);
                    //PathController.StartCoroutine(PathController.DelayedCheckEqualBalls(this, 0f));
                }
            }
        }
        if (other.gameObject.tag == "Ball" && other.gameObject.GetComponent<Ball>().BallId == BallId)
        {
            GameManager.Instance.arrowShooterScript.isShooting = false;
            Debug.Log("Both Balls hit");
            PathController.InsertBallInSequence(this, other.GetComponent<Ball>());
            //MustBeDestroyed = true;
            //PathController.DestroyBall(this);
            //PathController.StartCoroutine(PathController.DelayedCheckEqualBalls(this, 0f));
        }

        //GetComponent<AnimationPlayer>().Play(AnimationThrowType.OnCollide);
        //if (PathController.BallSequence.Contains(this)) return;
        //if (other.gameObject.tag == "BallsTrap") return;

        //StopAllCoroutines();
        //if (other.gameObject.tag == "Ball")
        //{
        //    PathController.InsertBallInSequence(this, other.GetComponent<Ball>());
        //    MustBeDestroyed = false;
        //    //PathController.StartCoroutine(PathController.DelayedCheckEqualBalls(this, 0f));

        //    //PathController.DestroyBall(this);
        //}
    }

    private IEnumerator ShootCoroutine(Vector3 target, float time)
    {
        var passedTime = 0f;
        var startPosition = transform.position;
        while (passedTime < time)
        {
            passedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target, passedTime / time);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Shoot(Vector3 target, float time)
    {
        StartCoroutine(ShootCoroutine(target, time));
        StartCoroutine(SelfDestroyCoroutine(time));
    }

}
