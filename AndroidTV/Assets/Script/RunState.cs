using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunState : MonoBehaviour
{

    public float RadiiValue;
    public int chooseRadiiNumber;

    [SerializeField]
    private int index = 0;

    public enum State
    {
        Idle, Eat
    }

    [SerializeField]
    private State state;
    public State CurrentState
    {
        set{
            state = value;
        }
        get{
            return state;
        }
    }

    public Vector3[] TargePositions;

	private void Awake()
	{
        RadiiValue = 5.0f;
        chooseRadiiNumber = 4;
        CurrentState = State.Idle;
        TargePositions = new Vector3[chooseRadiiNumber];
        calTargePoint();
	}

	private void Start()
	{
        ExecuteState();
	}

    private void opinionState(){
        if (getFood() != null){
            CurrentState = State.Eat;
        }else{
            CurrentState = State.Idle;
        }
    }

	private void ExecuteState()
	{
        if(CurrentState == State.Eat){
            Debug.Log("Eat");
        }else{
            Debug.Log("Idle");
            StartCoroutine(Idle());
        }
	}

    private void callBack(){
        opinionState();
        ExecuteState();
    }

   
    private IEnumerator Idle()
    {
        yield return new WaitForSeconds(0.2f);
       
        transform.DOMove(TargePositions[index % chooseRadiiNumber],7.0f);
        transform.DOLookAt(TargePositions[index % chooseRadiiNumber], 4.0f);
  
        yield return new WaitForSeconds(7.0f);
        index++;

        if (index % chooseRadiiNumber * 4 == 0 && index > 0) {
            index = 0;
            calTargePoint();
        }
        yield return new WaitForSeconds(0.3f);
        callBack();
    }




    private void calTargePoint()
    {
        ClearCube();
        for (int i = 0; i < TargePositions.Length; i++)
        {
            float x = Random.RandomRange(-RadiiValue,RadiiValue);
            float y = Mathf.Sqrt(RadiiValue * RadiiValue - x * x);
            TargePositions[i] = new Vector3(transform.localPosition.x+x, -1.9f, transform.localPosition.z+y);
            CreateCube(TargePositions[i]);
        }
    }


    private void CreateCube(Vector3 targePoit){
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = targePoit;
        cube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        cube.tag = "Cube";
    }

    private void ClearCube(){
        var cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach(var a in cubes){
            Destroy(a);
        }
    }

    private GameObject getFood(){
        int layerMark = LayerMask.NameToLayer("Food");
        Collider[] food = Physics.OverlapSphere(transform.position,2.0f,layerMark);
        if(food.Length > 0){
            return food[0].gameObject;
        }else{
            return null;
        }
    }

    private void setAnimator(){
        
    }

}
