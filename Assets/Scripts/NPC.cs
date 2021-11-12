using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    private int minActiveTime = 5;      /* Min time (in sec) the NPC will be in the level */
    private int maxActiveTime = 10;     /* Max time (in sec) the NPC will be in the level */

    public bool isSusceptible;          /* Is the NPC susceptible */
    public bool hasMask;                /* Does NPC have a mask */
    public bool isVaccinated;           /* Is the NPC vaccinated */

    public float chanceOfDeath = 0.5f;

    public float timeEntered;           /* Amount of seconds between the start of the game and when the NPC spawned */
    public float timeAvailable;         /* Amount of time the NPC can stay in the level */
    public float currentTime;           /* Amount of seconds between the start of the game and now */

    public bool hasSocialized;

    public Movement movement;

    private void Awake()
    {
        movement = gameObject.GetComponent<Movement>();
    }

    private void Start()
    {
        timeEntered = Time.realtimeSinceStartup;
        timeAvailable = Random.Range(minActiveTime, maxActiveTime + 1);
    }

    private void Update()
    {
        currentTime = Time.realtimeSinceStartup;

        if (currentTime - timeEntered > timeAvailable)
            movement.isLeaving = true;

        checkExposure();
    }

    public void Socialize()
    {
        if (!hasSocialized)
        {
            hasSocialized = true;
            movement.isSocializing = true;
        }
    }

    public void hitWith(string projectile)
    {
        switch (projectile)
        {
            case "Vaccine":
                GiveVaccine();
                break;

            case "Mask":
                if (!hasMask)
                    Instantiate(Entity.gameManager.multiMaskPrefab, transform.position, transform.rotation);
                break;

            case "Ticket":
                GiveTicket();
                break;

            case "Firecracker":
                Instantiate(Entity.gameManager.explosionPrefab, transform.position, transform.rotation);
                break;

            default:
                break;
        }
    }

    public bool Scare(bool ignorePoints = false)
    {
        if (movement.isSocializing)
        {
            movement.isSocializing = false;
            return true;
        }

        return false;
    }

    private bool GiveTicket(bool ignorePoints = false)
    {
        if (!movement.isLeaving)
        {
            if (!ignorePoints)
                if (isInfected)
                    GameManager.currentScore++;
                else
                    GameManager.currentScore--;

            movement.isLeaving = true;
            return true;
        }

        return false;
    }

    public bool GiveInfection(bool ignorePoints = false)
    {
        if (!isInfected)
        {
            isInfected = true;
            if (isSusceptible)
            {
                float chance = Random.Range(0, 101);
                if (chance >= chanceOfDeath * 100)
                {
                    GameManager.currentScore -= (ignorePoints ? 0 : 1);
                    Destroy(gameObject);
                }
            }
            GameManager.currentScore -= (ignorePoints ? 0 : 1);
            infectionZone = Instantiate(gameManager.infectionZonePrefab, transform.position, transform.rotation, transform);
            infectionZone.transform.localScale = infectionZoneRadius * Vector2.one;
            return true;
        }

        return false;
    }

    public bool MakeSusceptible(bool ignorePoints = false)
    {
        if (!isSusceptible)
        {
            isSusceptible = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            return true;
        }

        return false;
    }

    public bool GiveMask(bool ignorePoints = false)
    {
        if (!hasMask)
        {
            hasMask = true;
            infectionZoneRadius = 4;
            if (isInfected)
                infectionZone.transform.localScale = infectionZoneRadius * Vector2.one;
            GameManager.currentScore += (ignorePoints ? 0 : 1);
            transform.Find("Mask").gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    public bool GiveVaccine(bool ignorePoints = false)
    {
        if (!isVaccinated)
        {
            isSusceptible = false;
            infectionTime = 5;
            isVaccinated = true;
            GameManager.currentScore += (ignorePoints ? 0 : 1);
            GetComponent<SpriteRenderer>().color = Color.gray;
            return true;
        }

        return false;
    }
}
