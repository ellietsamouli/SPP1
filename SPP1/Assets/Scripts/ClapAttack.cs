using System.Collections;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject cubePrefab;
    public Transform spawnPoint; // Reference to the empty GameObject acting as the spawn point
    public float cubeSpeed = 5f;
    public float delay = 1.0f;      // Adjust the delay time before moving the cube
    public float levitationDuration = 1.0f;  // Adjust the duration of levitation
    public float levitationHeight = 1.0f;  // Adjust the height of levitation
    public float disappearanceDelay = 3.0f;  // Adjust the delay time before destroying the cube

    private bool isShooting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isShooting)
        {
            Debug.Log("E key pressed");

            // Trigger the animation and instantiate the cube with delays
            StartCoroutine(ShootWithDelays());
        }
    }

    private IEnumerator ShootWithDelays()
    {
        isShooting = true;

        // Trigger the animation with a delay
        yield return new WaitForSeconds(delay);
        playerAnimator.SetTrigger("KnockOut");

        // Wait for the animation to complete before instantiating the cube
        yield return new WaitForSeconds(0.1f);  // Adjust as needed

        // Instantiate the cube using the spawn point as the position
        StartCoroutine(InstantiateCube(spawnPoint.position));

        isShooting = false;
    }

    private IEnumerator InstantiateCube(Vector3 spawnPosition)
    {
        Debug.Log("Instantiating cube");

        // Instantiate a cube at the specified spawn position
        GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        // Levitate the cube for the specified duration
        yield return StartCoroutine(LevitateCube(cube, levitationDuration));

        // Calculate the opposite direction of the player
        Vector3 oppositeDirection = -transform.forward;

        // Apply force to the cube in the opposite direction after levitation
        cube.GetComponent<Rigidbody>().AddForce(oppositeDirection * cubeSpeed, ForceMode.Impulse);

        // Destroy the cube after a certain amount of time
        Destroy(cube, disappearanceDelay);
    }

    private IEnumerator LevitateCube(GameObject cube, float duration)
    {
        float elapsed = 0f;
        Vector3 initialPosition = cube.transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newY = Mathf.Lerp(initialPosition.y, initialPosition.y + levitationHeight, t);
            cube.transform.position = new Vector3(cube.transform.position.x, newY, cube.transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

