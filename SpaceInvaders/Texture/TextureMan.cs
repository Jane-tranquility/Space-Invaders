using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //
    //  Singleton class - use only public static methods for customers
    //
    //  * One single compare node is owned by this singleton - used for find, prevent extra news
    //  * Create one - NULL Object - Texture Default
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class TextureMan_MLink : Manager
    {
        public Texture_Link poActive;
        public Texture_Link poReserve;
    }
    public class TextureMan : TextureMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private TextureMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new Texture();
        }

//        ~TextureMan()
//        {
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("~TextureMan():{0}", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            TextureMan.pInstance = null;
//        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureMan(reserveNum, reserveGrow);

                // Default texture
                TextureMan.Add(Texture.Name.Default, "HotPink.tga");

                // NullObject texture
                TextureMan.Add(Texture.Name.NullObject, "HotPink.tga");
            }

        }

        public static void Destroy()
        {
            // Get the instance
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->TextureMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", TextureMan.pInstance, TextureMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            TextureMan.pInstance = null;
        }

        public static Texture Add(Texture.Name name, string pTextureName)
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Debug.Assert(pTextureName != null);

            pNode.Set(name, pTextureName);

            return pNode;
        }
        public static Texture Find(Texture.Name name)
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            Texture pData = (Texture)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(Texture pNode)
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TextureMan pMan = TextureMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture pDataA = (Texture)pLinkA;
            Texture pDataB = (Texture)pLinkB;

            Boolean status = false;

            if (pDataA.GetName() == pDataB.GetName())
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pNode = (Texture)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pData = (Texture)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TextureMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static TextureMan pInstance = null;
        private Texture poNodeCompare;
    }
}
