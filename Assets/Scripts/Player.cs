using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8;

    private float strength= 5f;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.space) || Input.GetMouseButtonDown(0)){
            direction=Vector3.up * strength;
        }

        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began){
                direction=Vector3.up * strength;
            }
        }

        direction.y += gravity * Time;
        


    }
}
 