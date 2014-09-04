using UnityEngine;
using System.Collections;

public class GemGenerator : MonoBehaviour {

    private GameObject[] gems = new GameObject[50];

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

    void Start()
    {
        SpawnGems();
    }

    private void SpawnGems()
    {
        for (int i = 0; i < gems.Length - 1; i++)
        {
            float randomX, randomY, randomZ;
            gemType gem;
            randomX = Random.Range(-5.0f, 15.0f);
            randomY = Random.Range(-5.0f, 5.0f);
            randomZ = Random.Range(-5.0f, 15.0f);

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

            gems[i].transform.position = new Vector3(randomX, randomY, randomZ);

            randomX = Random.Range(0, 360);
            randomY = Random.Range(0, 360);
            randomZ = Random.Range(0, 360);

            gems[i].transform.rotation = new Quaternion(randomX, randomY, randomZ, 0);
        }
    }

}
