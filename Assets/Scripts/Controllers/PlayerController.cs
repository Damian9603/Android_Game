using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    Camera cam;
    public LayerMask movementMask;
    public Interactable focus;
    PlayerMotor motor;
    public CameraController cameraController;
    private float holdTime = 1f; 
    private float acumTime = 0f;
    // Use this for initialization
    void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if(IsPointerOverUIObject() )
        {
            return;
        }
        else if (IsPointerOverGameObject())
        {
            return;
        }
       /*if(Input.touchCount==1)
        {
            acumTime += Input.GetTouch(0).deltaTime;
            if(acumTime >= holdTime)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }

            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended && acumTime < holdTime)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
                acumTime = 0;
                
            }
        }
        else
        {
            acumTime = 0;
        }*/

       if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
            if(Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                    Vector3 stop = transform.position;
                    motor.MoveToPoint(stop);
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
        if(newFocus!= focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }           
            focus = newFocus;
            if(newFocus.transform.tag!="Enemy")
            {
                Follow();
                //motor.FollowTarget(newFocus);
            }

        }
        
        newFocus.OnFocused(transform);
        
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }

    public void Follow()
    {
        if(focus!=null)
        {
            motor.FollowTarget(focus);
        }
        
    }

    public void Follow(float stopDistance = .8f)
    {
        if (focus != null)
        {
            motor.FollowTarget(focus, stopDistance);
        }

    }
}
