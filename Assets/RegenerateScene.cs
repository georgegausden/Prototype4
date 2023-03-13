using UnityEngine;
using UnityEngine.SceneManagement;

public class RegenerateScene : MonoBehaviour
{
    public GameObject sunPrefab; // The prefab for the sun
    public GameObject[] planetPrefabs; // The prefabs for the planets
    public float[] distances; // The distances of each planet from the sun

    private void Start()
    {
        // Create a new game object for holding the solar system
        GameObject solarSystem = new GameObject("Solar System");

        // Instantiate the sun prefab at the center of the scene
        GameObject sun = Instantiate(sunPrefab, Vector3.zero, Quaternion.identity);

        Vector3 scale = Vector3.one * Random.Range(20f, 30f);
        sun.transform.localScale = scale;

        // Make it a child of the solar system object
        sun.transform.parent = solarSystem.transform;

        // Loop through each planet prefab
        for (int i = 0; i < planetPrefabs.Length; i++)
        {
            // Instantiate it at a random position on a circle with a given radius
            float angle = Random.Range(0f, 360f);
            Vector3 position = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distances[i];
            GameObject planet = Instantiate(planetPrefabs[i], position, Quaternion.identity);

            // Set its localScale to a random value between 1 and 10 for each axis
            Vector3 scale2 = Vector3.one * Random.Range(1f, 10f);
            planet.transform.localScale = scale2;

            // Make it a child of the solar system object
            planet.transform.parent = solarSystem.transform;

            // Add a RotateAround component to the planet object and get its reference
            RotateAround rotateAround = planet.AddComponent<RotateAround>();

            // Set the target for the RotateAround component to the sun
            rotateAround.target = sun;

            // Set its speed to a random value between 0 and 5
            rotateAround.speed = Random.Range(0f, 5f);

            // Add a SelfRotate component to the planet object
            planet.AddComponent<SelfRotate>();
        }

        // Add 5 asteroids to the scene
        int numAsteroids = 5;
        float asteroidRadius = 100f;
        for (int i = 0; i < numAsteroids; i++)
        {
            // Instantiate the asteroid prefab at a random position on a circle with the asteroid radius
            float angle = Random.Range(0f, 360f);
            Vector3 position = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * asteroidRadius;
            GameObject asteroid = GameObject.CreatePrimitive(PrimitiveType.Cube);
            asteroid.transform.position = position;

            // Set its localScale to a random value between 1 and 5 for each axis
            Vector3 scale3 = Vector3.one * Random.Range(1f, 5f);
            asteroid.transform.localScale = scale3;

            // Make it a child of the solar system object
            asteroid.transform.parent = solarSystem.transform;

            // Add the "pullable" tag to the asteroid object
            asteroid.tag = "Actors";

            // Add a Rigidbody component to the asteroid object
            Rigidbody rb = asteroid.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }

    private void Update()
    {
        // If space key is pressed, reload this scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
