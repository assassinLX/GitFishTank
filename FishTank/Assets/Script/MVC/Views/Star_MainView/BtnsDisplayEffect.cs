using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 需要 EventTrigger 脚本的支援
[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class BtnsDisplayEffect : MonoBehaviour {

    public GameObject MainCamera;
    public Button[] buttons;

	private void Start()
	{
        buttons = transform.GetComponentsInChildren<Button>();
        foreach (Button btn in buttons){
            EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(OnMouseDown);
            trigger.triggers.Add(entry);
        }
	}

    private void OnMouseDown(BaseEventData pointData)
	{
        var waterEffect = MainCamera.transform.GetComponent<WaterWaveEffect>();
        waterEffect.waveStartTime = Time.time;
	}
}
