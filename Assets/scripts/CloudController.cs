using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject[] clouds;  // Array de prefabs de nubes
    public float minSpeed = 1f;  // Velocidad mínima de las nubes
    public float maxSpeed = 3f;  // Velocidad máxima de las nubes
    public float minDepth = -5f; // Profundidad mínima para el efecto parallax
    public float maxDepth = 5f;  // Profundidad máxima para el efecto parallax
    public Vector2 spawnRangeX = new Vector2(10f, 15f); // Rango de posiciones X para la aparición (empieza desde la derecha)
    public Vector2 spawnRangeY = new Vector2(-3f, 3f);   // Rango de posiciones Y para la aparición

    private List<GameObject> activeClouds = new List<GameObject>();

    void Start()
    {
        SpawnClouds();
    }

    void Update()
    {
        MoveClouds();
        RecycleClouds();
    }

    void SpawnClouds()
    {
        foreach (GameObject cloudPrefab in clouds)
        {
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);  // Posición aleatoria dentro del rango de spawn en X
            float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);  // Posición aleatoria en Y
            float randomDepth = Random.Range(minDepth, maxDepth);        // Profundidad aleatoria

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomDepth);

            GameObject newCloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
            newCloud.transform.localScale *= Random.Range(0.8f, 1.2f);  // Escala aleatoria
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            newCloud.AddComponent<CloudMovement>().SetSpeed(randomSpeed);
            activeClouds.Add(newCloud);
        }
    }

    void MoveClouds()
    {
        // Aquí las nubes ya están moviéndose automáticamente debido al componente CloudMovement
    }

    void RecycleClouds()
    {
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            if (activeClouds[i].transform.position.x < -spawnRangeX.x)  // Si la nube sale del lado izquierdo
            {
                float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);  // Posición Y aleatoria
                float randomDepth = Random.Range(minDepth, maxDepth);        // Profundidad aleatoria
                activeClouds[i].transform.position = new Vector3(spawnRangeX.y, randomY, randomDepth);  // Reposicionar a la derecha
            }
        }
    }
}
