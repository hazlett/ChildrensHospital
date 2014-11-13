using UnityEngine;
using System.Collections;

public class SpiderSpawner : MonoBehaviour {

     private GameObject[] spiders = new GameObject[250];
    private GameObject[] assistedSpiders;
    private float spawnDistance = 0.075f;
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
        if (!GameControl.Instance.Gems)
        {
            assistedSpiders = new GameObject[(int)((6 / spawnDistance) * (zMax - zMin))];
            GameControl.Instance.TotalGems = assistedSpiders.Length + spiders.Length;
            Debug.Log(GameControl.Instance.TotalGems);
            InstantiateSpiders();
            InstatiateAssitedSpiders();
            this.enabled = false;
            SetupSpiders();
        }
    }

    void Awake()
    {

    }

    public void SetupSpiders()
    {
        SetAreas();
        SpawnSpiders();
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

    private void SpawnSpiders()
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
                float randomX, randomY, randomZ, rotX, rotY, rotZ;
                SpawnArea spawnArea;

                //spawnArea = (SpawnArea)Random.Range(0, 14); 
                spawnArea = WeightedSpawn();

                Area area;
                switch (spawnArea)
                {
                    case SpawnArea.LOWERLEFT:
                        dirt = LOWERLEFT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.LOWERRIGHT:
                        dirt = LOWERRIGHT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.MIDDLELEFT:
                        dirt = MIDDLELEFT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.MIDDLERIGHT:
                        dirt = MIDDLERIGHT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.UPPERLEFT:
                        dirt = UPPERLEFT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontLeftMin, frontLeftMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.UPPERRIGHT:
                        dirt = UPPERRIGHT;
                        rotX = 0;
                        rotY = 0;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(frontRightMin, frontRightMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = Random.Range(zMin, zMax);
                        area = Area.Z;
                        break;
                    case SpawnArea.TOPLEFT:
                        dirt = TOPLEFT;
                        rotX = 270;
                        rotY = Random.Range(0.0f, 359.9f);
                        rotZ = 0;
                        randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                        randomY = Random.Range(yMin, yMax);
                        randomZ = Random.Range(zFarMin, zTop);
                        area = Area.Y;
                        break;
                    case SpawnArea.TOPRIGHT:
                        dirt = TOPRIGHT;
                        rotX = 270;
                        rotY = Random.Range(0.0f, 359.9f);
                        rotZ = 0;
                        randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                        randomY = Random.Range(yMin, yMax);
                        randomZ = Random.Range(zFarMin, zTop);
                        area = Area.Y;
                        break;
                    case SpawnArea.LOWERFARLEFT:
                        dirt = LOWERFARLEFT;
                        rotX = 0;
                        rotY = 270;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMin, xMidLeft);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XLEFT;
                        break;
                    case SpawnArea.LOWERFARRIGHT:
                        dirt = LOWERFARRIGHT;
                        rotX = 0;
                        rotY = 90;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMidRight, xMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XRIGHT;
                        break;
                    case SpawnArea.MIDDLEFARLEFT:
                        dirt = MIDDLEFARLEFT;
                        rotX = 0;
                        rotY = 270;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMin, xMidLeft);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XLEFT;
                        break;
                    case SpawnArea.MIDDLEFARRIGHT:
                        dirt = MIDDLEFARRIGHT;
                        rotX = 0;
                        rotY = 90;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMidRight, xMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XRIGHT;
                        break;
                    case SpawnArea.UPPERFARLEFT:
                        dirt = UPPERFARLEFT;
                        rotX = 0;
                        rotY = 270;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMin, xMidLeft);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XLEFT;
                        break;
                    case SpawnArea.UPPERFARRIGHT:
                        dirt = UPPERFARRIGHT;
                        rotX = 0;
                        rotY = 90;
                        rotZ = Random.Range(0.0f, 359.9f);
                        randomX = Random.Range(xMidRight, xMax);
                        randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                        randomZ = Random.Range(zFarMin, zFarMax);
                        area = Area.XRIGHT;
                        break;
                    default:
                        dirt = null;
                        rotX = rotY = rotZ = randomX = randomY = randomZ = 0.0f;
                        area = 0;
                        break;
                }

                if (index < assistedSpiders.Length - 1)
                {


                    assistedSpiders[index].transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    assistedSpiders[index].transform.position = new Vector3(randomX, randomY, randomZ);


                    Quaternion newRotation = new Quaternion();
                    newRotation.eulerAngles = new Vector3(rotX, rotY, rotZ);

                    assistedSpiders[index].transform.rotation = newRotation;
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
        for (int i = 0; i < spiders.Length - 1; i++)
        {
            float randomX, randomY, randomZ, rotX, rotY, rotZ;
            SpawnArea spawnArea;
           
            //spawnArea = (SpawnArea)Random.Range(0, 14); 
            spawnArea = WeightedSpawn();

            Area area;
            switch (spawnArea)
            {
                case SpawnArea.LOWERLEFT:
                    dirt = LOWERLEFT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.LOWERRIGHT:
                    dirt = LOWERRIGHT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLELEFT:
                    dirt = MIDDLELEFT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLERIGHT:
                    dirt = MIDDLERIGHT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERLEFT:
                    dirt = UPPERLEFT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontLeftMin, frontLeftMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERRIGHT:
                    dirt = UPPERRIGHT;
                    rotX = 0;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(frontRightMin, frontRightMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.TOPLEFT:
                    dirt = TOPLEFT;
                    rotX = 270;
                    rotY = Random.Range(0.0f, 359.9f);
                    rotZ = 0;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    area = Area.Y;
                    break;
                case SpawnArea.TOPRIGHT:
                    dirt = TOPRIGHT;
                    rotX = 270;
                    rotY = Random.Range(0.0f, 359.9f);
                    rotZ = 0;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    area = Area.Y;
                    break;
                case SpawnArea.LOWERFARLEFT:
                    dirt = LOWERFARLEFT;
                    rotX = 0;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.LOWERFARRIGHT:
                    dirt = LOWERFARRIGHT;
                    rotX = 0;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.MIDDLEFARLEFT:
                    dirt = MIDDLEFARLEFT;
                    rotX = 0;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.MIDDLEFARRIGHT:
                    dirt = MIDDLEFARRIGHT;
                    rotX = 0;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.UPPERFARLEFT:
                    dirt = UPPERFARLEFT;
                    rotX = 0;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.UPPERFARRIGHT:
                    dirt = UPPERFARRIGHT;
                    rotX = 0;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 359.9f);
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.min.y + upperY);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XRIGHT;
                    break;
                default:
                    dirt = null;
                    rotX = rotY = rotZ = randomX = randomY = randomZ = 0.0f;
                    area = 0;
                    break;
            }

            spiders[i].transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            spiders[i].transform.position = new Vector3(randomX, randomY, randomZ);

            Quaternion newRotation = new Quaternion();
            newRotation.eulerAngles = new Vector3(rotX, rotY, rotZ);

            spiders[i].transform.rotation = newRotation;
            spiders[i].GetComponent<SpiderBehavior>().SetDirt(dirt, area);
        }
    }
    private void InstantiateSpiders()
    {
        for (int i = 0; i < spiders.Length - 1; i++)
        {
            spiders[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Spider"));
                 
            spiders[i].transform.parent = this.gameObject.transform;
        }
    }
    private void InstatiateAssitedSpiders()
    {
        for (int i = 0; i < assistedSpiders.Length - 1; i++)
        {

            assistedSpiders[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Spider"));

            assistedSpiders[i].transform.parent = this.gameObject.transform;
        }
    }
}