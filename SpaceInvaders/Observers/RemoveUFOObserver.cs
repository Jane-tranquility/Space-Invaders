using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver : ColObserver
    {
        public RemoveUFOObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pUFO = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveUFOObserver(RemoveUFOObserver m, SpriteBatchMan pSpriteBatchMan)
        {
            this.pUFO = m.pUFO;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pUFO = (UFOCategory)this.pSubject.pObjA;
            Debug.Assert(this.pUFO != null);

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;
                //   Delay
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pUFO.Remove(pSpriteBatchMan);
        }

        // data
        private GameObject pUFO;
        private SpriteBatchMan pSpriteBatchMan;
    }
}