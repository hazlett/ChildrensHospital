using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

    private GameObject mainCamera, dirt;
    private Vector3 mainCameraBehind;

    public AudioSource audioPlay;
    public GemGenerator.Area area;
    public string Dirt;
    public Vector3 dirtPosition;
	void Start () {

        mainCamera = GameObject.Find("Main Camera");
        mainCameraBehind = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 2);

	}
	
	void Update () {
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

                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case GemGenerator.Area.XLEFT:
                    if (dirt.transform.position.x < this.transform.position.x)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }

                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case GemGenerator.Area.Y:
                    if (dirt.transform.position.y > this.transform.position.y)
                    {
                        GravityOn();
                        //this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                    }


                    if (this.transform.position.z < mainCamera.transform.position.z - 1)
                    {
                        GameObject.Destroy(this.gameObject);
                    }
                    break;
                case GemGenerator.Area.Z:
                    if (dirt.transform.position.z > this.transform.position.z)
                    {
                        GravityOn();
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
        audioPlay.Play();
        GameControl.Instance.score += 50;
    }
}
