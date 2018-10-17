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
    //  * Create one - NULL Object - Sprite Default
    //  * Dependency - ImageMan needs to be initialized before SpriteMan
    //
    //---------------------------------------------------------------------------------------------------------
    abstract public class GameSpriteMan_MLink : Manager
    {
        public GameSprite_Base poActive;
        public GameSprite_Base poReserve;
    }
    public class GameSpriteMan : GameSpriteMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GameSpriteMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameSprite();
        }

//        ~GameSpriteMan()
//        {
//#if(TRACK_DESTRUCTOR)
//            Debug.WriteLine("~GameSpriteMan():{0}", this.GetHashCode());
//#endif
//            this.poNodeCompare = null;
//            GameSpriteMan.pInstance = null;
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
                pInstance = new GameSpriteMan(reserveNum, reserveGrow);

                // Add a NULL Sprite into the Manager, allows find 
                GameSprite pGSprite = GameSpriteMan.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
                Debug.Assert(pGSprite != null);
            }
        }

        public static void Destroy()
        {
            // Get the instance
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);
#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->GameSpriteMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", GameSpriteMan.pInstance, GameSpriteMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            GameSpriteMan.pInstance = null;
        }

        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameSprite pNode = (GameSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Image pImage = ImageMan.Find(ImageName);
            Debug.Assert(pImage != null);


            pNode.Set(name, pImage, x, y, width, height, pColor);

            return pNode;
        }
        public static GameSprite Find(GameSprite.Name name)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.SetName(name);

            GameSprite pData = (GameSprite)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(GameSprite pNode)
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            GameSpriteMan pMan = GameSpriteMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameSprite pDataA = (GameSprite)pLinkA;
            GameSprite pDataB = (GameSprite)pLinkB;

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
            GameSprite pNode = (GameSprite)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pData = (GameSprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameSpriteMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static GameSpriteMan pInstance = null;
        private GameSprite poNodeCompare;
    }
}
