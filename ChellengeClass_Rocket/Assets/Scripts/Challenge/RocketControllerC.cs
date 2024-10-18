using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;
    
    private bool _isMoving;
    private float _movementDirection;
    
    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }
    
    private void FixedUpdate()
    {
        if (!_isMoving) return;
        
        if(!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;
        
        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    private void OnMove(InputValue value)
    {
        if(value.Get<float>() != 0)
        {
            Debug.Log("좌우입력");
            _isMoving = true;
            _movementDirection = -value.Get<float>();
        }
        else
        {
            _isMoving = false;
        }
    }


    // OnBoost 구현
    // private void OnBoost...
    private void OnBoost(InputValue value)
    {
        Debug.Log("부스터");
        _energySystem.UseEnergy(ENERGY_BURST);
        _rocketMovement.ApplyBoost();

    }
}