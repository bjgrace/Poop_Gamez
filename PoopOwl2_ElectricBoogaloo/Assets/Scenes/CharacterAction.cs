using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction
{
    public CharacterAction() { }
}

public class ActionSegment
{
    public delegate void ActionDelegate();

    private float xMove;
    private float yMove;
    private bool areIFrames;
    private float poiseMult;
    private int framesLeft;
    private ActionDelegate action;

    public ActionSegment(float _xMove, float _yMove, bool _areIFrames, float _poiseMult, int frames, ActionDelegate _action)
    {
        xMove = _xMove;
        yMove = _yMove;
        areIFrames = _areIFrames;
        poiseMult = _poiseMult;
        framesLeft = frames;
        action = _action;
    }

    // Returns true if there are no more frames left for this action segment; false otherwise
    public bool downTick()
    {
        framesLeft--;
        return framesLeft == 0;
    }

    public ActionDelegate pullAction()
    {
        return action;
    }
}
