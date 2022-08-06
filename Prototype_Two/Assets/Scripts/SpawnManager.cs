using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Main Cam for depth")]
    [SerializeField] GameObject Main_Camera;
   
    [Header("Current Depth")]
    [SerializeField] float Depth;
   
    [Header("Axis offsets")]
    [SerializeField] float ZOffset;
    [SerializeField] float YOffset;

    [Header("Jelly Fish Swarm")]
    [SerializeField] GameObject JellyFishSwarmPrefab;
    [SerializeField] float JellyFishSwarmSpawnRate = 8.0f;

    [Header("Oxygem")]
    [SerializeField] GameObject OxygemPrefab;
    [SerializeField] float OxygemSpawnRate = 3.0f;

    [Header("SeaUrchin")]
    [SerializeField] GameObject SeaUrchinPrefab;
    [SerializeField] float SeaUrchinSpawnRate = 7.0f;

    public SpawnableObject SeaUrchin;
    public SpawnableObject JellyFishSwarm;
    public SpawnableObject Oxygem;

    public struct SpawnableObject{
        public GameObject Object;
        public float DepthCounter;
        public float SpawnRate;
        public Vector3 SpawnPoint;
        public float Offset;
        };

    private void Start()
    {
        // Initialize SeaUrchin
        SeaUrchin.Object = SeaUrchinPrefab;
        SeaUrchin.DepthCounter = 0.0f;
        SeaUrchin.SpawnRate = SeaUrchinSpawnRate;

        // Initialize JellyFishSwarm
        JellyFishSwarm.Object = JellyFishSwarmPrefab;
        JellyFishSwarm.DepthCounter = 0.0f;
        JellyFishSwarm.SpawnRate = JellyFishSwarmSpawnRate;

        // Initialize Oxygem
        Oxygem.Object = OxygemPrefab;
        Oxygem.DepthCounter = 0.0f;
        Oxygem.SpawnRate = OxygemSpawnRate;
    }
    private void Update()
    {
        // Update Depth to camera position
        Depth = Main_Camera.transform.position.y;

        // Update spawnpoints
        Vector3 camPos = Main_Camera.transform.position;
        camPos.y += YOffset;
        camPos.z += ZOffset;


        if ((-Depth) >= SeaUrchin.DepthCounter)
        {
            SpawnJellyFishSwarm(camPos);
        }
        
        if ((-Depth) >= Oxygem.DepthCounter)
        {
            SpawnOxygem(camPos);
        }
        
        if ((-Depth) >= SeaUrchin.DepthCounter)
        {
            SpawnSeaUrchin(camPos);
        }
    }

    void SpawnJellyFishSwarm(Vector3 camPos)
    {
        JellyFishSwarm.Offset = Random.Range(-5.0f, 5.0f);
        JellyFishSwarm.SpawnPoint = new Vector3(camPos.x + JellyFishSwarm.Offset, camPos.y, camPos.z);
        Instantiate(JellyFishSwarm.Object, JellyFishSwarm.SpawnPoint, Quaternion.identity);
        JellyFishSwarm.DepthCounter = -Depth + JellyFishSwarm.SpawnRate;
    }
    
    void SpawnOxygem(Vector3 camPos)
    {
        Oxygem.Offset = Random.Range(-6.5f, 6.5f);
        Oxygem.SpawnPoint = new Vector3(camPos.x + Oxygem.Offset, camPos.y, camPos.z);
        Instantiate(Oxygem.Object, Oxygem.SpawnPoint, Quaternion.identity);
        Oxygem.DepthCounter = -Depth + Oxygem.SpawnRate;
    }

    void SpawnSeaUrchin(Vector3 camPos)
    {
        // Spawn Left or Right
        int random = Random.Range(-3, 4);
        if (random <= 0)
        {
            SeaUrchin.Offset = -8.0f;
            SeaUrchin.SpawnPoint = new Vector3(camPos.x + SeaUrchin.Offset, camPos.y, camPos.z);
            Instantiate(SeaUrchin.Object, SeaUrchin.SpawnPoint, Quaternion.identity);
        }
        else
        {
            SeaUrchin.Offset = 8.0f;
            SeaUrchin.SpawnPoint = new Vector3(camPos.x + SeaUrchin.Offset, camPos.y, camPos.z);
            Quaternion rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
            Instantiate(SeaUrchin.Object, SeaUrchin.SpawnPoint, (rotation));
        }
        SeaUrchin.DepthCounter = -Depth + SeaUrchin.SpawnRate;
    }
}
