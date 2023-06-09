﻿using MazeRunner.Gui.Buttons.States;
using System;

namespace MazeRunner.Gui.Buttons;

public class MenuButton : Button
{
    public override event Action ButtonPressedNotify;

    public MenuButton(float boxScale, Func<bool> canBeClicked) : base(boxScale, canBeClicked)
    {
    }

    public override void Initialize()
    {
        State = new MenuButtonIdleState(this);
    }

    public override void Click(bool notifyAboutPush = true)
    {
        if (notifyAboutPush)
        {
            ButtonPressedNotify.Invoke();
        }
    }
}
