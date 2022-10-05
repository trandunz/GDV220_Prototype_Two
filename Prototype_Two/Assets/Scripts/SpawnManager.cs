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

    [Header("Coral")]
    [SerializeField] GameObject[] CoralPrefabs;
    [SerializeField] float CoralSpawnRate = 3.0f;
    private GameObject CachedLastCoralSpawned;

    [Header("Squid")]
    [SerializeField] GameObject SquidPrefab;
    [SerializeField] float SquidSpawnRate = 14.0f;

    [Header("Angler")]
    [SerializeField] GameObject AnglerPrefab;
    [SerializeField] float AnglerSpawnRate = 14.0f;

    CameraMovement CameraMovement;

    public SpawnableObject SeaUrchin;
    public SpawnableObject JellyFishSwarm;
    public SpawnableObject Oxygem;
    public SpawnableObject Eel;
    public SpawnableObject SchoolOfFish;
    public SpawnableObject OxygenBubble;
    public SpawnableObject BubbleBuffChest;
    public SpawnableObject SeaMine;
    public SpawnableObject Clam;
    public SpawnableObject Coral;
    public SpawnableObject Squid;
    public SpawnableObject Angler;

    public struct SpawnableObject{
        public GameObject Object;
        public float DepthCounter;
        public float SpawnRate;
        public Vector3 SpawnPoint;
        public float Offset;
        };

    private void Start()
    {
        CameraMovement = FindObjectOfType<CameraMovement>();

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

        // Initialize Coral
        Coral.Object = CoralPrefabs[0];
        Coral.DepthCounter = 0.0f;
        Coral.SpawnRate = CoralSpawnRate;

        Squid.Object = SquidPrefab;
        Squid.DepthCounter = 0.0f;
        Squid.SpawnRate = SquidSpawnRate;

        Angler.Object = AnglerPrefab;
        Angler.DepthCounter = 0.0f;
        Angler.SpawnRate = AnglerSpawnRate;
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
            int random = Random.Range(0, 3);
            if (random == 0)
                SpawnSeaMine(cameraPosition);
            else if (random == 1)
                SpawnSquid(cameraPosition);
            else if (CameraMovement.lightingLevel <= 1)
                SpawnAngler(cameraPosition);
        }

        if ((-Depth) >= Coral.DepthCounter)
        {
            // Leftside
            Coral.Offset = -8.0f;
            SpawnCoral(cameraPosition);

            // Rightside
            Coral.Offset = 8.0f;
            SpawnCoral(cameraPosition);
        }
    }

    void SpawnAngler(Vector3 camPos)
    {
        SeaMine.DepthCounter = -Depth + SeaMine.SpawnRate;
        Squid.DepthCounter = -Depth + Squid.SpawnRate;
        Angler.DepthCounter = -Depth + Angler.SpawnRate;
        Angler.Offset = Random.Range(-5.5f, 5.5f);
        Angler.SpawnPoint = new Vector3(camPos.x + SeaMine.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Angler.Object, Angler.SpawnPoint, Quaternion.identity), ObjectLifeTime);
    }

    void SpawnJellyFishSwarm(Vector3 camPos)
    {
        JellyFishSwarm.Offset = Random.Range(-5.0f, 5.0f);
        JellyFishSwarm.SpawnPoint = new Vector3(camPos.x + JellyFishSwarm.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(JellyFishSwarm.Object, JellyFishSwarm.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        JellyFishSwarm.DepthCounter = -Depth + JellyFishSwarm.SpawnRate;
    }

    void SpawnSeaMine(Vector3 camPos)
    {
        SeaMine.Offset = Random.Range(-6.5f, 6.5f);
        SeaMine.SpawnPoint = new Vector3(camPos.x + SeaMine.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(SeaMine.Object, SeaMine.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        SeaMine.DepthCounter = -Depth + SeaMine.SpawnRate;
    }
    
    void SpawnSquid(Vector3 camPos)
    {
        Squid.Offset = Random.Range(-6.5f, 6.5f);
        Squid.SpawnPoint = new Vector3(camPos.x + Squid.Offset, camPos.y, camPos.z);
        Destroy(Instantiate(Squid.Object, Squid.SpawnPoint, Quaternion.identity), ObjectLifeTime);
        SeaMine.DepthCounter = -Depth + SeaMine.SpawnRate;
        Squid.DepthCounter = -Depth + Squid.SpawnRate;
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

    void SpawnClam(Vector3 camPos)
    {
        // Spawn Left or Right
        int random = Random.Range(-3, 4);
        if (random <= 0)
        {
            Quaternion rotation = Quaternion.Euler(-25, -90, -20);
            Clam.Offset = -6.0f;
            Clam.SpawnPoint = new Vector3(camPos.x + Clam.Offset, camPos.y, camPos.z);
            Destroy(Instantiate(Clam.Object, Clam.SpawnPoint, rotation), ObjectLifeTime);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(-25, 90, 20);
            Clam.Offset = 6.0f;
            Clam.SpawnPoint = new Vector3(camPos.x + Clam.Offset, camPos.y, camPos.z);
            Destroy(Instantiate(Clam.Object, Clam.SpawnPoint, rotation), ObjectLifeTime);
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
            Destroy(Instantiate(BubbleBuffChest.Object, BubbleBuffChest.SpawnPoint, rotation), ObjectLifeTime);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            BubbleBuffChest.Offset = 6.0f;
            BubbleBuffChest.SpawnPoint = new Vector3(camPos.x + BubbleBuffChest.Offset, camPos.y, camPos.z);
            Destroy(Instantiate(BubbleBuffChest.Object, BubbleBuffChest.SpawnPoint, rotation), ObjectLifeTime);
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
        int coralIndex;

        // First make sure its not spawning the same thing 
        // twice in a row
        coralIndex = Random.Range(0, CoralPrefabs.Length - 1);
        Coral.Object = CoralPrefabs[coralIndex];

        // Get rid of duplicate spawns
       // if (Coral.Object != CachedLastCoralSpawned)
       // {
       //     coralIndex = ((CoralPrefabs.Length - 1) - coralIndex);
       //     Coral.Object = CoralPrefabs[coralIndex];
       // }
       //   CachedLastCoralSpawned = Coral.Object;

        // rotation
        if (coralIndex >= 5) // Starfish
        {
            x = 0.0f;
            y = 0.0f;
            z = Random.Range(0.0f, 360.0f);
        }
        else // Not starfish (sea weed, coral, etc)
        {
            if (Coral.Offset < 0.0f)
            {
                y = 60.0f;
                z = -90.0f;
            }
            else
            {
                y = -60.0f;
                z = 90.0f;
            }
            float randomX = Random.Range(0.0f, 360.0f);
            x = randomX;
        }
        rot.eulerAngles = new Vector3(x, y, z);

        // position
        x = Random.Range(-0.5f, 0.5f);
        y = Random.Range(0.0f, 2.0f);
        Coral.SpawnPoint = new Vector3(camPos.x + Coral.Offset + x, camPos.y + y, -4.0f);

        // scale
        x = Random.Range(0.3f, 0.5f);

        Transform transform = Coral.Object.transform;
        //= new Transform()//(Coral.SpawnPoint, rot.eulerAngles, Coral.SpawnPoint);
        transform.position = Coral.SpawnPoint;
        transform.rotation = rot;
        transform.localScale = new Vector3(x, x, x);
        Destroy(Instantiate(Coral.Object, transform), ObjectLifeTime);// Coral.SpawnPoint, rot), ObjectLifeTime);
        Coral.DepthCounter = -Depth + Coral.SpawnRate;



        //// create variables
        //Quaternion rot = Quaternion.identity;
        //float x, y, z;

        //// Left side
        //int coralIndex = Random.Range(0, CoralPrefabs.Length - 1);
        //Coral.Object = CoralPrefabs[coralIndex];
        //if (coralIndex >= 5)
        //{
        //    x = 0.0f;
        //    y = 0.0f;
        //    z = Random.Range(0.0f, 360.0f);
        //}
        //else
        //{
        //    y = 60.0f;
        //    z = -90.0f;
        //    float randomX = Random.Range(0.0f, 360.0f);
        //    x = randomX;
        //}
        //rot.eulerAngles = new Vector3(x, y, z);
        //Coral.Offset = -8.0f;
        //Coral.SpawnPoint = new Vector3(camPos.x + Coral.Offset, camPos.y, -4.0f);
        //Destroy(Instantiate(Coral.Object, Coral.SpawnPoint, rot), ObjectLifeTime);
        //Coral.DepthCounter = -Depth + Coral.SpawnRate;

        //// Right side
        //coralIndex = Random.Range(0, CoralPrefabs.Length - 1);
        //Coral.Object = CoralPrefabs[coralIndex];
        //if (coralIndex >= 5)
        //{
        //    x = 0.0f;
        //    y = 0.0f;
        //    z = Random.Range(0.0f, 360.0f); ;
        //}
        //else
        //{
        //    y = -60.0f;
        //    z = 90.0f;
        //    float randomX = Random.Range(0.0f, 360.0f);
        //    x = randomX;
        //}
        //rot.eulerAngles = new Vector3(x, y, z);
        //Coral.Offset = 8.0f;
        //Coral.SpawnPoint = new Vector3(camPos.x + Coral.Offset, camPos.y, -4.0f);
        //Destroy(Instantiate(Coral.Object, Coral.SpawnPoint, rot), ObjectLifeTime);
        //Coral.DepthCounter = -Depth + Coral.SpawnRate;
    }
}
