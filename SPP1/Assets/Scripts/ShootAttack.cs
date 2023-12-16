using System.Collections;
using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject cubePrefab;
    public float cubeSpeed = 5f;
    public float delay = 1.0f;  // Adjust the delay time before moving the cube
    public float disappearanceDelay = 3.0f;  // Adjust the delay time before destroying the cube

    private bool isShooting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isShooting)
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
        playerAnimator.SetTrigger("Shoot");

        // Wait for the animation to complete before instantiating the cube
        yield return new WaitForSeconds(0.1f);  // Adjust as needed

        // Instantiate the cube
        InstantiateCube();

        isShooting = false;
    }

    private void InstantiateCube()
    {
        Debug.Log("Instantiating cube");

        // Instantiate a cube
        GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);

        // Calculate the opposite direction of the player
        Vector3 oppositeDirection = -transform.forward;

        // Apply force to the cube in the opposite direction
        cube.GetComponent<Rigidbody>().AddForce(oppositeDirection * cubeSpeed, ForceMode.Impulse);

        // Destroy the cube after a certain amount of time
        Destroy(cube, disappearanceDelay);
    }

}
