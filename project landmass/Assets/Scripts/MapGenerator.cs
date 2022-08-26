using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    private Vector2 PlayerCoords;
    public Slider lacunaritySlider;
    private float currentLacunarityValue;
    public Slider persistanceSlider;
    private float currentPersistanceityValue;
    public Slider noiseScaleSlider;
    private float currentScaleValue;
    public Slider seedSlider;
    private float currentSeedValue;

    public Slider Sand;
    public Slider Grass;
    public Slider Forest;

    private float oldLacunarityValue;
    private float oldPersistanceValue;
    private float oldScaleValue;
    private float oldSeedValue;
    private float oldSandValue;
    private float oldGrassValue;
    private float oldForestValue;


    public GameObject MainCamera;
    private float restartScreenTime = 0.5f;
    private float restartScreenTimeSaver;



    public Transform master;
    public GameObject[,] ChunkManager;
    public GameObject player;
    public GameObject chunkObject;
    public GameObject[] trees;
    private List<GameObject> instantiatedTrees;
    public enum DrawMode { noiseMap, colorMap };
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    private int rotationValue = 180;

    public GameObject SeedText;
    public GameObject LacunarityText;
    public GameObject PersistanceText;
    public GameObject ScaleText;



    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offsets;

    public TerrainType[] regions;


    public float[,] savedNoiseMap;
    public Color[] savedColorMap;
    MapDisplay display;

    Vector3 prevPosition;




    public void GenerateMap() {
       
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth,mapHeight,seed, noiseScale,octaves,persistance,lacunarity,offsets); //2D Array with noise vales
        savedNoiseMap = noiseMap;
        instantiatedTrees = new List<GameObject>();

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        savedColorMap[y * mapWidth + x] = regions[i].color;


                        //GENERATE ENTITIES

                        //Tree check
                        if(savedColorMap[y * mapWidth + x] == regions[0].color)
                        {
                            float randomNumber = UnityEngine.Random.Range(0, 7);

                            if(x % 3 == 0 && y % 3 == 0) //for even spacing
                            {
                                if (randomNumber == 1)
                                {
                                    var q = Instantiate(trees[0], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                    q.GetComponent<SpriteRenderer>().sortingOrder = -x;
                                    instantiatedTrees.Add(q);




                                }
                                randomNumber = UnityEngine.Random.Range(0, 5);
                                if(randomNumber == 1)
                                {
                                    var q = Instantiate(trees[1], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                    q.GetComponent<SpriteRenderer>().sortingOrder = -x - 15;
                                    instantiatedTrees.Add(q);

                                }
                                randomNumber = UnityEngine.Random.Range(0, 10);
                                if (randomNumber == 1)
                                {
                                    var q = Instantiate(trees[2], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                    q.GetComponent<SpriteRenderer>().sortingOrder = -x - 15;
                                    instantiatedTrees.Add(q);

                                }
                                randomNumber = UnityEngine.Random.Range(0, 15);
                                if (randomNumber == 1)
                                {
                                    var q = Instantiate(trees[3], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                    q.GetComponent<SpriteRenderer>().sortingOrder = -x;
                                    instantiatedTrees.Add(q);
                                }
                                randomNumber = UnityEngine.Random.Range(0, 35);
                                if (randomNumber == 1)
                                {
                                    var q = Instantiate(trees[4], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                    q.GetComponent<SpriteRenderer>().sortingOrder = -x - 100;
                                    instantiatedTrees.Add(q);

                                }



                            }

                        }
                        if (savedColorMap[y * mapWidth + x] == regions[1].color)
                        {
                            float randomNumber = UnityEngine.Random.Range(0, 500);

                            if (randomNumber == 1)
                            {
                                var q = Instantiate(trees[1], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                q.GetComponent<SpriteRenderer>().sortingOrder = -x;
                                instantiatedTrees.Add(q);

                            }
                            randomNumber = UnityEngine.Random.Range(0, 500);

                            if (randomNumber == 1)
                            {
                                var q = Instantiate(trees[2], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                q.GetComponent<SpriteRenderer>().sortingOrder = -x;
                                instantiatedTrees.Add(q);

                            }
                            randomNumber = UnityEngine.Random.Range(0, 700);

                            if (randomNumber == 1)
                            {
                                var q = Instantiate(trees[3], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                q.GetComponent<SpriteRenderer>().sortingOrder = -x;
                                instantiatedTrees.Add(q);

                            }
                            randomNumber = UnityEngine.Random.Range(0, 600);

                            if (randomNumber == 1)
                            {
                                var q = Instantiate(trees[4], new Vector2(-10 * y, 10 * x), Quaternion.identity);
                                q.GetComponent<SpriteRenderer>().sortingOrder = -x - 100;
                                instantiatedTrees.Add(q);
                            }
                        }
                        break;
                    }
                
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.noiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap)); //draw noise map on plane
        }else if(drawMode == DrawMode.colorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap,mapWidth,mapHeight));
        }
        savedColorMap = colorMap;

        int a = 0;
        int b = 0;
        for(int x = 0; x < 10; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                var q = Instantiate(chunkObject, new Vector2(x*300, y*300), Quaternion.identity);
                for(int i = 0; i < 30; i++)
                {
                    for(int j = 0; j < 30; j++)
                    {

                      
                        
                        if(noiseMap[i + 30 * x ,j + 30 * y] <= regions[0].height)
                        {
                            q.GetComponent<Chunk>().DrawBlock(new Vector3(i, j),1);
                            q.transform.SetParent(master);

                        }else if(noiseMap[i + 30 * x, j + 30 * y] <= regions[1].height)
                        {
                            q.GetComponent<Chunk>().DrawBlock(new Vector3(i, j), 3);
                            q.transform.SetParent(master);
                        }else if(noiseMap[i + 30 * x, j + 30 * y] <= regions[2].height)
                        {
                            q.GetComponent<Chunk>().DrawBlock(new Vector3(i, j), 10);
                            q.transform.SetParent(master);
                        }
                        else
                        {
                            q.GetComponent<Chunk>().DrawBlock(new Vector3(i, j), 2);
                            q.transform.SetParent(master);
                        }
                        
                       
                    }
                   
                }
                a += 30;
                q.GetComponent<Chunk>().RenderMesh();
                ChunkManager[x, y] = q;

            }
        }



      



    }

    private void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
        
    }

    private void Start()
    {
        restartScreenTimeSaver = restartScreenTime;

        instantiatedTrees = new List<GameObject>();
        
        ChunkManager = new GameObject[10, 10];
        savedColorMap = new Color[mapWidth * mapHeight];
        GenerateMap();
        master.transform.rotation = Quaternion.Euler(0, 0, 90);


        display = FindObjectOfType<MapDisplay>();
        lacunaritySlider.value = lacunarity;
        persistanceSlider.value = persistance;
        noiseScaleSlider.value = noiseScale;

        oldLacunarityValue = lacunarity;
        oldPersistanceValue = persistance;
        oldScaleValue = noiseScale;
        oldSeedValue = seed;

        Debug.Log(regions[0].height);
        Sand.value = regions[0].height;
        Grass.value = regions[1].height;
        Forest.value = regions[2].height;


        oldSandValue = regions[0].height;
        oldGrassValue = regions[1].height;
        oldForestValue = regions[2].height;


    }
    private void FixedUpdate()
    {
        SeedText.GetComponent<Text>().text = "Seed " + seed.ToString();
       // LacunarityText.GetComponent<Text>().text = "Lacunarity " + lacunarity.ToString();
       // PersistanceText.GetComponent<Text>().text = "Persistance " + persistance.ToString();
        ScaleText.GetComponent<Text>().text = "Scale " + noiseScale.ToString();


        if (Input.GetKey(KeyCode.E))
        {
            MainCamera.GetComponent<Camera>().orthographicSize += 10;

        }
        if (Input.GetKey(KeyCode.Q))
        {
            MainCamera.GetComponent<Camera>().orthographicSize -= 10;

        }
        lacunarity = lacunaritySlider.value;
        persistance = persistanceSlider.value;
        noiseScale = noiseScaleSlider.value;
        regions[0].height = Sand.value;
        regions[1].height = Grass.value;
        regions[2].height = Forest.value;

        seed = (int)seedSlider.value*10;

   
        if (lacunarity != oldLacunarityValue || persistance != oldPersistanceValue || noiseScale != oldScaleValue || seed != oldSeedValue || Sand.value != oldSandValue || Grass.value != oldGrassValue || Forest.value != oldForestValue || Input.GetKeyDown(KeyCode.V)) 
        {
            oldLacunarityValue = lacunarity;
            oldPersistanceValue = persistance;
            oldScaleValue = noiseScale;
            oldSeedValue = seed;
            oldForestValue = Forest.value;
            oldSandValue = Sand.value;
            oldGrassValue = Grass.value;

            savedNoiseMap = null;
            savedColorMap = new Color[mapWidth * mapHeight];
            foreach (GameObject chunker in ChunkManager)
            {
                Destroy(chunker);
            }
            foreach(GameObject tree in instantiatedTrees)
            {
                Destroy(tree);
            }
            GenerateMap();
           master.transform.rotation = Quaternion.Euler(0, 0, rotationValue);
            rotationValue += 90;
        }

        if (savedColorMap[Math.Abs((int)(player.transform.position.x / 10)) * mapWidth + Math.Abs((int)(player.transform.position.y / 10))] == Color.blue)
        {player.transform.position = prevPosition; }
        else {prevPosition = player.transform.position;}
    }
 
    
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
   

}
