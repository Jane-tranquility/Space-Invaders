using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShieldsObserver : ColObserver
    {

        public RemoveShieldsObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pShields = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveShieldsObserver(RemoveShieldsObserver b, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(b != null);
            this.pShields = b.pShields;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {

            this.pShields = (ShieldGrid)this.pSubject.pObjB;
            Debug.Assert(this.pShields != null);

            if (pShields.bMarkForDeath == false)
            {
                pShields.bMarkForDeath = true;
                //   Delay
                RemoveShieldsObserver pObserver = new RemoveShieldsObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pShields;
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
        private GameObject pShields;
        private SpriteBatchMan pSpriteBatchMan;
    }
}
