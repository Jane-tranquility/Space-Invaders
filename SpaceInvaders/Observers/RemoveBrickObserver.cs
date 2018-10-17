using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver : ColObserver
    {

        public RemoveBrickObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pBrick = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveBrickObserver(RemoveBrickObserver b, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {

            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (pBrick.bMarkForDeath == false)
            {
                pBrick.bMarkForDeath = true;
                //   Delay
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = this.pBrick;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Remove(pSpriteBatchMan);

            // TODO: Need a better way... 
            if (privCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)Iterator.GetParent(pB);
                pB.Remove(pSpriteBatchMan);

                if (privCheckParent(pC) == true)
                {
                      pC.Remove(pSpriteBatchMan);
                }

            }
        }

        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }


        // data
        private GameObject pBrick;
        private SpriteBatchMan pSpriteBatchMan;
    }
}

