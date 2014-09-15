using UnityEngine;
using System.Collections;

public class SpiderBehavior : MonoBehaviour {

    private GameObject mainCamera, dirt;
    private Vector3 mainCameraBehind;

    public SpiderSpawner.Area area;
    public string Dirt;
    public Vector3 dirtPosition;
    public Animation spiderAnimation;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        mainCameraBehind = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 2);
        spiderAnimation.wrapMode = WrapMode.Loop;
    }

    void Update()
    {
            if (dirt == null)
                return;
            dirtPosition = dirt.transform.position;
            switch (area)
            {
                case SpiderSpawner.Area.XRIGHT:
                    if (dirt.transform.position.x > this.transform.position.x + 0.1f)
                    {
                        KillSpider();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case SpiderSpawner.Area.XLEFT:
                    if (dirt.transform.position.x < this.transform.position.x - 0.1f)
                    {
                        KillSpider();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case SpiderSpawner.Area.Y:
                    if (dirt.transform.position.y > this.transform.position.y + 0.3f)
                    {
                        KillSpider();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }


                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case SpiderSpawner.Area.Z:
                    if (dirt.transform.position.z > this.transform.position.z + 0.1f)
                    {
                        KillSpider();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
            }
            if (this.transform.position.z < mainCamera.transform.position.z - 7)
            {
                GameObject.Destroy(this.gameObject);
            }
      
    }
    public void SetDirt(GameObject dirt, SpiderSpawner.Area area)
    {
        if (dirt == null)
        {
            Destroy(gameObject);
        }
        this.dirt = dirt;
        Dirt = this.dirt.name;
        this.area = area;
    }

    private void KillSpider()
    {
        GameObject particles = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/SpiderBlood"));
        particles.transform.position = this.transform.position;
        
        GameObject.Destroy(this.gameObject);
    }
}
