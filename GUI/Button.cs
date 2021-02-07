class Button
{
    private readonly Sound rollover = Engine.LoadSound("ButtonRollover.wav");

    private Texture tNormal;
    private Texture tHovered;
    private Texture tPressed;

    private bool hovered, pressed, clicked;

    private Bounds2 bounds;

    public Button(Bounds2 bounds, Texture tNormal, Texture tHovered, Texture tPressed)
    {
        hovered = false;
        pressed = false;

        clicked = false;

        this.bounds = bounds;

        this.tNormal = tNormal;
        this.tHovered = tHovered;
        this.tPressed = tPressed;
    }

    public void Update()
    {
        if (bounds.Contains(Engine.MousePosition))
        {
            if (!hovered)
            {
                Engine.PlaySound(rollover);
                hovered = true;
            }
            if (Engine.GetMouseButtonHeld(MouseButton.Left))
            {
                if (!pressed)
                {
                    Engine.PlaySound(rollover);
                }
                pressed = true;
                Engine.DrawTexture(tPressed, bounds.Position);
            }
            else
            {
                if (pressed)
                {
                    Engine.PlaySound(rollover);
                    clicked = true;
                }
                else
                {
                    clicked = false;
                }
                Engine.DrawTexture(tHovered, bounds.Position);
            }
        }
        else
        {
            if (pressed && !Engine.GetMouseButtonHeld(MouseButton.Left))
            {
                Engine.PlaySound(rollover);
                clicked = true;
            }
            else
            {
                clicked = false;
            }
            hovered = false;
            Engine.DrawTexture(tNormal, bounds.Position);
        }

    }

    public bool IsClicked()
    {
        return clicked;
    }
}
