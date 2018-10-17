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
    //  * Create one - NULL Object - Image Default
    //  * Dependency - TextureMan needs to be initialized before ImageMan
    //
    //---------------------------------------------------------------------------------------------------------

    abstract public class ImageMan_MLink : Manager
    {
        public Image_Link poActive;
        public Image_Link poReserve;
    }
    public class ImageMan : ImageMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private ImageMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);


            // initialize derived data here
            this.poNodeCompare = new Image();


        }

//        ~ImageMan()
//        {
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("~ImageMan():{0}", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            ImageMan.pInstance = null;
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
                pInstance = new ImageMan(reserveNum, reserveGrow);

                // Add a NULL Texture into the Manager, allows find 
                Image pImage;

                pImage = ImageMan.Add(Image.Name.NullObject, Texture.Name.NullObject, 0, 0, 128, 128);
                Debug.Assert(pImage != null);

                // Default image manager
                pImage = ImageMan.Add(Image.Name.Default, Texture.Name.Default, 0, 0, 128, 128);
                Debug.Assert(pImage != null);
            }

        }

        public static void Destroy()
        {
            // Get the instance
            ImageMan pMan = ImageMan.PrivGetInstance();
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->ImageMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", ImageMan.pInstance, ImageMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            ImageMan.pInstance = null;
        }

        public static Image Add(Image.Name ImageName, Texture.Name TextureName, float x, float y, float width, float height)
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Texture pTexture = TextureMan.Find(TextureName);
            Debug.Assert(pTexture != null);

            pNode.Set(ImageName, pTexture, x, y, width, height);

            return pNode;
        }
        public static Image Find(Image.Name name)
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            Image pData = (Image)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(Image pNode)
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            ImageMan pMan = ImageMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Image pDataA = (Image)pLinkA;
            Image pDataB = (Image)pLinkB;

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
            Image pNode = (Image)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Image pData = (Image)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static ImageMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static ImageMan pInstance = null;
        private Image poNodeCompare;
    }
}
