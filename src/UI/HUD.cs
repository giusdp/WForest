using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WForest.UI.WidgetTreeHandlers;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;


        public HUD()
        {
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree(Factories.Widgets.Container(new Rectangle(0, 0, 1280, 720)));
            _root.AddProperty(Factories.Properties.Row());
            _root.AddProperty(Factories.Properties.JustifyCenter());
            var container = _root.AddChild(Factories.Widgets.Container());
            container.AddProperty(Factories.Properties.Column());
            container.AddProperty(Factories.Properties.Stretch());
            container.AddProperty(Factories.Properties.JustifyCenter());
            container.AddProperty(Factories.Properties.Border());

            var imgButton = Factories.Widgets.ImageButton("SpriteBtnL");
            var child = container.AddChild(imgButton);
            child.AddProperty(Factories.Properties.Margin(10, 10, 0, 0));
            child.AddProperty(Factories.Properties.Rounded(30));
            child.AddProperty(Factories.Properties.Border());
            
            _widgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Update()
        {
            _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}