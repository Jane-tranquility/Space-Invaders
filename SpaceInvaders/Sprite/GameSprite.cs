using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Only "new" happens in the default constructor Sprite()
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class GameSprite_Base : SpriteBase
    {

    }
    public class GameSprite : GameSprite_Base
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Squid,
            Octopus,
            Crab,

            Ship,
            ShipImage,
            WallVertical,
            WallHorizontal,
            Missile,
            UFO,

            BombStraight,
            BombZigZag,
            BombDagger,

            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            Splash,
            TopSplash,
            PlayerEnd,

            NullObject,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public GameSprite()
            : base()   // <--- Delegate (kick the can)
        {
            this.name = GameSprite.Name.Uninitialized;

            // Use the default - it will be replaced in the Set
            this.pImage = ImageMan.Find(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            this.poScreenRect = new Azul.Rect();
            Debug.Assert(this.poScreenRect != null);
            this.poScreenRect.Clear();

            Debug.Assert(GameSprite.psTmpColor != null);
            GameSprite.psTmpColor.Set(1, 1, 1);

            // here is the actual new
            this.poAzulSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, psTmpColor);
            Debug.Assert(this.poAzulSprite != null);

            // here is the actual new
            this.poAzulColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poAzulColor != null);

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

//        ~GameSprite()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~GameSprite():{0} ", this.GetHashCode());
//#endif
//            this.name = Name.Uninitialized;
//            this.pImage = null;
//            this.poAzulColor = null;
//            this.poAzulSprite = null;
//        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public void Set(GameSprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poScreenRect != null);

            this.poScreenRect.Set(x, y, width, height);
            this.pImage = pImage;
            this.name = name;

            if (pColor == null)
            {
                Debug.Assert(GameSprite.psTmpColor != null);
                GameSprite.psTmpColor.Set(1, 1, 1);

                this.poAzulColor.Set(psTmpColor);
            }
            else
            {
                this.poAzulColor.Set(pColor);
            }

            this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, this.poAzulColor);

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }
        public new void Clear()    // the "new" is there to shut up warning - overriding at derived class
        {
            this.pImage = null;
            this.name = GameSprite.Name.Uninitialized;

            // NOTE:
            // Do not NULL the poAzulSprite it is created once in Default then reused
            // Do not NULL the poColor it is created once in Default then reused

            this.poAzulColor.Set(1, 1, 1);

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        public void SwapColor(Azul.Color _pColor)
        {
            Debug.Assert(_pColor != null);
            Debug.Assert(this.poAzulColor != null);
            Debug.Assert(this.poAzulSprite != null);
            this.poAzulColor.Set(_pColor);
            this.poAzulSprite.SwapColor(_pColor);
        }

        public void SwapColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poAzulColor != null);
            Debug.Assert(this.poAzulSprite != null);
            this.poAzulColor.Set(red, green, blue, alpha);
            this.poAzulSprite.SwapColor(this.poAzulColor);
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(pNewImage != null);
            this.pImage = pNewImage;

            this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.poScreenRect != null);
            return this.poScreenRect;
        }

        public void SetName(GameSprite.Name inName)
        {
            this.name = inName;
        }

        public GameSprite.Name GetName()
        {
            return this.name;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public override void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            this.poAzulSprite.Update();
        }

        public override void Render()
        {
            this.poAzulSprite.Render();
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private Name name;
        private Image pImage;
        private Azul.Color poAzulColor;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poScreenRect;

        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        //---------------------------------------------------------------------------------------------------------
        // Static Data - prevent unecessary "new" in the above methods
        //---------------------------------------------------------------------------------------------------------
        //   static private Azul.Rect psTmpRect = new Azul.Rect();
        static private Azul.Color psTmpColor = new Azul.Color(1, 1, 1);
    }
}