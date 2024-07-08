using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] private float moveSpeed;
	[SerializeField] float paddingLeft;
	[SerializeField] float paddingRight;
	[SerializeField] float paddingTop;
	[SerializeField] float paddingBottom;

	Vector2 minBounds;
    Vector2 maxBounds;

    Vector3 eulerVector;
    public float tiltingSpeed;
    public float tiltingAngle;

	public float getBackSpeed;
	bool rightPressed;
	bool leftPressed;
	float inputValue;
	void Start()
    {
		eulerVector = transform.localEulerAngles;
		InitBounds();
    }
    void Update()
    {
		Move();
		Tilt();
		MobileInput();
	}

	void MobileInput()
	{
		bool noInput = false;

		// detecting the direction which value shoud be going
		int dir = 0;
		if (rightPressed && leftPressed) // both directions
			dir = 0;
		else if (rightPressed) // only right
			dir = 1;
		else if (leftPressed) // only left
			dir = -1;
		else // no input at all. force must be lerp into zero
			noInput = true;

		Vector2 rawMobileInput = new Vector2(inputValue, 0);
		Vector2 delta = rawMobileInput * moveSpeed * Time.deltaTime;
		Vector2 newPos = new Vector2();
		newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
		newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
		transform.position = newPos;

		if (noInput)
		{
			// lerping force into zero if the force is greater than a threshold (0.01)
			if (Mathf.Abs(inputValue) >= 0.01f)
			{
				int opositeDir = (inputValue > 0) ? -1 : 1;
				inputValue += Time.deltaTime * getBackSpeed * opositeDir;
			}
			else
			{
				inputValue = 0;
				transform.localEulerAngles = new Vector3(0,0,0);
			}

			if(Mathf.Abs(inputValue) <= 0.05f)
			{
				tiltingSpeed = 10;
				eulerVector.z = Mathf.Lerp(eulerVector.z, 0, tiltingSpeed * Time.deltaTime);
				transform.localEulerAngles = eulerVector;
			}
		}
		else
		{
			// increase force towards desired direction
			inputValue += Time.deltaTime * dir * moveSpeed;
			inputValue = Mathf.Clamp(inputValue, -1, 1);
		}
	}
	public void RightButton_Down()
	{
		rightPressed = true;
	}

	public void RightButton_Up()
	{
		rightPressed = false;
	}

	public void LeftButton_Down()
	{
		leftPressed = true;
	}

	public void LeftButton_Up()
	{
		leftPressed = false;
	}
	public void TiltLeft()
    {
		if (leftPressed == true && !rightPressed & inputValue < 0)
		{
			leftPressed = true;
			tiltingSpeed = 3;
			float z = -1.5f * -tiltingAngle;
			eulerVector.z = Mathf.Lerp(eulerVector.z, z, tiltingSpeed * Time.deltaTime);
			transform.localEulerAngles = eulerVector;
		}
		else if (leftPressed && rightPressed)
		{
			leftPressed = false;
			rightPressed = false;
		}
	}
	public void TiltRight()
	{
		if (rightPressed == true && !leftPressed && inputValue > 0)
		{
			rightPressed = true;
			tiltingSpeed = 3;
			float z = 1.5f * -tiltingAngle;
			eulerVector.z = Mathf.Lerp(eulerVector.z, z, tiltingSpeed * Time.deltaTime);
			transform.localEulerAngles = eulerVector;
		}
		else if (leftPressed && rightPressed)
		{
			leftPressed = false;
			rightPressed = false;
		}
	}
	private void Tilt()
    {
		float z = Input.GetAxis("Horizontal") * -tiltingAngle;
		eulerVector.z = Mathf.Lerp(eulerVector.z, z, tiltingSpeed * Time.deltaTime);
		transform.localEulerAngles = eulerVector;
	}
    private void Move()
    {        
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
		Vector2 newPos = new Vector2();
		newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
		newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
		transform.position = newPos;

		//float z = newPos.x * -tiltingAngle;
		//eulerVector.z = Mathf.Lerp(eulerVector.z, z, tiltingSpeed * Time.deltaTime);
		//transform.localEulerAngles = eulerVector;
	}
	private void OnMove(InputValue value)
	{
		rawInput = value.Get<Vector2>();
	}
	void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
