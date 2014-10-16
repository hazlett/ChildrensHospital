using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

    private GameObject dirt;
    private bool audioLinked = false;

    private AudioSource audioPlay;
    public GemGenerator.Area area;
    public string Dirt;
    public Vector3 dirtPosition;
	void Start () {

        
	}
	
	void Update () {

        if (GameControl.Instance.IsPlaying)
        {
            if (!audioLinked)
            {
                audioPlay = GameObject.Find("RockFall").GetComponent<AudioSource>();
                audioLinked = true;
            }
        }


        if (this.rigidbody.useGravity == false)
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
        this.rigidbody.useGravity = true;
        if (!audioPlay.isPlaying)
        {
            audioPlay.Play();
        }
    }
}
