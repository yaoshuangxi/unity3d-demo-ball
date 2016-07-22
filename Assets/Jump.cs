using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{

	float speed = 5.0f;
	float x;
	float y;

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {//鼠标按着左键移动 
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			x = Input.GetAxis ("Mouse X") * Time.deltaTime * speed;               
			y = Input.GetAxis ("Mouse Y") * Time.deltaTime * speed; 
			Debug.Log (x);
			Debug.Log (y);
		} else {
			x = y = 0;
			gameObject.GetComponent<Rigidbody>().useGravity = true;
		}



		//旋转角度（增加）
		transform.Translate (new Vector3 (x, y, 0));
	}
}
