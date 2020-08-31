using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class ReflectionCache
    {
        #region Member Classes

        class TypeFinder : Dictionary<Type, Cache>
        {
        }

        class Cache
        {
            public Type Type;
            public FieldFinder Fields;
            public PropertyFinder Properties;
            public MethodFinder Methods;

            public Cache(Type type)
            {
                Type = type;
                Fields = new FieldFinder(this);
                Properties = new PropertyFinder(this);
                Methods = new MethodFinder(this);
            }
        }

        class PropertyFinder
        {
            private Cache _cache;
            private Dictionary<string, PropertyInfo> _list;

            public PropertyFinder(Cache cache)
            {
                _cache = cache;
                _list = new Dictionary<string, PropertyInfo>();
            }

            public PropertyInfo this[string name]
            {
                get
                {
                    if (!_list.ContainsKey(name))
                    {
                        _list.Add(name, _cache.Type.GetProperty(name));
                    }
                    return _list[name];
                }
            }
        }

        class FieldFinder
        {
            private Cache _cache;
            private Dictionary<string, FieldInfo> _list;

            public FieldFinder(Cache cache)
            {
                _cache = cache;
                _list = new Dictionary<string, FieldInfo>();
            }

            public FieldInfo this[string name]
            {
                get
                {
                    if (!_list.ContainsKey(name))
                    {
                        _list.Add(name, _cache.Type.GetField(name));
                    }
                    return _list[name];
                }
            }
        }

        class MethodFinder : Dictionary<string, MethodInfo>
        {
            private Cache _cache;
            private Dictionary<string, MethodInfo> _list;

            public MethodFinder(Cache cache)
            {
                _cache = cache;
                _list = new Dictionary<string, MethodInfo>();
            }

            public MethodInfo this[string name]
            {
                get
                {
                    if (!_list.ContainsKey(name))
                    {
                        _list.Add(name, _cache.Type.GetMethod(name));
                    }
                    return _list[name];
                }
            }
        }

        #endregion

        #region Member Variables

        public static readonly ReflectionCache Instance = new ReflectionCache();

        private readonly PropertyInfo _enginePropertyOfRule = typeof(Rule).GetProperty("Engine");
        private TypeFinder _typefinder = new TypeFinder();

        #endregion

        #region Constructors

        private ReflectionCache()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a handle to the root engine property
        /// </summary>
        /// <returns></returns>
        public PropertyInfo GetThis()
        {
            return _enginePropertyOfRule;
        }

        /// <summary>
        /// Get a property within a type
        /// </summary>
        public FieldInfo GetField(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Parameter cannot be null");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name", "Parameter cannot be null");
            }
            if (!_typefinder.ContainsKey(type))
            {
                _typefinder.Add(type, new Cache(type));
            }
            return _typefinder[type].Fields[name];
        }

        /// <summary>
        /// Get a property within a type
        /// </summary>
        public PropertyInfo GetProperty(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Parameter cannot be null");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name", "Parameter cannot be null");
            }
            if (!_typefinder.ContainsKey(type))
            {
                _typefinder.Add(type, new Cache(type));
            }
            return _typefinder[type].Properties[name];
        }

        /// <summary>
        /// Get a method within a type
        /// </summary>
        public MethodInfo GetMethod(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Parameter cannot be null");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name", "Parameter cannot be null");
            }
            if (!_typefinder.ContainsKey(type))
            {
                _typefinder.Add(type, new Cache(type));
            }
            return _typefinder[type].Methods[name];
        }

        #endregion

        #region Properties

        #endregion
    }
}
