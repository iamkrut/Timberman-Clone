using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPadScript : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown(PointerEventData data){
		if (data.pointerCurrentRaycast.screenPosition.x < Screen.width / 2) {
			PlayerControllerScript.instance.TapLeft();
		} else if(data.pointerCurrentRaycast.screenPosition.x > Screen.width / 2){
			PlayerControllerScript.instance.TapRight();			
		}
	}
}
