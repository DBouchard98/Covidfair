using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float infectionTime;           /* Time needed to become infected */
    public float infectionZoneRadius;     /* Radius of infection zone */

    protected static GameManager gameManager;

    protected GameObject infectionZone;             /* Infection zone GameObject */
    public bool isInfected;                         /* Is Entity infected */
    protected bool exposedToInfection;              /* Is Entity currently exposed */
    protected float exposureTime;                   /* Amount of time exposed to virus */

    private void Start()
    {
        if (gameManager == null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void setExposed(bool value)
    {
        exposedToInfection = value;
    }

    protected void checkExposure()
    {
        if (isInfected)
            return;

        if (exposedToInfection)
            exposureTime += Time.deltaTime;

        if (exposureTime >= infectionTime)
            infect();
    }

    public void infect()
    {
        isInfected = true;
        GameManager.currentScore--;
        infectionZone = Instantiate(gameManager.infectionZonePrefab, transform.position, transform.rotation, transform);
        infectionZone.transform.localScale = infectionZoneRadius * Vector2.one;
    }

    public bool checkInfection()
    {
        return isInfected;
    }

    public void setInfection(bool status)
    {
        isInfected = status;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Entity") && GetComponentInChildren<Entity>().isInfected && !collision.gameObject.GetComponentInChildren<Entity>().isInfected)
            collision.gameObject.GetComponentInChildren<Entity>().infect();
    }
}
