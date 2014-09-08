using UnityEngine;
using System.Collections;

public class GemGenerator : MonoBehaviour {

    private GameObject[] gems = new GameObject[50];
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
        for (int i = 0; i < gems.Length - 1; i++)
        {
            float randomX, randomY, randomZ;
            SpawnArea spawnArea = (SpawnArea)Random.Range(0, 13);
            Area area;
            switch (spawnArea)
            {
                case SpawnArea.LOWERLEFT:
                    dirt = LOWERLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.LOWERRIGHT:
                    dirt = LOWERRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLELEFT:
                    dirt = MIDDLELEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.MIDDLERIGHT:
                    dirt = MIDDLERIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERLEFT:
                    dirt = UPPERLEFT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
                    randomZ = Random.Range(zMin, zMax);
                    area = Area.Z;
                    break;
                case SpawnArea.UPPERRIGHT:
                    dirt = UPPERRIGHT;
                    randomX = Random.Range(dirt.renderer.bounds.min.x, dirt.renderer.bounds.max.x);
                    randomY = Random.Range(dirt.renderer.bounds.min.y, dirt.renderer.bounds.max.y);
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
                    randomY = Random.Range(yMin, yMax);
                    randomZ = Random.Range(zFarMin, zFarMax);
                    area = Area.XLEFT;
                    break;
                case SpawnArea.UPPERFARRIGHT:
                    dirt = UPPERFARRIGHT;
                    randomX = Random.Range(xMidRight, xMax);
                    randomY = Random.Range(yMin, yMax);
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
            gems[i].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            gems[i].transform.position = new Vector3(randomX, randomY, randomZ);

            randomX = Random.Range(0, 360);
            randomY = Random.Range(0, 360);
            randomZ = Random.Range(0, 360);

            gems[i].transform.rotation = new Quaternion(randomX, randomY, randomZ, 0);
            gems[i].GetComponent<GemBehavior>().SetDirt(dirt, area);
        }
    }
}
