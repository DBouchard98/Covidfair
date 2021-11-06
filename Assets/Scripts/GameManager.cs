using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Camera mainCamera;
    private Camera secondaryCamera;

    private void Start()
    {
        mainCamera = player.transform.Find("1st Person").GetComponent<Camera>();
        secondaryCamera = player.transform.Find("3rd Person").GetComponent<Camera>();
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
