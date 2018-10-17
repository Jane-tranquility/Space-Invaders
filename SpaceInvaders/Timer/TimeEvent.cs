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
    public class TimeEvent : DLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            SquidAnimation,
            CrabAnimation,
            OctopusAnimation,
            AlienGridMovement,
            DisplayUFO,
            RemoveSplash,
            MoveToEndState,
            CycleBackToMenu,


            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public TimeEvent()
            : base()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public void Set(TimeEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);

            this.name = eventName;
            this.pCommand = pCommand;
            this.deltaTime = deltaTimeToTrigger;

            // set the trigger time
            this.triggerTime = TimerMan.GetCurrTime() + deltaTimeToTrigger;
        }
        public void Process()
        {
            // make sure the command is valid
            Debug.Assert(this.pCommand != null);
            // fire off command
            this.pCommand.Execute(deltaTime, this.name);
        }
        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }
        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Command: {0}", this.pCommand);
            Debug.WriteLine("   Event Name: {0}", this.name);
            Debug.WriteLine(" Trigger Time: {0}", this.triggerTime);
            Debug.WriteLine("   Delta Time: {0}", this.deltaTime);

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.GetName(), pTmp.GetHashCode());
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;
    }
}
