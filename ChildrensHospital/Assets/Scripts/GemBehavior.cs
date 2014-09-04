using UnityEngine;
using System.Collections;

public class GemBehavior : MonoBehaviour {

    private GameObject mainCamera, dirt;

    private Vector3 mainCameraBehind;

	// Use this for initialization
	void Start () {

        mainCamera = GameObject.Find("Main Camera");
        dirt = GameObject.Find("Cube");

        mainCameraBehind = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 8);
	}
	
	// Update is called once per frame
	void Update () {

        if (dirt.transform.position.z > this.transform.position.z)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, mainCameraBehind, Time.deltaTime * 2);
        }

        if (this.transform.position.z < mainCamera.transform.position.z - 7)
        {
            GameObject.Destroy(this.gameObject);
        }

	}
}
