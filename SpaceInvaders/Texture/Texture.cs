using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  No "new" happens constructor Texture() 
    //      Initializes texture to "HotPink" - static texture.
    //
    //  Managers - create a pool of them...
    //  Add - Takes one and reuses it by using Set() 
    //        Actually creates a new texture and replaces the HotPink
    //  Design - side effect
    //      If you recycle multiple textures it's going to garabage collect old texture
    //      Will not do this the first recycle - since texture is pointing to the static HotPink
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class Texture_Link : DLink
    {

    }
    public class Texture : Texture_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Default, // HotPink

            Aliens,
            Shields,
            //Birds,
            Consolas36pt,

            NullObject,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------------------------------
        public Texture()
            : base()   // <--- Delegate (kick the can)
        {
            Debug.Assert(Texture.psDefaultAzulTexture != null);

            this.poAzulTexture = psDefaultAzulTexture;
            Debug.Assert(this.poAzulTexture != null);

            this.name = Name.Default;
        }

//        ~Texture()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~Texture():{0} ", this.GetHashCode());
//#endif
//            this.name = Name.Uninitialized;
//            this.poAzulTexture = null; ;
//        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public void Set(Name name, string pTextureName)
        {
            // Copy the data over
            this.name = name;

            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);

            //  Here is a Texture Swap
            //
            //  Replace the existing texture
            //     Manage Language is doing some work here....
            //     Since we are replacing the "HotPink" texture, its removing its reference
            //     A new allocation is replacing the old "HotPink"
            //     Now the old "HotPink" is marked for garabage collection....but its a static (yeah) no GC.
            //     But if it Set() is called on a User defined texture multiple times... GC is envoked
            //
            //  Not super happy... but this only happens in setup.

            this.poAzulTexture = new Azul.Texture(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            Debug.Assert(this.poAzulTexture != null);

        }
        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            // NOTE:
            // Do not clear the poAzulTexture it is created once in Default then replaced in Set

            this.name = Name.Uninitialized;
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poAzulTexture != null);
            return this.poAzulTexture;
        }

        public void SetName(Texture.Name inName)
        {
            this.name = inName;
        }

        public Texture.Name GetName()
        {
            return this.name;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            if (this.poAzulTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.poAzulTexture.GetHashCode());
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
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private Name name;
        private Azul.Texture poAzulTexture;

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Texture psDefaultAzulTexture = new Azul.Texture("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
    }
}
