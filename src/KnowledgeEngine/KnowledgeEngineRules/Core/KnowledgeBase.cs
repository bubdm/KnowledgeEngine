using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class KnowledgeBase : IEnumerable
    {
        #region Events

        public event KnowledgeChangedEventHandler Changed;

        #endregion

        #region Member Variables

        private Dictionary<RelationKey, List<Relationship>> _knowledge = new Dictionary<RelationKey, List<Relationship>>();
        private Dictionary<RelationKey, List<Rule>> _associations = new Dictionary<RelationKey, List<Rule>>();

        #endregion

        #region Constructors

        public KnowledgeBase()
        {
            _variableBase = new VariableBase();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _knowledge.Clear();
        }

        /// <summary>
        /// Adds a relationship to the knowledge base
        /// </summary>
        public void Add(Relationship r)
        {
            if (!_knowledge.ContainsKey(r.Key))
            {
                _knowledge.Add(r.Key, new List<Relationship>());
            }

            if (!_knowledge[r.Key].Contains(r))
            {
                Debug.WriteLine(string.Format("Added Relationship ({0}) to knowledge base", r));
                
                _knowledge[r.Key].Add(r);
                OnChanged(r);
            }
        }

        /// <summary>
        /// Adds a relationship to the knowledge base
        /// </summary>
        public void AddAssociation(RelationKey key, Rule rule)
        {
            if (!_associations.ContainsKey(key))
            {
                _associations.Add(key, new List<Rule>());
            }

            if (!_associations[key].Contains(rule))
            {
                Debug.WriteLine(string.Format("Added Association ({0} -> {1})", key, rule));

                _associations[key].Add(rule);
            }
        }

        /// <summary>
        /// Removes a relationship to the knowledge base
        /// </summary>
        public void Remove(Relationship r)
        {
            if (_knowledge.ContainsKey(r.Key))
            {
                if (_knowledge[r.Key].Contains(r))
                {
                    Debug.WriteLine(string.Format("Removed Relationship ({0}) from knowledge base", r));
                    
                    _knowledge[r.Key].Remove(r);
                    OnChanged(r);
                    
                    if (_knowledge[r.Key].Count < 1)
                    {
                        _knowledge.Remove(r.Key);
                    }
                }
            }
        }

        private void OnChanged(Relationship rel)
        {
            if (Changed != null)
            {
                Changed(this, new KnowledgeChangedEventArgs(rel.Key, GetAssociatedRules(rel.Key)));
            }
        }

        /// <summary>
        /// Does the knowledge base contain the relationship
        /// </summary>
        public bool Contains(Relationship relationship)
        {
            if (!_knowledge.ContainsKey(relationship.Key))
            {
                return false;
            }

            return _knowledge[relationship.Key].Contains(relationship);
        }

        /// <summary>
        /// Gets a list of rules who are affected by the related key
        /// </summary>
        private List<Rule> GetAssociatedRules(RelationKey key)
        {
            if (_associations.ContainsKey(key))
            {
                return _associations[key];
            }
            return new List<Rule>();
        }

        /// <summary>
        /// String Respresentation
        /// </summary>
        public override string ToString()
        {
            List<string> output = new List<string>();

            foreach (KeyValuePair<RelationKey, List<Relationship>> kvp in _knowledge)
            {
                foreach (Relationship relationship in kvp.Value)
                {
                    output.Add(relationship.ToString());
                }
            }

            if (output.Count == 0)
            {
                return "(Empty)";
            }

            return string.Join(", ", output.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        public List<KnowledgeItem> ToKnowledgeItems()
        {
            List<KnowledgeItem> list = new List<KnowledgeItem>();
            foreach (KeyValuePair<RelationKey, List<Relationship>> item in _knowledge)
            {
                foreach (Relationship r in item.Value)
                {
                    if (!r.IsWildcard)
                    {
                        KnowledgeItem ki = new KnowledgeItem(r.ID);
                        foreach (SetItem si in r.Set)
                        {
                            ki.Set.Add(string.Format("{0}", si.Value));
                        }
                        list.Add(ki);
                    }
                }
            }
            return list;
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Returns an array of relationships.
        /// </summary>
        public Relationship[] this[RelationKey key]
        {
            get
            {
                if (_knowledge.ContainsKey(key))
                {
                    return _knowledge[key].ToArray();
                }
                return new Relationship[0];
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a handle to the knowledge base
        /// </summary>
        public VariableBase Variables
        {
            get { return _variableBase; }
        }
        private VariableBase _variableBase;

        #endregion        

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return _knowledge.GetEnumerator();
        }

        #endregion
    }
}
