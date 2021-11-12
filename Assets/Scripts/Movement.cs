using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // States
    public bool isLeaving;
    public bool isSocializing;
    public bool isWandering;

    private Vector2 direction;
    private Rigidbody2D mRigidbody2D;

    //private Zone currentZone;
    private GameObject currentCheckpoint;

    private void Start()
    {
        mRigidbody2D = transform.GetComponent<Rigidbody2D>();
        //currentCheckpoint = currentZone.checkpoint;
    }

    private void Update()
    {
        if (!isSocializing)
            if (isLeaving)
            {
                if (currentCheckpoint == null)
                    //currentCheckpoint = currentZone.checkpoint;

                if (currentCheckpoint != null)
                    transform.position = Vector2.MoveTowards(transform.position, currentCheckpoint.transform.position, 3 * Time.deltaTime);
                else
                    Destroy(gameObject);
            }
            else if (!isWandering)
                StartCoroutine(Wander());
    }

    private void FixedUpdate()
    {
        if (!isSocializing && !isLeaving && isWandering)
            mRigidbody2D.MovePosition(mRigidbody2D.position + (direction * 2 * Time.deltaTime));
    }

    public void SetCurrentCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    //public void SetCurrentZone(Zone zone)
    //{
    //    currentZone = zone;
    //}

    IEnumerator Wander()
    {
        int walkTime = Random.Range(1, 5);
        float x = (float)Random.Range(-100, 101) / 100;
        float y = (float)Random.Range(-100, 101) / 100;

        direction = new Vector2(x, y).normalized;

        isWandering = true;

        yield return new WaitForSeconds(walkTime);

        isWandering = false;
    }
}
