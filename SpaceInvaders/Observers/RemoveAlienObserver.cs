using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveAlienObserver : ColObserver
    {

        public RemoveAlienObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pAlien = null;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public RemoveAlienObserver(RemoveAlienObserver b, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(b != null);
            this.pAlien = b.pAlien;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            
            this.pAlien = this.pSubject.pObjB;
            //Image pImage = ImageMan.Find(Image.Name.Splash);
            //this.pAlien.poProxySprite.Set(GameSprite.Name.Splash);
            //this.pAlien.Update();
            Debug.Assert(this.pAlien != null);

            if (pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;
                //   Delay
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this, pSpriteBatchMan);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Remove(pSpriteBatchMan);

            // TODO: Need a better way... 
            if (privCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)Iterator.GetParent(pB);
                if (pC != null)
                {
                    pB.Remove(pSpriteBatchMan);

                    if (privCheckParent(pC) == true)
                    {
                        pC.Remove(pSpriteBatchMan);
                    }
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

        public void SetGameObject(GameObject b)
        {
            this.pAlien = b;
        }


        // data
        private GameObject pAlien;
        private SpriteBatchMan pSpriteBatchMan;
    }
}