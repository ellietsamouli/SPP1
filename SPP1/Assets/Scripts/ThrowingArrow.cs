using System.Collections;
using UnityEngine;

public class ThrowingArrow : MonoBehaviour
{
    public Transform socket;
    public GameObject arrowPrefab;
    public float arrowSpeed = 10;
    public float delay = 1.0f; // Adjust the delay time as needed

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SpawnBulletWithDelay());
        }
    }

    IEnumerator SpawnBulletWithDelay()
    {
        yield return new WaitForSeconds(delay);

        var bullet = Instantiate(arrowPrefab, socket.position, socket.rotation);
        bullet.GetComponent<Rigidbody>().velocity = socket.forward * arrowSpeed;
    }
}
