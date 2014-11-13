using UnityEngine;
using System.Collections;

public class GemGenerator : MonoBehaviour {
    private GameObject[] gems = new GameObject[250];
    private GameObject[] assistedGems;
    private float spawnDistance = 0.025f;
    private float xMax = 2.5f, xMidLeft = -1.0f, xMidRight = 1.0f, xMin = -2.5f, zMin = 0.5f, zMax = 5.0f, yMin = 1.0f, yMax = 3.0f, zFarMin = -1.0f, zFarMax = 0.55f, zTop = 0.1f, upperY = 1.0f,
        frontLeftMin = -2.5f, frontLeftMax = 0.0f, frontRightMin = 0.0f, frontRightMax = 2.5f;
    private GameObject dirt,
        LOWERLEFT,
        LOWERRIGHT,
        MIDDLELEFT,
        MIDDLERIGHT,
        UPPERLEFT,
        UPPERRIGHT,
        TOPLEFT,
        TOPRIGHT,
        LOWERFARLEFT,
        LOWERFARRIGHT,
        MIDDLEFARLEFT,
        MIDDLEFARRIGHT,
        UPPERFARLEFT,
        UPPERFARRIGHT;

    enum gemType
    {
        DIAMOND,
        RUBY,
        EMERALD,
        SAPPHIRE,
        AMETHYST,
        TOPAZ,
        PERIDOT
    }
    enum SpawnArea
    { 
        TOPLEFT,
        TOPRIGHT,
        LOWERLEFT,
        LOWERRIGHT,        
        LOWERFARLEFT,
        LOWERFARRIGHT,
        MIDDLELEFT,
        MIDDLERIGHT,
        MIDDLEFARLEFT,
        MIDDLEFARRIGHT, 
        UPPERLEFT,
        UPPERRIGHT,
        UPPERFARLEFT,
        UPPERFARRIGHT
    }
    public enum Area
    {
        XRIGHT,
        XLEFT,
        Y,
        Z
    }

    void Start()
    {
        if (GameControl.Instance.Gems)
        {
            assistedGems = new GameObject[(int)((6 / spawnDistance) * (zMax - zMin))];
            GameControl.Instance.TotalGems = assistedGems.Length + gems.Length;
            Debug.Log(GameControl.Instance.TotalGems);
            InstantiateGems();
            InstatiateAssitedGems();
            this.enabled = false;
            SetupGems();
        }
    }

    void Awake()
    {

    }

    public void SetupGems()
    {
        SetAreas();
        SpawnGems();
    }

    private void SetAreas()
    {
        LOWERLEFT = GameObject.Find("LowerLeft");
        LOWERRIGHT = GameObject.Find("LowerRight");
        MIDDLELEFT = GameObject.Find("MiddleLeft");
        MIDDLERIGHT = GameObject.Find("MiddleRight");
        UPPERLEFT = GameObject.Find("UpperLeft");
        UPPERRIGHT = GameObject.Find("UpperRight");
        TOPLEFT = GameObject.Find("TopLeft");
        TOPRIGHT = GameObject.Find("TopRight");
        LOWERFARLEFT = GameObject.Find("LowerFarLeft");
        LOWERFARRIGHT = GameObject.Find("LowerFarRight");
        MIDDLEFARLEFT = GameObject.Find("MiddleFarLeft");
        MIDDLEFARRIGHT = GameObject.Find("MiddleFarRight");
        UPPERFARLEFT = GameObject.Find("UpperFarLeft");
        UPPERFARRIGHT = GameObject.Find("UpperFarRight");
    }

    private void SpawnGems()
    {
        AssistedSpawn();
        RandomSpawn();
    }

    private void AssistedSpawn()
    {
        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < (zMax - zMin) / spawnDistance; j++)
            {
                float randomX, randomY, randomZ;

                Area area;
                switch (i)
                {
                    case 0:
                        dirt = LOWERLEFT;
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    case 1:
                        dirt = LOWERRIGHT;
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    case 2:
                        dirt = MIDDLELEFT;
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    case 3:
                        dirt = MIDDLERIGHT;
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    case 4:
                        dirt = UPPERLEFT;
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    case 5:
                        dirt = UPPERRIGHT;
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = zMin + (j * spawnDistance);
                        area = Area.Z;
                        break;
                    default:
                        dirt = null;
                        randomX = 0.0f;
                        randomY = 0.0f;
                        randomZ = 0.0f;
                        area = 0;
                        break;

                }
                if (index < assistedGems.Length - 1)
                {
                    assistedGems[index].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                    assistedGems[index].transform.position = new Vector3(randomX, randomY, randomZ);

                    randomX = Random.Range(0, 360);
                    randomY = Random.Range(0, 360);
                    randomZ = Random.Range(0, 360);

                    assistedGems[index].transform.rotation = new Quaternion(randomX, randomY, randomZ, 0);
                    assistedGems[index].GetComponent<GemBehavior>().SetDirt(dirt, area);
                }
                index++;
            }
        }
    }
    private SpawnArea WeightedSpawn()
    {
        SpawnArea spawnArea;
        float weight = Random.Range(0.0f, 1.0f);
        if (weight <= .1) //top
        {
            spawnArea = (SpawnArea)Random.Range((int)SpawnArea.TOPLEFT, (int)SpawnArea.TOPRIGHT + 1);
        }
        else if (weight < .25) //lower
        {
            spawnArea = (SpawnArea)Random.Range((int)SpawnArea.LOWERLEFT, (int)SpawnArea.LOWERFARRIGHT + 1);
        }
        else if (weight < .5) //middle
        {
            spawnArea = (SpawnArea)Random.Range((int)SpawnArea.MIDDLELEFT, (int)SpawnArea.MIDDLEFARRIGHT + 1);
        }
        else //upper
        {
            spawnArea = (SpawnArea)Random.Range((int)SpawnArea.UPPERLEFT, (int)SpawnArea.UPPERFARRIGHT + 1);
        }
        return spawnArea;
    }
    private void RandomSpawn()
    {
        for (int i = 0; i < gems.Length - 1; i++)
        {
            float randomX, randomY, randomZ;
            SpawnArea spawnArea;
           
            //spawnArea = (SpawnArea)Random.Range(0, 14); 
            spawnArea = WeightedSpawn();

            Area area;
            switch (spawnArea)
            {
                case SpawnArea.LOWERLEFT:
                    dirt = LOWERLEFT;
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.LOWERRIGHT:
                    dirt = LOWERRIGHT;
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLELEFT:
                    dirt = MIDDLELEFT;
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLERIGHT:
                    dirt = MIDDLERIGHT;
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERLEFT:
                    dirt = UPPERLEFT;
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERRIGHT:
                    dirt = UPPERRIGHT;
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.TOPLEFT:
                    dirt = TOPLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    area = Area.Y;
                    break;
                case SpawnArea.TOPRIGHT:
                    dirt = TOPRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    area = Area.Y;
                    break;
                case SpawnArea.LOWERFARLEFT:
                    dirt = LOWERFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.LOWERFARRIGHT:
                    dirt = LOWERFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.MIDDLEFARLEFT:
                    dirt = MIDDLEFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.MIDDLEFARRIGHT:
                    dirt = MIDDLEFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.UPPERFARLEFT:
                    dirt = UPPERFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.UPPERFARRIGHT:
                    dirt = UPPERFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                default:
                    dirt = null;
                    randomX = 0.0f;
                    randomY = 0.0f;
                    randomZ = 0.0f;
                    area = 0;
                    break;
            }

            gems[i].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            gems[i].transform.position = new Vector3(randomX, randomY, randomZ);

            randomX = Random.Range(0, 360);
            randomY = Random.Range(0, 360);
            randomZ = Random.Range(0, 360);

            gems[i].transform.rotation = new Quaternion(randomX, randomY, randomZ, 0);
            gems[i].GetComponent<GemBehavior>().SetDirt(dirt, area);
        }
    }
    private void InstantiateGems()
    {
        for (int i = 0; i < gems.Length - 1; i++)
        {
            gemType gem;
            gem = (gemType)Random.Range(0, 7);

            switch (gem)
            {
                case gemType.AMETHYST: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Amethyst"));
                    break;
                case gemType.DIAMOND: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Diamond"));
                    break;
                case gemType.EMERALD: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Emerald"));
                    break;
                case gemType.PERIDOT: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Peridot"));
                    break;
                case gemType.RUBY: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Ruby"));
                    break;
                case gemType.SAPPHIRE: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Sapphire"));
                    break;
                case gemType.TOPAZ: gems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Topaz"));
                    break;
            }
            gems[i].transform.parent = this.gameObject.transform;
        }
    }
    private void InstatiateAssitedGems()
    {
        for (int i = 0; i < assistedGems.Length - 1; i++)
        {
            gemType gem;
            gem = (gemType)Random.Range(0, 7);

            switch (gem)
            {
                case gemType.AMETHYST: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Amethyst"));
                    break;
                case gemType.DIAMOND: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Diamond"));
                    break;
                case gemType.EMERALD: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Emerald"));
                    break;
                case gemType.PERIDOT: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Peridot"));
                    break;
                case gemType.RUBY: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Ruby"));
                    break;
                case gemType.SAPPHIRE: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Sapphire"));
                    break;
                case gemType.TOPAZ: assistedGems[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Topaz"));
                    break;
            }
            assistedGems[i].transform.parent = this.gameObject.transform;
        }
    }
}
