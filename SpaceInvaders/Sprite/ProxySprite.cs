using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------

    abstract public class ProxySprite_Base : SpriteBase
    {

    }
    public class ProxySprite : ProxySprite_Base
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Proxy,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public ProxySprite()
            : base()
        {
            this.name = ProxySprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;


            this.pSprite = null;
        }

//        ~ProxySprite()
//        {
//#if (TRACK_DESTRUCTOR)   
//            Debug.WriteLine("~ProxySprite():{0} ", this.GetHashCode());
//#endif
//            this.pSprite = null;
//            this.name = ProxySprite.Name.Uninitialized;
//        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public ProxySprite(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public void Set(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.PrivPushToReal();
            this.pSprite.Update();
        }


        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.name = Name.Uninitialized;
            this.pSprite = null;
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        private void PrivPushToReal()
        {
            // push the data from proxy to Real GameSprite
            Debug.Assert(this.pSprite != null);

            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        public override void Render()
        {
            // move the values over to Real GameSprite
            this.PrivPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public void SetName(Name inName)
        {
            this.name = inName;
        }
        public Name GetName()
        {
            return this.name;
        }
        public void Dump()
        {

            //// Dump - Print contents to the debug output window
            ////        Using HASH code as its unique identifier 
            //Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            //Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            //if (this.pTexture != null)
            //{
            //    Debug.WriteLine("   Texture: {0} ", this.pTexture.GetName());
            //}
            //else
            //{
            //    Debug.WriteLine("   Texture: null ");
            //}


            //if (this.pNext == null)
            //{
            //    Debug.WriteLine("      next: null");
            //}
            //else
            //{
            //    Image pTmp = (Image)this.pNext;
            //    Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}

            //if (this.pPrev == null)
            //{
            //    Debug.WriteLine("      prev: null");
            //}
            //else
            //{
            //    Image pTmp = (Image)this.pPrev;
            //    Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            //}
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------

        public ProxySprite.Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public GameSprite pSprite;
    }
}