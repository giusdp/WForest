using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.Props.Interfaces;

namespace WForest.Widgets.Interfaces
{
    /// <summary>
    /// Base interface for Widgets. It contains the main functionality for having trees of widgets
    /// and implements the functionalities to use props and to draw. 
    /// </summary>
    public interface IWidget : IEnumerable<IWidget>, IDrawable, IPropHolder, IInteractive
    {
        internal void ApplyProps()
        {
            foreach (var prop in Props.OfType<IApplicableProp>().OrderBy(p => p.Priority))
                if (!prop.IsApplied)
                    prop.ApplyOn(this);
        }

        #region Widget Tree

        /// <summary>
        /// The parent of this widget. If there is not one, then this widget is root.
        /// </summary>
        IWidget? Parent { get; set; }

        /// <summary>
        /// The collection of widget children.
        /// </summary>
        public ICollection<IWidget> Children { get; }

        /// <summary>
        /// Add a widget as a child of this widget. It updates it's location relative to this widget location
        /// and the child's parent is set to this widget.
        /// </summary>
        /// <param name="widget"></param>
        /// <returns>The added child.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        IWidget AddChild(IWidget widget);

        /// <summary>
        /// Check if it's root by checking if Parent is null.
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// Check if it's leaf by checking if Children.Count to 0.
        /// </summary>
        public bool IsLeaf => Children.Count == 0;

        #endregion

        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Enumerator to cycle through the tree.
        /// </summary>
        /// <returns></returns>
        IEnumerator<IWidget> IEnumerable<IWidget>.GetEnumerator()
        {
            var stack = new Stack<IWidget>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                yield return next;

                if (next.IsLeaf) continue;
                foreach (var c in next.Children.Reverse())
                    stack.Push(c);
            }
        }

        #endregion
    }
}