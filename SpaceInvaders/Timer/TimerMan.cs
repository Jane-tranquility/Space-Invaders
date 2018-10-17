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
    public class TimerMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private TimerMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new TimeEvent();
        }

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
                pInstance = new TimerMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            // Get the instance
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("--->TimerMan.Destroy()");
#endif
            pMan.BaseDestroy();

#if (TRACK_DESTRUCTOR_MAN)
            Debug.WriteLine("     {0} ({1})", pMan.poNodeCompare, pMan.poNodeCompare.GetHashCode());
            Debug.WriteLine("     {0} ({1})", TimerMan.pInstance, TimerMan.pInstance.GetHashCode());
#endif

            pMan.poNodeCompare = null;
            TimerMan.pInstance = null;
        }
        public static TimeEvent Add(TimeEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            TimeEvent pNode = (TimeEvent)pMan.baseAddSorted(deltaTimeToTrigger + GetCurrTime());
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            return pNode;
        }
        public static TimeEvent Find(TimeEvent.Name name)
        {
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            Debug.Assert(pMan.poNodeCompare != null);
            pMan.poNodeCompare.Wash();
            pMan.poNodeCompare.name = name;

            TimeEvent pData = (TimeEvent)pMan.BaseFind(pMan.poNodeCompare);
            return pData;
        }
        public static void Remove(TimeEvent pNode)
        {
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BaseDump();
        }

        public static void Update(float totalTime)
        {
            // Get the instance
            TimerMan pMan = TimerMan.PrivGetInstance();
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted
            while (pEvent != null && (pMan.mCurrTime >= pEvent.triggerTime))
            {
                // Difficult to walk a list and remove itself from the list
                // so squirrel away the next event now, use it at bottom of while
                pNextEvent = (TimeEvent)pEvent.pNext;

                if (pMan.mCurrTime >= pEvent.triggerTime)
                {
                    // call it
                    pEvent.Process();

                    // remove from list
                    pMan.BaseRemove(pEvent);
                }

                // advance the pointer
                pEvent = pNextEvent;
            }
        }
        public static float GetCurrTime()
        {
            // Get the instance
            TimerMan pTimerMan = TimerMan.PrivGetInstance();

            // return time
            return pTimerMan.mCurrTime;
        }
        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new TimeEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimeEvent pDataA = (TimeEvent)pLinkA;
            TimeEvent pDataB = (TimeEvent)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pNode = (TimeEvent)pLink;
            pNode.Wash();
        }
        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pData = (TimeEvent)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerMan PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static TimerMan pInstance = null;
        private TimeEvent poNodeCompare;
        protected float mCurrTime;
    }
}
