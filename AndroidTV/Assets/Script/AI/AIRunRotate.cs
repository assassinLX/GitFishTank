using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRunRotate : MonoBehaviour {

    private Vector3 centre;
    private Vector3 veccentripetalForce;

    public enum rotateDirection
    {
        leftRotation, rightRotation
    }

    public rotateDirection controllerDirection;
    public float radius;
    public float Speed;

	private void Awake()
	{
        centre = new Vector3(transform.position.x,transform.position.y - radius,transform.position.z);
	}


	// Update is called once per frame
	void Update () {
        veccentripetalForce = (centre - transform.position).normalized;
        var vectical = GetVerticalDir(veccentripetalForce);
        transform.position += vectical.normalized * Speed * Time.deltaTime;

    }


    Vector3 GetVerticalDir(Vector3 target){
        if(controllerDirection == rotateDirection.leftRotation){
            return new Vector3(target.y / Mathf.Sqrt(target.x * target.x + target.y * target.y),
                               -target.x / Mathf.Sqrt(target.x * target.x + target.y * target.y), target.z);
        }else{
            return new Vector3(-target.y / Mathf.Sqrt(target.x * target.x + target.y * target.y),
                               target.x / Mathf.Sqrt(target.x * target.x + target.y * target.y), target.z);
        }
    }
     
}
