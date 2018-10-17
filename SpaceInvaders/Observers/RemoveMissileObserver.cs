using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveMissileObserver : ColObserver
    {
        public RemoveMissileObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pMissile = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveMissileObserver(RemoveMissileObserver m, SpriteBatchMan pSpriteBatchMan)
        {
            this.pMissile = m.pMissile;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pMissile = (Missile)this.pSubject.pObjA;
            Debug.Assert(this.pMissile != null);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;
                //   Delay
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
             this.pMissile.Remove(pSpriteBatchMan);
        }

        // data
        private GameObject pMissile;
        private SpriteBatchMan pSpriteBatchMan;
    }
}