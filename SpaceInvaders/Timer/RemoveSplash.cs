using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSplash: Command
    {
        public RemoveSplash(GameObject pGameObject, SpriteBatchMan pSpriteBatchMan)
        {
            // initialized the sprite animation is attached to
            this.pGameObject = pGameObject;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }
        public override void Execute(float deltaTime, TimeEvent.Name name)
        {
            if (this.pGameObject.bMarkForDeath == false)
            {
                this.pGameObject.bMarkForDeath = true;
                //   Delay
                //switch (this.pGameObject.name)
                //{
                //    case GameObject.Name.Ship:
                //        ShipMan.DettachShip();
                //        break;
                //}
                RemoveAlienObserver pObserver = new RemoveAlienObserver(pSpriteBatchMan);
                pObserver.SetGameObject(this.pGameObject);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public GameObject pGameObject;
        private SpriteBatchMan pSpriteBatchMan;
    }
}