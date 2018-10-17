using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Only "new" happens in the default constructor Image()
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //
    //---------------------------------------------------------------------------------------------------------

    abstract public class Image_Link : DLink
    {

    }
    public class Image : Image_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Default,    // Hot Pink

            //RedBird,
            //YellowBird,
            //GreenBird,
            //WhiteBird,
            //BlueBird,

            SquidOut,
            SquidIn,
            CrabUp,
            CrabDown,
            OctopusOut,
            OctopusIn,

            Ship,
            Wall,
            Missile,
            UFO,

            BombStraight,
            BombZigZag,
            BombCross,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            Splash,
            TopSplash,
            PlayerEnd,

            NullObject,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------

        public Image()
            : base()   // <--- Delegate (kick the can)
        {
            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);

            this.Clear();
        }
//        ~Image()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~Image():{0} ", this.GetHashCode());
//#endif
//            this.name = Name.Uninitialized;
//            this.pTexture = null;
//            this.poRect = null;
//        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public void Set(Name name, Texture pTexture, float x, float y, float width, float height)
        {
            // Copy the data over
            this.name = name;

            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;

            this.poRect.Set(x, y, width, height);
        }
        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.pTexture = null;
            this.name = Name.Uninitialized;
            this.poRect.Clear();
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }


        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public void SetName(Image.Name inName)
        {
            this.name = inName;
        }

        public Image.Name GetName()
        {
            return this.name;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            if (this.pTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.pTexture.GetName());
            }
            else
            {
                Debug.WriteLine("   Texture: null ");
            }


            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private Name name;
        private Azul.Rect poRect;
        private Texture pTexture;
    }
}
