using Game.Interfaces;
using UnityEngine;

public class Player3D : BaseEntity
{
    [Header("Movement Strategy")]
    public IMovementStrategy moveStrategy;
    public Camera mainCamera;

    InputManager input;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
