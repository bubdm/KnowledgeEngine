using System;
using System.Collections;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class OpStack
    {
        #region Member Variables

        private Stack _stack;

        #endregion

        #region Constructors

        public OpStack()
        {
            _stack = new Stack();
        }

        #endregion

        #region Methods

        public void Clear()
        {
            _stack.Clear();
        }

        public override string ToString()
        {
            if (_stack.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object o in _stack)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(o);
                }
                return sb.ToString();
            }
            return "(Empty)";
        }

        #endregion

        #region Pop

        /// <summary>
        /// Pops an object off the stack
        /// </summary>
        public object Pop()
        {
            return _stack.Pop();
        }

        /// <summary>
        /// Pops a float off the stack
        /// </summary>
        /// <returns>Float representation of the object</returns>
        public float PopNbr()
        {
            try
            {
                return Convert.ToSingle(PopStr());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Pops a boolean off the stack
        /// </summary>
        /// <returns>Boolean representation of the object</returns>
        public bool PopBool()
        {
            try
            {
                return Convert.ToBoolean(PopStr());
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Pops a string off the stack
        /// </summary>
        /// <returns>String representation of the object</returns>
        public string PopStr()
        {
            return Convert.ToString(_stack.Pop());
        }

        #endregion

        #region Push

        /// <summary>
        /// Pushs a float onto the stack
        /// </summary>
        /// <returns>Float representation of the object</returns>
        public void Push(object obj)
        {
            _stack.Push(obj);
        }

        /// <summary>
        /// Pushs a float onto the stack
        /// </summary>
        /// <returns>Float representation of the object</returns>
        public void PushNbr(object obj)
        {
            _stack.Push(Convert.ToSingle(obj));
        }

        /// <summary>
        /// Pushs a boolean onto the stack
        /// </summary>
        /// <returns>Boolean representation of the object</returns>
        public void PushBool(object obj)
        {
            _stack.Push(Convert.ToBoolean(obj));
        }

        /// <summary>
        /// Pushs a string onto the stack
        /// </summary>
        /// <returns>String representation of the object</returns>
        public void PushStr(object obj)
        {
            _stack.Push(Convert.ToString(obj));
        }

        #endregion
    }
}