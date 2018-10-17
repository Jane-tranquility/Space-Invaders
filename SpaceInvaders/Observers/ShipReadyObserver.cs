using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipReadyObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();
            pShip.SetState(ShipMan.State.Ready);
        }

        // data


    }
}