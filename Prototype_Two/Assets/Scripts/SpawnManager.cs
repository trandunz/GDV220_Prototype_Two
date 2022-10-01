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

    [Header("Eel")]
    [SerializeField] GameObject EelPrefab;
    [SerializeField] float EelSpawnRate = 8.0f;

    [Header("School of Fish")]
    [SerializeField] GameObject SchoolOfFishPrefab;
    [SerializeField] float SchoolOfFishSpawnRate = 14.0f;

    [Header("Bubble")]
    [SerializeField] GameObject BubblePrefab;
    [SerializeField] float BubbleSpawnRate = 10.0f;

    [Header("Bubble Buff")]
    [SerializeField] GameObject BubleBuffChestPrefab;
    [SerializeField] float BubbleBuffSpawnRate = 7.0f;

    [Header("Sea Mine")]
    [SerializeField] GameObject SeaMinePrefab;
    [SerializeField] float SeaMineSpawnRate = 14.0f;

    [Header("Clam")]
    [SerializeField] GameObject ClamPrefab;
    [SerializeField] float ClamSpawnRate = 7.0f;

    public SpawnableObject SeaUrchin;
    public SpawnableObject JellyFishSwarm;
    public SpawnableObject Oxygem;
    public SpawnableObject Eel;
    public SpawnableObject SchoolOfFish;
    public SpawnableObject OxygenBubble;
    public SpawnableObject BubbleBuffChest;
    public SpawnableObject SeaMine;
    public SpawnableObject Clam;

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

        OxygenBubble.Object = BubblePrefab;
        OxygenBubble.DepthCounter = 0.0f;
        OxygenBubble.SpawnRate = BubbleSpawnRate;

        BubbleBuffChest.Object = BubleBuffChestPrefab;
        BubbleBuffChest.DepthCounter = 0.0f;
        BubbleBuffChest.SpawnRate = BubbleBuffSpawnRate;

        SeaMine.Object = SeaMinePrefab;
        SeaMine.DepthCounter = 0.0f;
        SeaMine.SpawnRate = SeaMineSpawnRate;

        Clam.Object = ClamPrefab;
        Clam.DepthCounter = 0.0f;
        Clam.SpawnRate = ClamSpawnRate;
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
            int random = Random.Range(0, 3);
            if (random <= 0)
            {
                SpawnBubbleBuff(cameraPosition);
            }
            else if (random <= 1)
            {
                SpawnClam(cameraPosition);
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

        if ((-Depth) >= SeaMine.DepthCounter)
        {
            SpawnSeaMine(cameraPosition);
        }
    }

    void SpawnJellyFishSwarm(Vector3 camPos)
    {
        JellyFishSwarm.Offset = Random.Range(-5.0f, 5.0f);
        JellyFishSwarm.SpawnPoint = new Vector3(camPos.x + JellyFishSwarm.Offset, camPos.y, camPos.z);
        Instantiate(JellyFishSwarm.Object, JellyFishSwarm.SpawnPoint, Quaternion.identity);
        JellyFishSwarm.DepthCounter = -Depth + JellyFishSwarm.SpawnRate;
    }

    void SpawnSeaMine(Vector3 camPos)
    {
        SeaMine.Offset = Random.Range(-6.5f, 6.5f);
        SeaMine.SpawnPoint = new Vector3(camPos.x + SeaMine.Offset, camPos.y, camPos.z);
        Instantiate(SeaMine.Object, SeaMine.SpawnPoint, Quaternion.identity);
        SeaMine.DepthCounter = -Depth + SeaMine.SpawnRate;
    }
    
    void SpawnBubble(Vector3 camPos)
    {
        OxygenBubble.Offset = Random.Range(-6.5f, 6.5f);
        OxygenBubble.SpawnPoint = new Vector3(camPos.x + OxygenBubble.Offset, camPos.y, camPos.z);
        Instantiate(OxygenBubble.Object, OxygenBubble.SpawnPoint, Quaternion.identity);
        OxygenBubble.DepthCounter = -Depth + OxygenBubble.SpawnRate;
    }

    void SpawnClam(Vector3 camPos)
    {
        // Spawn Left or Right
        int random = Random.Range(-3, 4);
        if (random <= 0)
        {
            Quaternion rotation = Quaternion.Euler(-25, -90, -20);
            Clam.Offset = -6.0f;
            Clam.SpawnPoint = new Vector3(camPos.x + Clam.Offset, camPos.y, camPos.z);
            Instantiate(Clam.Object, Clam.SpawnPoint, rotation);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(-25, 90, 20);
            Clam.Offset = 6.0f;
            Clam.SpawnPoint = new Vector3(camPos.x + Clam.Offset, camPos.y, camPos.z);
            Instantiate(Clam.Object, Clam.SpawnPoint, rotation);
        }

        // Setting SeaUrchin and Eel depth counter because they are together
        BubbleBuffChest.DepthCounter = -Depth + BubbleBuffChest.SpawnRate;
        SeaUrchin.DepthCounter = -Depth + BubbleBuffChest.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
        Clam.DepthCounter = -Depth + Clam.SpawnRate;
    }

    void SpawnBubbleBuff(Vector3 camPos)
    {
        // Spawn Left or Right
        int random = Random.Range(-3, 4);
        if (random <= 0)
        {
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            BubbleBuffChest.Offset = -6.0f;
            BubbleBuffChest.SpawnPoint = new Vector3(camPos.x + BubbleBuffChest.Offset, camPos.y, camPos.z);
            Instantiate(BubbleBuffChest.Object, BubbleBuffChest.SpawnPoint, rotation);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            BubbleBuffChest.Offset = 6.0f;
            BubbleBuffChest.SpawnPoint = new Vector3(camPos.x + BubbleBuffChest.Offset, camPos.y, camPos.z);
            Instantiate(BubbleBuffChest.Object, BubbleBuffChest.SpawnPoint, rotation);
        }

        // Setting SeaUrchin and Eel depth counter because they are together
        BubbleBuffChest.DepthCounter = -Depth + BubbleBuffChest.SpawnRate;
        SeaUrchin.DepthCounter = -Depth + BubbleBuffChest.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
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

        // Setting SeaUrchin and Eel depth counter because they are together
        SeaUrchin.DepthCounter = -Depth + SeaUrchin.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
    }

    void SpawnEel(Vector3 camPos)
    {
        Eel.Offset = -8.5f;
        Eel.SpawnPoint = new Vector3(camPos.x + Eel.Offset, camPos.y, camPos.z);
        Instantiate(Eel.Object, Eel.SpawnPoint, Quaternion.identity);

        // Setting SeaUrchin and Eel depth counter because they are together
        SeaUrchin.DepthCounter = -Depth + Eel.SpawnRate;
        Eel.DepthCounter = -Depth + Eel.SpawnRate;
    }

    void SpawnSchoolOfFish(Vector3 camPos)
    {
        SchoolOfFish.Offset = 0;  //Random.Range(-6.5f, 6.5f);
        SchoolOfFish.SpawnPoint = new Vector3(camPos.x + SchoolOfFish.Offset, camPos.y, camPos.z + 10.0f);
        Instantiate(SchoolOfFish.Object, SchoolOfFish.SpawnPoint, Quaternion.identity);
        SchoolOfFish.DepthCounter = -Depth + SchoolOfFish.SpawnRate;
    }
}
