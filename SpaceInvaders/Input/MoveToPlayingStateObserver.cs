using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveToPlayingStateObserver : InputObserver
    {
        public override void Notify()
        {
            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            pSI.UnLoadContent();

            pSI.SetState(SpaceInvaders.State.GamePlayingState);
            pSI.LoadContent();
        }
    }
}