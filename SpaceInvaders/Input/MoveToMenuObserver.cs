using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveToMenuObserver : InputObserver
    {
        public override void Notify()
        {
            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            pSI.UnLoadContent();

            pSI.SetState(SpaceInvaders.State.MenuState);
            pSI.LoadContent();

            pSI.scoreOne = 0;
        }
    }
}