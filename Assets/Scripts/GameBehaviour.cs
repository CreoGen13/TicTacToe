using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject crossPrefab;
    public Transform spawnPoint;
    public Material glowingMaterial;

    private GameObject[,] details = new GameObject[3, 3];
    private float spawnStep = 1;
    public void SpawnDetails(bool [,] tableBool)
    {
        DestroyDetails();
        for (int i = 0; i < details.GetLength(0); i++)
            for (int j = 0; j < details.GetLength(0); j++)
            {
                details[i, j] = tableBool[i, j] ? Instantiate(circlePrefab, spawnPoint) : Instantiate(crossPrefab, spawnPoint);
                ChangePosition(i*10 + j, details[i, j]);
            }

        void ChangePosition(int position, GameObject detail)
        {
            switch (position)
            {
                case 00:
                    detail.transform.localPosition = new Vector3(-spawnStep, spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 01:
                    detail.transform.localPosition = new Vector3(0, spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 02:
                    detail.transform.localPosition = new Vector3(spawnStep, spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 10:
                    detail.transform.localPosition = new Vector3(-spawnStep, 0, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 11:
                    detail.transform.localPosition = new Vector3(0, 0, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 12:
                    detail.transform.localPosition = new Vector3(spawnStep, 0, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 20:
                    detail.transform.localPosition = new Vector3(-spawnStep, -spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 21:
                    detail.transform.localPosition = new Vector3(0, -spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
                case 22:
                    detail.transform.localPosition = new Vector3(spawnStep, -spawnStep, 0);
                    detail.transform.rotation = spawnPoint.rotation;
                    break;
            }
        }
    }
    public void DestroyDetails()
    {
        if (details[0, 0] != null)
            foreach (GameObject detail in details)
            {
                detail.GetComponent<BoxCollider>().enabled = false;
                Destroy(detail.gameObject, 1f);
            }
    }
    public void ChangeMaterial(int x, int y, int directionX, int directionY)
    {
        for(int i = 0; i < 3; i++)
            details[x + directionX * i, y + directionY * i].GetComponent<MeshRenderer>().material = glowingMaterial;
    }
}
