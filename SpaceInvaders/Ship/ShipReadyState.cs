using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateReady : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.MissileFlying);
        }

        public override void MoveRight(Ship pShip)
        {
            
            if ((pShip.x + 0.5 * (pShip.GetColObject().poColRect.width)) < 831)
            {
                pShip.x += pShip.shipSpeed;
            }
        }

        public override void MoveLeft(Ship pShip)
        {
            if ((pShip.x - 0.5 * (pShip.GetColObject().poColRect.width)) > 65)
            {
                pShip.x -= pShip.shipSpeed;
            }
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();

            pMissile.SetPos(pShip.x, pShip.y + 5);


            // switch states
            this.Handle(pShip);
        }

    }
}
