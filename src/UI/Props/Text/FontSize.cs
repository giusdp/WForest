using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Utils;

namespace WForest.UI.Props.Text
{
    /// <summary>
    /// Property only applicable on Text Widget, it changes the size of the font of the widget.
    /// </summary>
    public class FontSize : Prop
    {
        private readonly int _size;

        internal FontSize(int size)
        {
            _size = size;
        }

        /// <summary>
        /// Applies font size change on Text Widget. It assigns the new size to the FontSize field of the widget and
        /// measures (and updates) the new space taken by the text.
        /// </summary>
        /// <param name="widgetNode"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            if (widgetNode.Data is Widgets.Text text)
            {
                if (text.FontSize == _size) return;
                text.FontSize = _size >= 0 ? _size : throw new ArgumentException("FontSize cannot be negative.");
                var (x,y,_,_) = text.Space;
                var (w, h) = text.Font.MeasureText(text.TextString, _size);
                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x,y, (int) w,(int) h));
            }
            else
            {
                Log.Error(
                    $"FontSize property is only applicable to a Text Widget. Instead it has received a {widgetNode}");
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget.");
            }
        }
    }
}