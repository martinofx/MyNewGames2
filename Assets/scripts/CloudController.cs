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
    public Vector2 spawnRangeX = new Vector2(-10f, 10f); // Rango de posiciones X para la aparición
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
            // Posición aleatoria dentro de los rangos de spawn
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            float randomDepth = Random.Range(minDepth, maxDepth);  // Profundidad aleatoria

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomDepth);

            GameObject newCloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
            newCloud.transform.localScale *= Random.Range(0.8f, 1.2f);  // Escalado aleatorio para dar variedad
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            // Asignar la velocidad aleatoria a cada nube
            newCloud.AddComponent<CloudMovement>().SetSpeed(randomSpeed);
            activeClouds.Add(newCloud);
        }
    }

    void MoveClouds()
    {
        foreach (GameObject cloud in activeClouds)
        {
            // Las nubes ya tienen su velocidad asignada, por lo que se mueven automáticamente
        }
    }

    void RecycleClouds()
    {
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            if (activeClouds[i].transform.position.y < spawnRangeX.x)  // Si la nube sale del lado derecho de la pantalla
            {
                // Reposicionar la nube en el lado izquierdo
                float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
                float randomDepth = Random.Range(minDepth, maxDepth);  // Profundidad aleatoria
                activeClouds[i].transform.position = new Vector3(spawnRangeX.x, randomX, randomDepth);
            }
        }
    }
}