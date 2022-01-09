using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceRollDemoManager : MonoBehaviour
{
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Debug.Log("sdf");
            PhysicsUtils.RaycastTypeMousePosition(
                out Dice target, LayerMask.NameToLayer("Default")
            );

            if(target != null){
                var forceValue = Random.Range(200, 500);
                var directionX = Random.Range(0, 500);
                var directionY = Random.Range(0, 500);
                var directionZ = Random.Range(0, 500);

                var diceRb = target.GetComponent<Rigidbody>();
                diceRb.AddForce(target.transform.up * forceValue);
                diceRb.AddTorque(directionX, directionY, directionZ);
            }
        }
    }
}
