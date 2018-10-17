using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipEndObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();
            pShip.SetState(ShipMan.State.End);
        }

        // data


    }
}