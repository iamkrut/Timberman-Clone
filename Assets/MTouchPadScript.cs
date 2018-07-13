using UnityEngine;
using UnityEngine.EventSystems;

public class MTouchPadScript : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown(PointerEventData data){
		if (data.pointerCurrentRaycast.screenPosition.x < Screen.width / 2) {
			MPlayer1ControllerScript.instance.TapLeft();
		} else if(data.pointerCurrentRaycast.screenPosition.x > Screen.width / 2){
			MPlayer1ControllerScript.instance.TapRight();			
		}
	}
}
