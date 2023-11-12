using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRunTrack : MyMonoBehaviour
{
    //[SerializeField] protected List<Transform> runTracks;
    
    private BoxCollider boxCollider;
    private int currentRunTrack = 0;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        for (int i = 0; i<2; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, 216 * i);
            string randString = RandomRunTrack();
            Transform newRunTrack = RunTrackSpawner.Instance.Spawn(randString, spawnPos);
            if (newRunTrack == null) return;
            newRunTrack.gameObject.SetActive(true);
            currentRunTrack++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().IncreaseSpeed();
            boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z + 216);

            ResetRunTrack();
        }
    }

    protected virtual void ResetRunTrack()
    {
        // Tạo đường chạy mới
        Vector3 spawnPos = new Vector3(0, 0, 216 * currentRunTrack);
        string randString = RandomRunTrack();
        Transform newRunTrack = RunTrackSpawner.Instance.Spawn(randString, spawnPos);
        if (newRunTrack == null) return;
        newRunTrack.gameObject.SetActive(true);
        currentRunTrack++;
    }

    private string RandomRunTrack()
    {
        int rand = Random.RandomRange(0, 3);
        if (rand == 0) return RunTrackSpawner.runTrackOne.ToString();
        else if (rand == 1) return RunTrackSpawner.runTrackTwo.ToString();
        else return RunTrackSpawner.runTrackThree.ToString();
    }
}
