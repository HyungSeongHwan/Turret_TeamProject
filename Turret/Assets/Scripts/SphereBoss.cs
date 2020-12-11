using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBoss : MonoBehaviour
{
	Animator anim;

	private Transform m_TargetPos = null;

	public bool m_isMove = false;
	public bool m_isRoll = false;

	private float m_MoveSpeed = 5.0f;

    // Update is called once per frame
    void Update()
	{
		MoveBoss();
	}

	public void Initialize(Transform targetPos)
    {
		m_TargetPos = targetPos;

		m_isRoll = true;
		m_isMove = false;

		anim = gameObject.GetComponentInChildren<Animator>();
	}

	private void MoveBoss()
    {
		if (m_TargetPos == null) return;

		if (m_isRoll)
		{
			Vector3 kTargetPos = new Vector3();
			kTargetPos.x = m_TargetPos.position.x;
			kTargetPos.z = m_TargetPos.position.z;
			kTargetPos.y = 0;

			transform.LookAt(kTargetPos);
			Debug.Log(transform.eulerAngles);

            anim.SetBool("Open_Anim", false);
            anim.SetBool("Roll_Anim", true);

            SetIsRoll(false);
            SetIsMove(true);
		}

		if (m_isMove)
		{
			transform.Translate(new Vector3(0, 0, 2.5f * m_MoveSpeed * Time.deltaTime));
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            SetIsMove(false);

            Coll_Wall();
        }
    }

    public void SetIsMove(bool isMove)
    {
        m_isMove = isMove;
    }

    public void SetIsRoll(bool isRoll)
    {
        m_isRoll = isRoll;
    }

    private void Coll_Wall()
    {
        anim.SetBool("Roll_Anim", false);
        anim.SetBool("Open_Anim", true);

        Invoke("Invoke_Move", 3.0f);
    }

    public void Invoke_Move()
    {
        SetIsRoll(true);
    }


    //void CheckKey()
    //{
    //
    //	// Walk
    //	if (Input.GetKey(KeyCode.W))
    //	{
    //		anim.SetBool("Walk_Anim", true);
    //	}
    //	else if (Input.GetKeyUp(KeyCode.W))
    //	{
    //		anim.SetBool("Walk_Anim", false);
    //	}
    //
    //	// Rotate Left
    //	if (Input.GetKey(KeyCode.A))
    //	{
    //		rot[1] -= rotSpeed * Time.fixedDeltaTime;
    //	}
    //
    //	// Rotate Right
    //	if (Input.GetKey(KeyCode.D))
    //	{
    //		rot[1] += rotSpeed * Time.fixedDeltaTime;
    //	}
    //
    //	// Roll
    //	if (Input.GetKeyDown(KeyCode.Space))
    //	{
    //		if (anim.GetBool("Roll_Anim"))
    //		{
    //			anim.SetBool("Roll_Anim", false);
    //		}
    //		else
    //		{
    //			anim.SetBool("Roll_Anim", true);
    //		}
    //	}
    //
    //	// Close
    //	if (Input.GetKeyDown(KeyCode.LeftControl))
    //	{
    //		if (!anim.GetBool("Open_Anim"))
    //		{
    //			anim.SetBool("Open_Anim", true);
    //		}
    //		else
    //		{
    //			anim.SetBool("Open_Anim", false);
    //		}
    //	}
    //}

}
	