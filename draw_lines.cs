using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw_lines : MonoBehaviour
{
    private Vector2 fingerUp;
    private Vector2 fingerDown;
    private Vector2 direction;
    private bool spawnAble;

    public GameObject temp;
    Camera camera;

    public List<GameObject> lines = new List<GameObject>(); // All lines are stored in this list 



    private bool spawnCollider;

    Vector3 camOffset = new Vector3(0, 0, 10);

    // Update is called once per frame

    private void Start()
    {
        camera = Camera.main;


    }

    void Update()
    {

      
/* For mouse only */

        
            if ( Input.GetMouseButtonDown(0))
            {
                temp = new GameObject("line");
                
                temp.AddComponent<LineRenderer>();
                temp.layer = LayerMask.NameToLayer("lines");
          

                lr = temp.GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetWidth(0.2f, 0.2f);
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.SetColors(Color.black, Color.black);

                temp.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                fingerUp = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;

                lr.useWorldSpace = true;
                spawnCollider = false;




            }

            if ( Input.GetMouseButton(0))
            {
                fingerDown = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            if ((fingerDown - fingerUp).magnitude > 0.1f){
                lr.SetPosition(0, fingerUp);
                lr.SetPosition(1, fingerDown);
                spawnCollider = true;
            }




            }

            if ( Input.GetMouseButtonUp(0))
            {
                if (spawnCollider == true)
                {
                    BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
                    col.transform.parent = temp.transform;
                    float lineLength = Vector2.Distance(fingerUp, fingerDown);
                    col.size = new Vector2(lineLength, 0.2f);
                    Vector2 midPoint = (fingerUp + fingerDown) / 2;
                    col.transform.position = midPoint;
                    float angle = (Mathf.Abs(fingerDown.y - fingerUp.y) / Mathf.Abs(fingerDown.x - fingerUp.x));
                    if ((fingerUp.y < fingerDown.y && fingerUp.x > fingerDown.x) || (fingerDown.y < fingerUp.y && fingerDown.x > fingerUp.x))
                    {
                        angle *= -1;
                    }
                    angle = Mathf.Rad2Deg * Mathf.Atan(angle);
                    col.transform.Rotate(0, 0, angle);
                lines.Add(temp);

            }

            }

    
    /* For mouse only */


        
   /* For touch only */ 

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                temp = new GameObject("line");

                temp.AddComponent<LineRenderer>();
                temp.layer = LayerMask.NameToLayer("lines");


                lr = temp.GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetWidth(0.2f, 0.2f);
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.SetColors(Color.black, Color.black);

                temp.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                fingerUp = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;

                lr.useWorldSpace = true;
                spawnCollider = false;




            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved || Input.GetMouseButton(0))
            {

                fingerDown = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                if ((fingerDown - fingerUp).magnitude > 0.5f)
                {
                    lr.SetPosition(0, fingerUp);
                    lr.SetPosition(1, fingerDown);
                    spawnCollider = true;
                }




            }

            if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                if (spawnCollider == true)
                {
                    BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
                    col.transform.parent = temp.transform;
                    float lineLength = Vector2.Distance(fingerUp, fingerDown);
                    col.size = new Vector2(lineLength, 0.2f);
                    Vector2 midPoint = (fingerUp + fingerDown) / 2;
                    col.transform.position = midPoint;
                    float angle = (Mathf.Abs(fingerDown.y - fingerUp.y) / Mathf.Abs(fingerDown.x - fingerUp.x));
                    if ((fingerUp.y < fingerDown.y && fingerUp.x > fingerDown.x) || (fingerDown.y < fingerUp.y && fingerDown.x > fingerUp.x))
                    {
                        angle *= -1;
                    }
                    angle = Mathf.Rad2Deg * Mathf.Atan(angle);
                    col.transform.Rotate(0, 0, angle);
                    lines.Add(temp);

                }

            }



            
        }

       /* For touch only */ 


    }
}
