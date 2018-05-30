using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class VirtualJoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler 
{
	private Image bgImg;
	private Image joyStickImg;
	public Vector3 inputVector,inputVector2;

	private void Start()
	{
		bgImg = GetComponent<Image> ();
		joyStickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);

	}
	public virtual void OnPointerUp(PointerEventData ped)
	{
		if (!inputVector.Equals (Vector3.zero)) {
			
			inputVector2 = inputVector;
		}


		inputVector = Vector3.zero;

		joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
	}
	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos=Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position,
			    ped.pressEventCamera, out pos))
		{
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
			inputVector = new Vector3 (pos.x * 2 - 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;
			//Debug.Log (pos);
			//move joystick img
			joyStickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
					inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));
			//Debug.Log (inputVector);
		}


	}
	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
			//return inputVector2.x;

	}
	public float Vertical()
	{
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis ("Vertical");
		//return inputVector2.z;

	}



}
