using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{


        /// <summary>
        /// The 'Originator' class
        /// </summary>
        public class Originator
        {
            private VisualNovelEngine _state;

            // Property
            public VisualNovelEngine State
            {
                get { return _state; }
                set
                {
                    _state = value;
                    Console.WriteLine("State = " + _state);
                }
            }

            // Creates memento
            public Memento CreateMemento()
            {
                return (new Memento(_state));
            }

            // Restores original state
            public void SetMemento(Memento memento)
            {
                Console.WriteLine("Restoring state...");
                State = memento.State;
            }
        }

        /// <summary>
        /// The 'Memento' class
        /// </summary>
        public class Memento
        {
            private VisualNovelEngine _state;

            // Constructor
            public Memento(VisualNovelEngine state)
            {
                this._state = state;
            }

            // Gets or sets state
            public VisualNovelEngine State
            {
                get { return _state; }
            }
        }

        /// <summary>
        /// The 'Caretaker' class
        /// </summary>
        public class Caretaker
        {
            private Memento _memento;

            // Gets or sets memento
            public Memento Memento
            {
                set { _memento = value; }
                get { return _memento; }
            }
        
    }
}
