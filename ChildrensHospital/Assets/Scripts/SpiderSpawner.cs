using UnityEngine;
using System.Collections;

public class SpiderSpawner : MonoBehaviour {

    private GameObject[] spiders = new GameObject[50];
    private float xMax = 2.5f, xMidLeft = 0.0f, xMidRight = 0.0f, xMin = -2.5f, zMin = 0.0f, zMax = 10.0f, yMin = 1.0f, yMax = 3.0f, zFarMin = 0.0f, zFarMax = 10.0f, zTop = 0.1f;
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
        this.enabled = false;
    }

    void OnEnable()
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
        for (int i = 0; i < spiders.Length - 1; i++)
        {
            float randomX, randomY, randomZ, rotX, rotY, rotZ;
            SpawnArea spawnArea = (SpawnArea)Random.Range(0, 13);

            Area area;
            switch (spawnArea)
            {
                case SpawnArea.LOWERLEFT:
                    dirt = LOWERLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.LOWERRIGHT:
                    dirt = LOWERRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLELEFT:
                    dirt = MIDDLELEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLERIGHT:
                    dirt = MIDDLERIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERLEFT:
                    dirt = UPPERLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERRIGHT:
                    dirt = UPPERRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    rotX = 270;
                    rotY = 0;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.Z;
                    break;
                case SpawnArea.TOPLEFT:
                    dirt = TOPLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    rotX = 180;
                    rotY = Random.Range(0.0f, 360.0f);
                    rotZ = 0;
                    area = Area.Y;
                    break;
                case SpawnArea.TOPRIGHT:
                    dirt = TOPRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zTop);
                    rotX = 180;
                    rotY = Random.Range(0.0f, 360.0f);
                    rotZ = 0;
                    area = Area.Y;
                    break;
                case SpawnArea.LOWERFARLEFT:
                    dirt = LOWERFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.LOWERFARRIGHT:
                    dirt = LOWERFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.MIDDLEFARLEFT:
                    dirt = MIDDLEFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.MIDDLEFARRIGHT:
                    dirt = MIDDLEFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XRIGHT;
                    break;
                case SpawnArea.UPPERFARLEFT:
                    dirt = UPPERFARLEFT;
                    randomX = Random.Range(xMin, xMidLeft);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 270;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.UPPERFARRIGHT:
                    dirt = UPPERFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    rotX = 270;
                    rotY = 90;
                    rotZ = Random.Range(0.0f, 360.0f);
                    area = Area.XRIGHT;
                    break;
                default:
                    dirt = null;
                    randomX = randomY = randomZ = rotX = rotY = rotZ = 0.0f;
                    area = 0;
                    break;
            }

            spiders[i] = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/SPIDER"));                 
            spiders[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            spiders[i].transform.position = new Vector3(randomX, randomY, randomZ);

            //randomX = Random.Range(0, 360);
            //randomY = Random.Range(0, 360);
            //randomZ = Random.Range(0, 360);

            spiders[i].transform.rotation = new Quaternion(rotX, rotY, rotZ, 0);
            spiders[i].GetComponent<SpiderBehavior>().SetDirt(dirt, area);
        }
    }
}
