using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PomlazkaControl : MonoBehaviour
{
    public enum ePomlazkaStates
    {
        None,
        Prepare,
        Swing
    }
    
    
    public float maxZRot;
    public float rotSpeed = 0.3f;
    public float swingSpeed = 10f;
    private float _originalAngle;
    private ePomlazkaStates _currentState;
    private Rigidbody2D _rb;
    

    void Start()
    {
        _originalAngle = transform.rotation.eulerAngles.z;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            _currentState = ePomlazkaStates.Prepare;

        switch (_currentState)
        {
            case ePomlazkaStates.Prepare:
                _rb.MoveRotation(Mathf.Lerp(transform.rotation.eulerAngles.z, maxZRot, rotSpeed * Time.deltaTime));
                
                if (Mathf.Abs(transform.rotation.eulerAngles.z -  maxZRot) < 3f || !Input.GetKey(KeyCode.Space))
                {
                    _currentState = ePomlazkaStates.Swing;
                }

                break;
            case ePomlazkaStates.Swing:
                _rb.MoveRotation(Mathf.Lerp(transform.rotation.eulerAngles.z, _originalAngle, swingSpeed * Time.deltaTime));
                
                if (Mathf.Abs(transform.rotation.eulerAngles.z - _originalAngle) < 3f )
                {
                    _rb.MoveRotation(_originalAngle);
                    _currentState = ePomlazkaStates.None;
                }

                break;
        }
        
        
    }
}
