using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{

	bool mClick = false;
	bool mMove = false;
	float upDist = 0;
	float beginX, beginY;
	float endX, endy;
	float speed = 10.0f;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			beginX = Input.mousePosition.x;
			beginY = Input.mousePosition.y;
		}

		if (Input.GetMouseButton (0)) {//鼠标按着左键移动
			Debug.Log(Input.mousePosition.x - beginX);
			if (Mathf.Abs(Input.mousePosition.x - beginX) > 10 || Mathf.Abs(Input.mousePosition.y - beginY) > 10 ) {
				mMove = true;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (mMove) {
				mMove = false;
				gameObject.GetComponent<Rigidbody> ().useGravity = true;
			} else {
				mClick = true;
			}
		}

		//拖动到某个位置落下
		if (mMove) {
			float x =	Input.GetAxis ("Mouse X") * Time.deltaTime * speed;               
			float y =	Input.GetAxis ("Mouse Y") * Time.deltaTime * speed; 
			transform.Translate (new Vector3 (x, y, 0));
		}

		//点击向上移动后落下
		if (mClick) {
			float translation = 5 * Time.deltaTime;
			transform.Translate (0, translation, 0);
			upDist += translation;
			if (upDist >= 5) {
				mClick = false;
				upDist = 0;
				gameObject.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
	}
}
