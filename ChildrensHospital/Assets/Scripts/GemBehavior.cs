using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

    private GameObject dirt;
    private bool audioLinked = false;
    private bool collected = false;
    private AudioSource audioPlay;
    public GemGenerator.Area area;
    public string Dirt;
    public Vector3 dirtPosition;
	void Start () {

        
	}

	void Update () {
        if (!GameControl.Instance.InGame)
            return;
        if (GameControl.Instance.IsPlaying)
        {
            if (!audioLinked)
            {
                audioPlay = GameObject.Find("RockFall").GetComponent<AudioSource>();
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
                case GemGenerator.Area.XRIGHT:
                    if (dirt.transform.position.x > this.transform.position.x)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

     
                    break;
                case GemGenerator.Area.XLEFT:
                    if (dirt.transform.position.x < this.transform.position.x)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    break;
                case GemGenerator.Area.Y:
                    if (dirt.transform.position.y > this.transform.position.y)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }


        
                    break;
                case GemGenerator.Area.Z:
                    if (dirt.transform.position.z > this.transform.position.z)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

       
                    break;
            }

        }
	}
    public void SetDirt(GameObject dirt, GemGenerator.Area area)
    {
        if (dirt == null)
        {
            Destroy(gameObject);
        }
        this.dirt = dirt;
        Dirt = this.dirt.name;
        this.area = area;
    }

    private void GravityOn()
    {
        if (!collected)
        {
            collected = true;
            GameControl.Instance.CollectGem();
            this.rigidbody.useGravity = true;
            rigidbody.AddForce(((Vector3.zero - transform.position).normalized + new Vector3(0,1,0)) * 100.0f);
            //if (!audioPlay.isPlaying)
            //{
            //    audioPlay.Play();
            //}
        }
    }
}
