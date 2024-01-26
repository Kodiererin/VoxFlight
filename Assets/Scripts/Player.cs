using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;

    private float strength= 5f;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            direction=Vector3.up * strength;
        }

        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began){
                direction=Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;  

        



    }
}
 