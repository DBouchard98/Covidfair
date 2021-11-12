using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : Entity
{
    [SerializeField] private float infectionTimePenalty;
    private bool lostPoint;

    private void Update()
    {
        if (isInfected)
            exposureTime += Time.deltaTime;

        checkExposure();

        if (exposureTime > infectionTime + infectionTimePenalty && !lostPoint)
        {
            lostPoint = true;
            GameManager.currentScore--;
        }
    }

    public void Disinfect()
    {
        if (isInfected)
        {
            GameManager.currentScore += 2;
            Destroy(infectionZone);
            exposedToInfection = false;
            isInfected = false;
            lostPoint = false;
            exposureTime = 0;
        }
    }
}
