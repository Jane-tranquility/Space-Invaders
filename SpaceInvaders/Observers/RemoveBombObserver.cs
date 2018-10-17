using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombObserver : ColObserver
    {
        public RemoveBombObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pBomb = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveBombObserver(RemoveBombObserver b, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(b != null);
            this.pBomb = b.pBomb;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {   //delete bomb
            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            //remove the current bomb
            GameObject pA = (GameObject)this.pBomb;
            pA.Remove(this.pSpriteBatchMan);
        }

        private GameObject pBomb;
        private SpriteBatchMan pSpriteBatchMan;
    }
}
