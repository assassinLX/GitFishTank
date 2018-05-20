using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AINormal : MonoBehaviour {
    public GameObject[] Target;

    public int index;
    public float Speed;

	private void Awake()
	{
        index = 0;
	}

	private void Update()
	{
        if(Vector3.Distance(transform.position,Target[index % Target.Length].transform.position) < 1.0f){
            index++;
            if(index > 100){
                index = 0;
            }
        }else{
            var currentVec = Target[index % Target.Length].transform.position - transform.position;
            var normalizedVec = currentVec.normalized;
            transform.position += normalizedVec * Speed * Time.deltaTime;
            transform.DOLookAt(Target[index % Target.Length].transform.position, 2.0f);
        }
	}
}
