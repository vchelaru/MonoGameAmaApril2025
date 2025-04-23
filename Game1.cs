using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.Forms.Controls.Primitives;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using System;
using System.Linq;

namespace MonoGameAndGum;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    GumService Gum => GumService.Default;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Gum.Initialize(this);







        var panel = new StackPanel();
        panel.Anchor(Anchor.Center);
        panel.Spacing = 10;
        panel.AddToRoot();

        for (int i = 0; i < 10; i++)
        {
            var button = new CustomButton();
            button.Text = "Button " + i;
            button.Width = 290;
            button.Click += (_, _) =>
                button.Text = $"Clicked at {DateTime.Now}";
            panel.AddChild(button);
        }





        base.Initialize();
    }


    protected override void Update(GameTime gameTime)
    {
        Gum.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        Gum.Draw();
        base.Draw(gameTime);
    }
}


class CustomButton : Button
{
    public CustomButton()
    {
        var container = new ContainerRuntime();
        container.Height = 32;

        var background = new NineSliceRuntime();
        background.Dock(Gum.Wireframe.Dock.Fill);
        background.SourceFileName = "button_square_depth_gloss.png";
        container.AddChild(background);

        var text = new TextRuntime();
        text.Name = "TextInstance";
        text.Dock(Gum.Wireframe.Dock.Fill);
        container.AddChild(text);

        var category = new StateSaveCategory();
        category.Name = ButtonCategoryName;
        container.AddCategory(category);

        var enabledState = new StateSave();
        enabledState.Name = FrameworkElement.EnabledStateName;
        enabledState.Apply = () =>
        {
            background.SourceFileName = "button_square_depth_gloss.png";
            text.Y = 0;
        };
        category.States.Add(enabledState);

        var highlightedState = enabledState.Clone();
        highlightedState.Name = FrameworkElement.HighlightedStateName;
        category.States.Add(highlightedState);

        var pushedState = new StateSave();
        pushedState.Name = FrameworkElement.PushedStateName;
        pushedState.Apply = () =>
        {
            background.SourceFileName = "button_square_gloss.png";
            text.Y = 4;
        };
        category.States.Add(pushedState);

        this.Visual = container;
    }
}