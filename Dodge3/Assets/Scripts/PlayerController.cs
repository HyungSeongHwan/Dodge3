using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody m_Rigidbody = null;

    public float m_MoveSpeed = 0.0f;

    public bool m_isMove = false;

    public Action<Collider> OnMyCollision = null;


    private void Update()
    {
        MovePlayer();
    }


    public void Initialize()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        SetIsMove(true);
        Show(true);
    }

    public void SetIsMove(bool bMove)
    {
        m_isMove = bMove;
    }

    private void MovePlayer()
    {
        if (m_isMove)
        {
            float xValue = Input.GetAxis("Horizontal");
            float yValue = Input.GetAxis("Vertical");

            xValue *= m_MoveSpeed;
            yValue *= m_MoveSpeed;

            m_Rigidbody.velocity = new Vector3(xValue, 0, yValue);
        }
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    public void PlayerDie()
    {
        SetIsMove(false);
        Show(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnMyCollision != null)
            OnMyCollision(other);
    }

}
