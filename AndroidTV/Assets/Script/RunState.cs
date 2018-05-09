using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        RadiiValue = 3.0f;
        chooseRadiiNumber = 10;
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

        while(Vector3.Distance(TargePositions[index % chooseRadiiNumber],transform.position) > 1.0f){
            yield return new WaitForSeconds(0.1f);
            move(TargePositions[index % chooseRadiiNumber]);   
        }
        index++;
        if(index >= 1000000000){
            index = 0;
        }
        yield return new WaitForSeconds(1.0f);
        callBack();
    }


    private void move(Vector3 targePoint){
        Debug.Log(targePoint);
        var currentVec = targePoint - transform.position;
        //Quaternion rotate = Quaternion.LookRotation(currentVec.normalized);
        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //rotate, 1.0f * Time.deltaTime);
        transform.position += currentVec.normalized * 1 * Time.deltaTime;
    }

    private void calTargePoint()
    {
        for (int i = 0; i < TargePositions.Length; i++)
        {
            float x = Random.RandomRange(-RadiiValue, RadiiValue);
            float y = Mathf.Sqrt(RadiiValue * RadiiValue - x * x);
            TargePositions[i] = new Vector3(transform.position.x+x, 0, transform.position.z+y);
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(transform.position.x + x, 0, transform.position.z + y);
            cube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
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
