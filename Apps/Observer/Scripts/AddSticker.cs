using Godot;

public partial class AddSticker : TextureButton
{
    public string TexturePath;

    public override void _Pressed()
    {
        base._Pressed();
        Texture2D texture = GetNode<TextureRect>("../Image").Texture;

        // save the sticker
        var pinboard = SavingManager.Load<LelsktopPinboard>(SavingManager.CurrentUser);
        var stickerdbgfdf = new PinboardItem
        {
            TexturePath = TexturePath,
            Position = ResolutionManager.Resolution/2
        };
        
        // yes.
        if (texture.GetSize() > ResolutionManager.Resolution)
            stickerdbgfdf.Scale = 0.2f;

        string bullshit = LelfsManager.GenerateID();
        pinboard.Items.Add(bullshit, stickerdbgfdf);
        SavingManager.Save(SavingManager.CurrentUser, pinboard);

        // add it and stuff :)
        var ftgkvtfyu = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Sticker.tscn");
        var sticker = ftgkvtfyu.Instantiate<Sticker>();
        sticker.Position = stickerdbgfdf.Position;
        sticker.Texture = texture;
        sticker.PinboardItem = bullshit;
        sticker.Scale = new Vector2(stickerdbgfdf.Scale, stickerdbgfdf.Scale);
        GetNode("/root/Lelsktop/Pinboard").AddChild(sticker);

        var notifications = GetNode<NotificationManager>("/root/NotificationManager");
        notifications.ShowNotification("A sticker has been added to your pinboard.");
    }
}
