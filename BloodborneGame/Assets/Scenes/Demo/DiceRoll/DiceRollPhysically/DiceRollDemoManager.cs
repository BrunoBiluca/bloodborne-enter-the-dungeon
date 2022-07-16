using DiceRoll;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.DiceSystem;

public class DiceRollDemoManager : MonoBehaviour
{
    [SerializeField] private DiceRoll.Dice dice;

    public void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var forceValue = Random.Range(400, 800);
            var directionX = Random.Range(0, 500);
            var directionY = Random.Range(0, 500);
            var directionZ = Random.Range(0, 500);

            var diceRb = dice.GetComponent<Rigidbody>();
            diceRb.AddForce(Vector3.up * forceValue);
            diceRb.AddTorque(directionX, directionY, directionZ);
        }
    }
}
