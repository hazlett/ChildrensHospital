using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

    private GameObject mainCamera, dirt;
    private Vector3 mainCameraBehind;
    private GemGenerator.Area area;

	void Start () {

        mainCamera = GameObject.Find("Main Camera");
        mainCameraBehind = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 2);

        dirt = GameObject.Find("Cube");
        mainCameraBehind = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 8);
	}
	
	void Update () {
        if (dirt == null)
            return;
        switch (area)
        {
            case GemGenerator.Area.XRIGHT:
                if (dirt.transform.position.x > this.transform.position.x)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                }

                if (this.transform.position.z < mainCamera.transform.position.z - 1)
                {
                    GameObject.Destroy(this.gameObject);
                }
                break;
            case GemGenerator.Area.XLEFT:
                if (dirt.transform.position.x < this.transform.position.x)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                }

                if (this.transform.position.z < mainCamera.transform.position.z - 1)
                {
                    GameObject.Destroy(this.gameObject);
                }
                break;
            case GemGenerator.Area.Y:
                if (dirt.transform.position.y > this.transform.position.y)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
                }

                if (this.transform.position.z < mainCamera.transform.position.z - 1)
                {
                    GameObject.Destroy(this.gameObject);
                }
                break;
            case GemGenerator.Area.Z:
                if (dirt.transform.position.z > this.transform.position.z)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
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
    public void SetDirt(GameObject dirt, GemGenerator.Area area)
    {
        if (dirt == null)
        {
            Destroy(gameObject);
        }
        this.dirt = dirt;
        this.area = area;
    }
}
