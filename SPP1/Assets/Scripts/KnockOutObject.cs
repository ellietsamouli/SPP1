using System.Collections;
using UnityEngine;

public class KnockOutObject : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject cubePrefab;
    public float cubeSpeed = 5f;
    public float delay = 1.0f; // Adjust the delay time as needed

    private bool cubeInstantiated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !cubeInstantiated)
        {
            // Trigger the animation and instantiate the cube after the delay
            StartCoroutine(ShootWithDelay());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        // Trigger the animation
        playerAnimator.SetTrigger("Shoot");

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Instantiate a cube
        GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);

        // Apply force to the cube in the direction the player is looking at
        Vector3 playerForward = transform.forward;
        cube.GetComponent<Rigidbody>().AddForce(playerForward * cubeSpeed, ForceMode.Impulse);

        // Set the flag to prevent multiple instantiations
        cubeInstantiated = true;
    }



}
