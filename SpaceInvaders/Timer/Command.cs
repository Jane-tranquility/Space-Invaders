using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Command
    {
        // define this in concrete
        abstract public void Execute(float deltaTime, TimeEvent.Name name);
    }
}

