using System.Collections;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject CirclePrefab;
    public GameObject CrossPrefab;

    public static int numberOfDetails = 16;

    private Vector3 spawnBounds;
    private GameObject[] details = new GameObject[numberOfDetails];

    void Start()
    {
        StartMenu();
    }
    public void ExitMenu()
    {
        foreach(GameObject detail in details)
        {
            detail.GetComponent<DetailController>().Death();
        }
    }
    public void StartMenu()
    {
        spawnBounds = GetComponent<BoxCollider>().size / 2;

        for (int i = 0; i < numberOfDetails; i++)
        {
            float x = i >= 5 ? spawnBounds.x : -spawnBounds.x;
            GameObject detail = Random.Range(0f, 1f) > 0.5f ? CirclePrefab : CrossPrefab;
            Vector3 startPosition = new Vector3(x, Random.Range(-spawnBounds.y, spawnBounds.y), Random.Range(-spawnBounds.z, spawnBounds.z));
            Vector3 endPosition = new Vector3(x, Random.Range(-spawnBounds.y, spawnBounds.y), Random.Range(-spawnBounds.z, spawnBounds.z)) - transform.position;
            Vector3 rotation = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            details[i] = Instantiate(detail, transform);
            details[i].transform.localPosition = startPosition;
            details[i].GetComponent<DetailController>().endPosition = endPosition;
            details[i].GetComponent<DetailController>().rotation = rotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Detail"))
        {
            float x = Random.Range(0f, 1f) > 0.5f ? spawnBounds.x : -spawnBounds.x;
            Vector3 startPosition = new Vector3(x, Random.Range(-spawnBounds.y, spawnBounds.y), Random.Range(-spawnBounds.z, spawnBounds.z));
            Vector3 endPosition = new Vector3(-x, Random.Range(-spawnBounds.y, spawnBounds.y), Random.Range(-spawnBounds.z, spawnBounds.z)) - transform.position;
            Vector3 rotation = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            other.transform.localPosition = startPosition;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<DetailController>().endPosition = endPosition;
            other.GetComponent<DetailController>().rotation = rotation;
        }
    }
}
