using System;

namespace WForest.UI.Properties.Actions
{
    public class OnPress : Property
    {
        private readonly Action _function; 
        
        public OnPress(Action onPress)
        {
            _function = onPress;
        }
        internal override void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnPress = _function;
        }
    }
}