using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Object Lifetime")]
    [SerializeField] float ObjectLifeTime = 20.0f;

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

    [Header("Eel")]
    [SerializeField] GameObject EelPrefab;
    [SerializeField] float EelSpawnRate = 8.0f;

    [Header("School of Fish")]
    [SerializeField] GameObject SchoolOfFishPrefab;
    [SerializeField] float SchoolOfFishSpawnRate = 14.0f;

    [Header("Bubble")]
    [SerializeField] GameObject BubblePrefab;
    [SerializeField] float BubbleSpawnRate = 20.0f;

    [Header("Coral")]
    [SerializeField] GameObject[] CoralPrefabs;
    [SerializeField] float CoralSpawnRate = 2.0f;

    public SpawnableObject SeaUrchin;
    public SpawnableObject JellyFishSwarm;
    public SpawnableObject Oxygem;
    public SpawnableObject Eel;
    public SpawnableObject SchoolOfFish;
    public SpawnableObject OxygenBubble;
    public SpawnableObject Coral;

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

        // Initialize Eel
        Eel.Object = EelPrefab;
        Eel.DepthCounter = 0.0f;
        Eel.SpawnRate = EelSpawnRate;

        // Initialize School of Fish
        SchoolOfFish.Object = SchoolOfFishPrefab;
        SchoolOfFish.DepthCounter = 0.0f;
        SchoolOfFish.SpawnRate = SchoolOfFishSpawnRate;

        // Initialize Oxygen Bubble
        OxygenBubble.Object = BubblePrefab;
        OxygenBubble.DepthCounter = 0.0f;
        OxygenBubble.SpawnRate = BubbleSpawnRate;

        // Initialize Coral
        Coral.Object = CoralPrefabs[0];
        Coral.DepthCounter = 0.0f;
        Coral.SpawnRate = CoralSpawnRate;

    }
    private void Update()
    {
        // Update Depth to camera position
        Depth = Main_Camera.transform.position.y;

        // Update spawnpoints
        Vector3 cameraPosition = Main_Camera.transform.position;
        cameraPosition.y += YOffset;
        cameraPosition.z += ZOffset;

        // Triggers spawns based on each objects depth counter
        if ((-Depth) >= JellyFishSwarm.DepthCounter)
        {
            SpawnJellyFishSwarm(cameraPosition);
        }
        if ((-Depth) >= Oxygem.DepthCounter)
        {
            SpawnOxygem(cameraPosition);
        }

        // Spawn either SeaUrchin or Eel
        if ((-Depth) >= SeaUrchin.DepthCounter)
        {
            int random = Random.Range(-4, 4);
            if (random <= 0)
            {
                SpawnSeaUrchin(cameraPosition);
            }
            else
            {
                SpawnEel(cameraPosition);
            }
        }

        if ((-Depth) >= SchoolOfFish.DepthCounter)
        {
            SpawnSchoolOfFish(cameraPosition);
        }

        if ((-Depth) >= OxygenBubble.DepthCounter)
        {
            //SpawnBubble(cameraPosition);
        }

        if ((-Depth) >= Coral.DepthCounter)
        {
            SpawnCoral(cameraPosition);
        }
    }

    void SpawnJellyFishSwarm(Vector3 camPos)
    {
        JellyFishSwarm.Offset = Random.Range(-5.0f, 5.0f);
        JellyFishSwarm.SpawnPoint = new Vector3(camPos.x + JellyFishSwarm.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(JellyFishSwarm.Object, JellyFishSwarm.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        JellyFishSwarm.DepthCounter = -Depth + JellyFishSwarm.SpawnRate;
    }
    
    void SpawnBubble(Vector3 camPos)
    {
        OxygenBubble.Offset = Random.Range(-6.5f, 6.5f);
        OxygenBubble.SpawnPoint = new Vector3(camPos.x + OxygenBubble.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(OxygenBubble.Object, OxygenBubble.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        OxygenBubble.DepthCounter = -Depth + OxygenBubble.SpawnRate;
    }

    public void SpawnBubble()
    {
        if ((-Depth) >= OxygenBubble.DepthCounter)
        {
            Vector3 camPos = Main_Camera.transform.position;
            camPos.y += YOffset;
            camPos.z += ZOffset;

            OxygenBubble.Offset = Random.Range(-6.5f, 6.5f);
            OxygenBubble.SpawnPoint = new Vector3(camPos.x + OxygenBubble.Offset, camPos.y, camPos.z);
            Destroy(Instantiate(OxygenBubble.Object, OxygenBubble.SpawnPoint, Quaternion.identity), ObjectLifeTime);
            OxygenBubble.DepthCounter = -Depth + OxygenBubble.SpawnRate;        
        }

    }

    void SpawnOxygem(Vector3 camPos)
    {
        Oxygem.Offset = Random.Range(-6.5f, 6.5f);
        Oxygem.SpawnPoint = new Vector3(camPos.x + Oxygem.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Oxygem.Object, Oxygem.SpawnPoint, Quaternion.identity), ObjectLifeTime);
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
            Destroy(Instantiate(SeaUrchin.Object, SeaUrchin.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        }
        else
        {
            SeaUrchin.Offset = 8.0f;
            SeaUrchin.SpawnPoint = new Vector3(camPos.x + SeaUrchin.Offset, camPos.y, camPos.z);
            Quaternion rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
            Destroy(Instantiate(SeaUrchin.Object, SeaUrchin.SpawnPoint, (rotation)), ObjectLifeTime);
        }

        // Setting SeaUrchin and Eel depth counter because they are together
        SeaUrchin.DepthCounter = -Depth + SeaUrchin.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
    }

    void SpawnEel(Vector3 camPos)
    {
        Eel.Offset = -8.5f;
        Eel.SpawnPoint = new Vector3(camPos.x + Eel.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Eel.Object, Eel.SpawnPoint, Quaternion.identity), ObjectLifeTime);

        // Setting SeaUrchin and Eel depth counter because they are together
        SeaUrchin.DepthCounter = -Depth + Eel.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
    }

    void SpawnSchoolOfFish(Vector3 camPos)
    {
        SchoolOfFish.Offset = 0;  //Random.Range(-6.5f, 6.5f);
        SchoolOfFish.SpawnPoint = new Vector3(camPos.x + SchoolOfFish.Offset, camPos.y, camPos.z + 10.0f);
        Destroy(Instantiate(SchoolOfFish.Object, SchoolOfFish.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        SchoolOfFish.DepthCounter = -Depth + SchoolOfFish.SpawnRate;
    }

    void SpawnCoral(Vector3 camPos)
    {
        // create variables
        Quaternion rot = Quaternion.identity;
        float x, y, z;

        // Left side
        int coralIndex = Random.Range(0, CoralPrefabs.Length - 1);
        Coral.Object = CoralPrefabs[coralIndex];
        if (coralIndex >= 5)
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        else
        {
            y = 60.0f;
            z = -90.0f;
            float randomX = Random.Range(0.0f, 360.0f);
            x = randomX;
        }
        rot.eulerAngles = new Vector3(x, y, z);
        Coral.Offset = -8.0f;
        Coral.SpawnPoint = new Vector3(camPos.x + Coral.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Coral.Object, Coral.SpawnPoint, rot), ObjectLifeTime);
        Coral.DepthCounter = -Depth + Coral.SpawnRate;

        // Right side
        coralIndex = Random.Range(0, CoralPrefabs.Length - 1);
        Coral.Object = CoralPrefabs[coralIndex];
        if (coralIndex >= 5)
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        else
        {
            y = -60.0f;
            z = 90.0f;
            float randomX = Random.Range(0.0f, 360.0f);
            x = randomX;
        }
        rot.eulerAngles = new Vector3(x, y, z);
        Coral.Offset = 8.0f;
        Coral.SpawnPoint = new Vector3(camPos.x + Coral.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Coral.Object, Coral.SpawnPoint, rot), ObjectLifeTime);
        Coral.DepthCounter = -Depth + Coral.SpawnRate;  
    }
}
