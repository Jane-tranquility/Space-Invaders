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
    abstract public class GameObjectNode_Link : DLink
    {

    }

    public class GameObjectNode : GameObjectNode_Link
    {
        public GameObjectNode()
            : base()
        {
            this.poGameObj = null;
        }
//        ~GameObjectNode()
//        {
//#if (TRACK_DESTRUCTOR)
//            Debug.WriteLine("~GameObjectNode():{0}", this.GetHashCode());
//#endif
//            this.poGameObj = null;
//        }
        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.poGameObj = pGameObject;
        }

        public void Wash()
        {
            this.poGameObj = null;
        }

        public Enum GetName()
        {
            return this.poGameObj.name;
        }

        public void Dump()
        {
            Debug.Assert(this.poGameObj != null);
            Debug.WriteLine("\t\t     GameObject: {0}", this.GetHashCode());

            this.poGameObj.Dump();
        }

        // Data: ------------------

        public GameObject poGameObj;

    }
}
