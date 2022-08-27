using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Character
{
    public FixedJoystick joystick;
    private bool isFacingRight;

    public List<GameObject> L_enemy = new List<GameObject>();

    private void Start()
    {
        isFacingRight = true;
    }
    private void Update()
    {
        MoveCharacter();
    }
    public override void MoveCharacter()
    {
        float MoveX = joystick.Horizontal;
        transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Speed * Time.deltaTime;
        if(MoveX < 0 && isFacingRight)
        {
            Flip();
        }
        else if(MoveX > 0 && !isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
