using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipMan
    {
        public enum State
        {
            Ready,
            MissileFlying,
            End
        }

        private ShipMan()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateEnd = new ShipStateEnd();

            // set active
            this.pShip = null;
            this.pMissile = null;
        }

        public static void Create(SpriteBatchMan pSpriteBatchMan)
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip(pSpriteBatchMan);
            instance.pShip.SetState(ShipMan.State.Ready);
            instance.pSpriteBatchMan = pSpriteBatchMan;

        }

        public static void Destroy()
        {
            ShipMan pMan = ShipMan.instance;
            pMan.pShip=null;
            pMan.pMissile=null;
            
            pMan.pStateReady=null;
            pMan.pStateMissileFlying=null;
            pMan.pStateEnd=null;
            pMan.pSpriteBatchMan=null;
            ShipMan.instance = null;
    }

        private static ShipMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static ShipState GetState(State state)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            ShipState pShipState = null;

            switch (state)
            {
                case ShipMan.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipMan.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipMan.State.End:
                    pShipState = pShipMan.pStateEnd;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            // This can be cleaned up more... no need to re-calling new()
            Missile pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 400, 100);
            pShipMan.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = instance.pSpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = instance.pSpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        private static Ship ActivateShip(SpriteBatchMan pSpriteBatchMan)
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 150, 50);
            pShipMan.pShip = pShip;

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSB_Aliens = pSpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.Attach(pShip.poProxySprite);

            SpriteBatch pSB_Box = pSpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pSB_Box.Attach(pShip.GetColObject().pColSprite);

            // Attach the missile to the missile root
            GameObject pShipRoot = GameObjectMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }

        public static void DettachShip()
        {
            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.pShip = null;
        }

        public static void Attach(SpriteBatchMan pSpriteBatchMan)
        {
            ActivateShip(pSpriteBatchMan);

            ShipMan pShipMan = ShipMan.PrivInstance();
            Debug.Assert(pShipMan != null);
            pShipMan.pShip.SetState(ShipMan.State.Ready);
            pShipMan.pSpriteBatchMan = pSpriteBatchMan;
        }
        // Data: ----------------------------------------------
        private static ShipMan instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateEnd pStateEnd;
        private SpriteBatchMan pSpriteBatchMan;

    }
}