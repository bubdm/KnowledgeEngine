using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KnowledgeEngineRules.Core
{
    class MatchCondition : ICondition
    {
        #region Member Variables

        private Relationship _relationship;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MatchCondition(Rule rule, Relationship relationship)
            : base(rule)
        {
            _relationship = relationship;
            _rule.KnowledgeBase.AddAssociation(relationship.Key, _rule);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if this expression returns true
        /// </summary>
        public override void Fire(LinkedListNode<ICondition> nextcondition)
        {
            if (!_relationship.IsWildcard)
            {
                Fire(nextcondition, _relationship);
            }
            else
            {
                FireWildCards(nextcondition);
            }
        }

        private void FireWildCards(LinkedListNode<ICondition> nextcondition)
        {
            //  set (a b c)
            //  set (a b ?x)
            if (!_variables.Contains(_relationship.Set))
            {
                //  if the variable does not exist we need to
                //  find the value within each relationship
                //  and fire the children

                _variables.Add(_relationship.Set);

                foreach (Relationship r in _knowledge[_relationship.Key])
                {
                    if (r.Set.IsMatchTo(_relationship.Set))
                    {
                        for (int i = 0; i < _relationship.Set.Count; i++)
                        {
                            SetItem item = _relationship.Set[i];
                            if (item.IsWildcard)
                            {
                                _variables[item.Variable.ID].Value = r.Set[i].Value;
                            }
                        }

                        FireNextCondition(nextcondition);
                    }
                }

                _variables.Remove(_relationship.Set);
            }
            else
            {
                //  if the variable does exist, we need to
                //  substitute the value
                //  and fire the children
                Fire(nextcondition, _relationship.Clone());
            }
        }

        private void Fire(LinkedListNode<ICondition> nextcondition, Relationship rel)
        {
            Debug.Write(string.Format("C: Match ({0}) = ", _relationship));
            if (_knowledge.Contains(rel))
            {
                Debug.WriteLine("true");
                FireNextCondition(nextcondition);
            }
            else
            {
                Debug.WriteLine("false");
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}
