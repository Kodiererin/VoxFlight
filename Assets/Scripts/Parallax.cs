using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Parallax Class : Parallax scrolling is a 2D art technique that gives an illusion of depth by
/// making background images move slower than those in the foreground. This motion can transform a scene 
/// into an a immersive,infonite landscape and enhancing storytelling.
/// </summary>
public class Parallax : MonoBehaviour
{
   private MeshRenderer meshRenderer;

   public float animationSpeed = 1f;

   private void Awake(){
    meshRenderer = GetComponent<MeshRenderer>();
   }


   private void Update(){
       meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed* Time.deltaTime, 0) ;
   }
}
