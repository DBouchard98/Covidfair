using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player Attributes
    public static int currentScore;
    private static int currentLevel;
    private static float remainingLevelTime;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject portalPrefab;

    // Cameras
    private Camera mainCamera;
    private Camera secondaryCamera;

    // Prefabs
    public GameObject npcPrefab;
    public GameObject infectionZonePrefab;
    public GameObject multiMaskPrefab;
    public GameObject explosionPrefab;

    private void Start()
    {
        mainCamera = player.transform.Find("1st Person").GetComponent<Camera>();
        secondaryCamera = player.transform.Find("3rd Person").GetComponent<Camera>();

        for (int i = 0; i < 5; i++)
            Instantiate(portalPrefab, transform.position + new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41)), transform.rotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Rect tempRect = mainCamera.rect;
            float tempDepth = mainCamera.depth;

            mainCamera.rect = secondaryCamera.rect;
            mainCamera.depth = secondaryCamera.depth;

            secondaryCamera.rect = tempRect;
            secondaryCamera.depth = tempDepth;
        }

    }
}
