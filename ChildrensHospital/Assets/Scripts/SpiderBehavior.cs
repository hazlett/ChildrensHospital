using UnityEngine;
using System.Collections;

public class SpiderBehavior : MonoBehaviour {


    private GameObject dirt;
    private bool audioLinked = false;
    private bool collected = false;
    private AudioSource audioPlay;
    public SpiderSpawner.Area area;
    public string Dirt;
    public Vector3 dirtPosition;
    public Animation spiderAnimation;

    private float angle, speed, radius;
    private int around;

    void Start()
    {
        Random.seed = Random.Range(-1000, 1000);
        around = Random.Range(-1, 1);
        if (around == 0) { around = 1; }
        radius = Random.Range(1, 5);
        spiderAnimation.wrapMode = WrapMode.Loop;
        spiderAnimation["Walk"].speed = 2.0f;
    }

    void Update()
    {
        MoveSpider();
        if (!GameControl.Instance.InGame)
            return;
        if (GameControl.Instance.IsPlaying)
        {
            if (!audioLinked)
            {
                audioPlay = GameObject.Find("SpiderSquish").GetComponent<AudioSource>();
                audioLinked = true;
            }
        }


        if (!collected)
        {
            if (dirt == null)
                return;
            dirtPosition = dirt.transform.position;
            switch (area)
            {
                case SpiderSpawner.Area.XRIGHT:
                    if (dirt.transform.position.x > this.transform.position.x)
                    {
                        Invoke("KillSpider", 0.25f);
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }


                    break;
                case SpiderSpawner.Area.XLEFT:
                    if (dirt.transform.position.x < this.transform.position.x)
                    {
                        Invoke("KillSpider", 0.25f);
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    break;
                case SpiderSpawner.Area.Y:
                    if (dirt.transform.position.y > this.transform.position.y)
                    {
                        Invoke("KillSpider", 0.25f);
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }



                    break;
                case SpiderSpawner.Area.Z:
                    if (dirt.transform.position.z > this.transform.position.z)
                    {
                        Invoke("KillSpider", 0.25f);
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }


                    break;
            }

        }
      
    }

    public void MoveSpider()
    {
        this.transform.Rotate(Vector3.forward, around);
        this.transform.position += this.transform.up * 0.001f * radius;
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

        GameControl.Instance.CollectGem();

        if (!audioPlay.isPlaying)
        {
            audioPlay.Play();
        }
        
        GameObject.Destroy(this.gameObject);
    }
}
