using UnityEngine;
using System.Collections;

public class MoveCtrl : MonoBehaviour 
{	
	void Update () 
	{
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);	
	}
}
