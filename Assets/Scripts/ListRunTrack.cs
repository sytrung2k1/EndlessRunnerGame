using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRunTrack : MonoBehaviour
{
    public GameObject[] runTracks;

    public List<GameObject> newRunTracks;

    private BoxCollider boxCollider;
    private int currentRunTrack = 0;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        for (int i = 0; i < 3; i++)
        {
            newRunTracks.Add(Instantiate(runTracks[Random.Range(0, runTracks.Length)], transform));
            newRunTracks[i].transform.localPosition = new Vector3(0, 0, 216 * i);
            newRunTracks[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().IncreaseSpeed();
            boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z + 216);

            StartCoroutine(ResetRunTrack());
            
        }
    }

    private IEnumerator ResetRunTrack()
    {
        yield return new WaitForSeconds(3f);

        // Xóa đường chạy cũ
        Destroy(newRunTracks[0]);
        newRunTracks.RemoveAt(0);

        // Tạo đường chạy mới
        GameObject newRunTrack = Instantiate(runTracks[Random.Range(0, runTracks.Length)], transform);
        newRunTrack.transform.localPosition = new Vector3(0, 0, 216 * (currentRunTrack + 3));
        newRunTrack.SetActive(true);
        newRunTracks.Add(newRunTrack);

        currentRunTrack++;
    }
}
