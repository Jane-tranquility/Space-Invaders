using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    {


        public enum Name
        {

            Squid,
            Octopus,
            Crab,

            AlienRoot,
            AlienGrid,
            AlienColumn,

            Missile,
            MissileGroup,

            ShieldRoot,
            ShieldGrid,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldBrick,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            Bomb,
            BombRoot,

            Ship,
            ShipRoot,

            UFO,
            UFOGroup,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            Null_Object,
            Uninitialized
        }

        protected GameObject(GameObject.Name gameName, GameSprite.Name spriteName)
        {
            this.name = gameName;
            this.bMarkForDeath = false;
            this.x = 0.0f;
            this.y = 0.0f;
            this.poProxySprite = ProxySpriteMan.Add(spriteName);
            //this.pSpriteBatchMan = pSpriteBatchMan;

            this.poColObj = new ColObject(this.poProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        //~GameObject()
        //{
        //    this.name = GameObject.Name.Uninitialized;
        //    this.poProxySprite = null;
        //}


        public virtual void Remove(SpriteBatchMan pSpriteBatchMan)
        {
            // Very difficult at first... if you are messy, you will pay here!
            // Given a game object....

            Debug.WriteLine("REMOVE: {0}", this);

            // Remove from SpriteBatch

            // Find the SBNode
            Debug.Assert(this.poProxySprite != null);
            SBNode pSBNode = this.poProxySprite.GetSBNode();

            // Remove it from the manager
            Debug.Assert(pSBNode != null);
            pSpriteBatchMan.Remove(pSBNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSBNode = this.poColObj.pColSprite.GetSBNode();

            Debug.Assert(pSBNode != null);
            pSpriteBatchMan.Remove(pSBNode);

            // Remove from GameObjectMan

            GameObjectMan.Remove(this, pSpriteBatchMan);

            //GhostMan.Add(this);

        }
        public virtual void Update()
        {
            Debug.Assert(this.poProxySprite != null);
            this.poProxySprite.x = this.x;
            this.poProxySprite.y = this.y;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.poColObj.pColSprite != null);
            this.poColObj.pColSprite.Update();
        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.poColObj.poColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    // go to next sibling
                    pNode = (GameObject)Iterator.GetSibling(pNode);
                }

                //this.poColObj.poColRect.Set(201, 201, 201, 201);
                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;

                //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
            }
        }

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }

        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.poProxySprite);
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetLineColor(red, green, blue);
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.poProxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.poProxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.poProxySprite.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

        }



        public ColObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        public GameObject.Name GetName()
        {
            return this.name;
        }


        // Data: ---------------
        public GameObject.Name name;
        //public SpriteBatchMan pSpriteBatchMan;

        public float x;
        public float y;
        public bool bMarkForDeath;
        public ProxySprite poProxySprite;
        protected ColObject poColObj;

    }
}
