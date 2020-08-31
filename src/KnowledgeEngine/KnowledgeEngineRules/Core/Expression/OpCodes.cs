using System;

namespace KnowledgeEngineRules.Core
{
    /// <summary>
    /// Operational Codes available to the Intelligent Gear VM
    /// </summary>
    public enum OpCodes
    {
        NoOp,
        /*
        =============================================================
        PUSHING / POPPING REFERENCES & VALUES
        */
        /// <summary>
        /// Loads a reference to the op stack of an object (which is a property of the running engine)
        /// </summary>
        LoadObject,
        /// <summary>
        /// Using reflection invoke a method
        /// </summary>
        InvokeMethod,
        /// <summary>
        /// Using reflection get a property value
        /// </summary>
        InvokeProperty,
        /// <summary>
        /// Loads a reference to a variable
        /// </summary>
        LoadVariable,
        /// <summary>
        /// (& : string concat) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Str,
        /// <summary>
        /// (isnull(expr)) Pop a value off of the op stack, and pushes the result
        /// </summary>
        IsNull,
        /*
        =============================================================
        MATH
        */
        /// <summary>
        /// (+) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Add,
        /// <summary>
        /// (-) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Sub,
        /// <summary>
        /// (*) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Mul,
        /// <summary>
        /// (/) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Div,
        /// <summary>
        /// (%) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Mod,
        /// <summary>
        /// (**) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Pow,
        /// <summary>
        /// (- negate) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Neg,
        /*
        =============================================================
        BOOLEAN
        */
        /// <summary>
        /// (AND) Pops two values off of the op stack, and pushes the result
        /// </summary>
        And,
        /// <summary>
        /// (OR) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Or,
        /// <summary>
        /// (XOR) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Xor,
        /// <summary>
        /// (NOT) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Not,
        /// <summary>
        /// (IF EQ) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Eq,
        /// <summary>
        /// (IF NE) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Ne,
        /// <summary>
        /// (IF GT) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Gt,
        /// <summary>
        /// (IF GE) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Ge,
        /// <summary>
        /// (IF LT) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Lt,
        /// <summary>
        /// (IF LE) Pops two values off of the op stack, and pushes the result
        /// </summary>
        Le,
        /*
        =============================================================
        PUSH / POP
        */
        /// <summary>
        /// Pushes a constant onto the stack
        /// </summary>
        Push,
        /// <summary>
        /// Pushes a constant integer onto the stack
        /// </summary>
        PushInt,
        /// <summary>
        /// Pushes a constant string onto the stack
        /// </summary>
        PushStr,
        /// <summary>
        /// Pushes a constant number onto the stack
        /// </summary>
        PushNbr,
        /// <summary>
        /// Pushes a constant boolean onto the stack
        /// </summary>
        PushBool,
        /// <summary>
        ///	Pops a constant off of the stack
        /// </summary>
        Pop,
        /// <summary>
        ///	Pops a constant integer off of the stack
        /// </summary>
        PopInt,
        /// <summary>
        ///	Pops a constant string off of the stack
        /// </summary>
        PopStr,
        /// <summary>
        ///	Pops a constant number off of the stack
        /// </summary>
        PopNbr,
        /// <summary>
        ///	Pops a constant boolean off of the stack
        /// </summary>
        PopBool
    }
}
