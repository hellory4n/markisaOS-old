using Godot;
using System;
using System.Collections.Generic;

public class BlockGame : Control {
    readonly Texture TPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/TPiece.png");
    readonly Texture LPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/LPiece.png");
    readonly Texture SPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/SPiece.png");
    readonly Texture SquarePiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/SquarePiece.png");
    readonly Texture StraightPiece = ResourceLoader.Load<Texture>("res://Apps/BlockGame/Assets/StraightPiece.png");
    enum Pieces {
        T,
        L,
        S,
        Square,
        Straight
    }
    List<TextureRect> fallingPieces = new List<TextureRect>();
    int thingToMakeItNotGoTooMuchToTheRight = 0;
    int rotateThing = 0;

    public override void _Ready() {
        base._Ready();
        thingToMakeItNotGoTooMuchToTheRight = GetThingForTheVariableWhoseNameIsTooLong(Pieces.T);
        foreach (var h in MakePiece(Pieces.T, 0, new Vector2(80, -60))) {
            AddChild(h);
            fallingPieces.Add(h);
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);

        // help
        if (Input.IsActionJustReleased("ui_left") && GetParent<BaseWindow>().IsActive())
            MoveLeft();
        if (Input.IsActionJustReleased("ui_right") && GetParent<BaseWindow>().IsActive())
            MoveRight();

        if (Input.IsActionJustReleased("ui_up") && GetParent<BaseWindow>().IsActive()) {
            rotateThing++;
            if (rotateThing > 3)
                rotateThing = 0;
            
            // replace the old pieces :)
            List<TextureRect> uh = MakePiece(Pieces.T, rotateThing, fallingPieces[0].RectPosition);
            foreach (var sucks in fallingPieces) {
                sucks.QueueFree();
            }
            fallingPieces = uh;
            foreach (var hfhjdfsdjsfj in fallingPieces) {
                AddChild(hfhjdfsdjsfj);
            }
        }
    }

    public void MakeStuffFall() {
        foreach (var dfgmhe in fallingPieces) {
            dfgmhe.RectPosition += new Vector2(0, 20);
        }
    }

    public void MoveLeft() {
        if (fallingPieces[0].RectPosition.x == 0)
            return;

        foreach (var dfgmhe in fallingPieces) {
            dfgmhe.RectPosition -= new Vector2(20, 0);
            dfgmhe.Name = $"{dfgmhe.RectPosition.x/20},{dfgmhe.RectPosition.y/20}";
        }
    }

    public void MoveRight() {
        if (fallingPieces[0].RectPosition.x == thingToMakeItNotGoTooMuchToTheRight)
            return;

        foreach (var dfgmhe in fallingPieces) {
            dfgmhe.RectPosition += new Vector2(20, 0);
        }
    }

    List<TextureRect> MakePiece(Pieces piece, int rotationThing, Vector2 position) {
        List<TextureRect> m = new List<TextureRect>();
        // this is ridiculous lol
        switch (piece) {
            case Pieces.T:
                switch (rotationThing) {
                    case 0:
                        var t1r0 = new TextureRect {
                            RectPosition = position,
                            Texture = TPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t2r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t3r0 = new TextureRect {
                            RectPosition = position + new Vector2(40, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+2},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t4r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            t1r0,
                            t2r0,
                            t3r0,
                            t4r0
                        });
                        break;
                    case 1:
                        var t1r1 = new TextureRect {
                            RectPosition = position - new Vector2(0, -20),
                            Texture = TPiece,
                            Name = $"{position.x/20},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t2r1 = new TextureRect {
                            RectPosition = position,
                            Texture = TPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t3r1 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t4r1 = new TextureRect {
                            RectPosition = position + new Vector2(-20, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)-1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            t1r1,
                            t2r1,
                            t3r1,
                            t4r1
                        });
                        break;
                    case 2:
                        var t1r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, -20),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t2r2 = new TextureRect {
                            RectPosition = position,
                            Texture = TPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t3r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t4r2 = new TextureRect {
                            RectPosition = position + new Vector2(40, 0),
                            Texture = TPiece,
                            Name = $"{(position.x/20)+2},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            t1r2,
                            t2r2,
                            t3r2,
                            t4r2
                        });
                        break;
                    case 3:
                        var t1r3 = new TextureRect {
                            RectPosition = position - new Vector2(20, 20),
                            Texture = TPiece,
                            Name = $"{(position.x/20)-1},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t2r3 = new TextureRect {
                            RectPosition = position,
                            Texture = TPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t3r3 = new TextureRect {
                            RectPosition = position - new Vector2(0, 20),
                            Texture = TPiece,
                            Name = $"{position.x/20},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var t4r3 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = TPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            t1r3,
                            t2r3,
                            t3r3,
                            t4r3
                        });
                        break;
                }
                break;
            case Pieces.L:
                switch (rotationThing) {
                    case 0:
                        var l1r0 = new TextureRect {
                            RectPosition = position,
                            Texture = LPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l2r0 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = LPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l3r0 = new TextureRect {
                            RectPosition = position + new Vector2(0, 40),
                            Texture = LPiece,
                            Name = $"{position.x/20},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l4r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 40),
                            Texture = LPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            l1r0,
                            l2r0,
                            l3r0,
                            l4r0
                        });
                        break;
                    case 2:
                        var l1r2 = new TextureRect {
                            RectPosition = position,
                            Texture = LPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l2r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = LPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l3r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = LPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l4r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 40),
                            Texture = LPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            l1r2,
                            l2r2,
                            l3r2,
                            l4r2
                        });
                        break;
                    case 3:
                        var l1r3 = new TextureRect {
                            RectPosition = position + new Vector2(-20, 20),
                            Texture = LPiece,
                            Name = $"{(position.x/20)-1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l2r3 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = LPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l3r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = LPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var l4r3 = new TextureRect {
                            RectPosition = position + new Vector2(-20, 40),
                            Texture = LPiece,
                            Name = $"{(position.x/20)-1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            l1r3,
                            l2r3,
                            l3r3,
                            l4r3
                        });
                        break;
                }
                break;
            case Pieces.S:
                switch (rotationThing) {
                    case 0:
                        var s1r0 = new TextureRect {
                            RectPosition = position,
                            Texture = SPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s2r0 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s3r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s4r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 40),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            s1r0,
                            s2r0,
                            s3r0,
                            s4r0
                        });
                        break;
                    case 1:
                        var s1r1 = new TextureRect {
                            RectPosition = position - new Vector2(20, 40),
                            Texture = SPiece,
                            Name = $"{(position.x/20)-1},{(position.y/40)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s2r1 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s3r1 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s4r1 = new TextureRect {
                            RectPosition = position + new Vector2(0, 40),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            s1r1,
                            s2r1,
                            s3r1,
                            s4r1
                        });
                        break;
                    case 2:
                        var s1r2 = new TextureRect {
                            RectPosition = position,
                            Texture = SPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s2r2 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s3r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s4r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 40),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            s1r2,
                            s2r2,
                            s3r2,
                            s4r2
                        });
                        break;
                    case 3:
                        var s1r3 = new TextureRect {
                            RectPosition = position - new Vector2(20, 40),
                            Texture = SPiece,
                            Name = $"{(position.x/20)-1},{(position.y/40)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s2r3 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s3r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = SPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var s4r3 = new TextureRect {
                            RectPosition = position + new Vector2(0, 40),
                            Texture = SPiece,
                            Name = $"{position.x/20},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            s1r3,
                            s2r3,
                            s3r3,
                            s4r3
                        });
                        break;
                }
                break;
            case Pieces.Square:
                var square1 = new TextureRect {
                    RectPosition = position,
                    Texture = SquarePiece,
                    Name = $"{position.x/20},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var square2 = new TextureRect {
                    RectPosition = position + new Vector2(20, 0),
                    Texture = SquarePiece,
                    Name = $"{(position.x/20)+1},{position.y/20}",
                    RectSize = new Vector2(20, 20)
                };
                var square3 = new TextureRect {
                    RectPosition = position + new Vector2(0, 20),
                    Texture = SquarePiece,
                    Name = $"{position.x/20},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                var square4 = new TextureRect {
                    RectPosition = position + new Vector2(20, 20),
                    Texture = SquarePiece,
                    Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                    RectSize = new Vector2(20, 20)
                };
                m.AddRange(new TextureRect[]{
                    square1,
                    square2,
                    square3,
                    square4
                });
                break;
            case Pieces.Straight:
                switch (rotationThing) {
                    case 0:
                        var straight1r0 = new TextureRect {
                            RectPosition = position,
                            Texture = StraightPiece,
                            Name = $"{position.x/20},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight2r0 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight3r0 = new TextureRect {
                            RectPosition = position + new Vector2(40, 0),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight4r0 = new TextureRect {
                            RectPosition = position + new Vector2(60, 0),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+3},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            straight1r0,
                            straight2r0,
                            straight3r0,
                            straight4r0
                        });
                        break;
                    case 1:
                        var straight1r1 = new TextureRect {
                            RectPosition = position + new Vector2(40, -20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight2r1 = new TextureRect {
                            RectPosition = position + new Vector2(40, 0),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight3r1 = new TextureRect {
                            RectPosition = position + new Vector2(40, 20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight4r1 = new TextureRect {
                            RectPosition = position + new Vector2(40, 40),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            straight1r1,
                            straight2r1,
                            straight3r1,
                            straight4r1
                        });
                        break;
                    case 2:
                        var straight1r2 = new TextureRect {
                            RectPosition = position + new Vector2(0, 20),
                            Texture = StraightPiece,
                            Name = $"{position.x/20},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight2r2 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight3r2 = new TextureRect {
                            RectPosition = position + new Vector2(40, 20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+2},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight4r2 = new TextureRect {
                            RectPosition = position + new Vector2(60, 20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+3},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            straight1r2,
                            straight2r2,
                            straight3r2,
                            straight4r2
                        });
                        break;
                    case 3:
                        var straight1r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, -20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)-1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight2r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, 0),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{position.y/20}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight3r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, 20),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+1}",
                            RectSize = new Vector2(20, 20)
                        };
                        var straight4r3 = new TextureRect {
                            RectPosition = position + new Vector2(20, 40),
                            Texture = StraightPiece,
                            Name = $"{(position.x/20)+1},{(position.y/20)+2}",
                            RectSize = new Vector2(20, 20)
                        };
                        m.AddRange(new TextureRect[]{
                            straight1r3,
                            straight2r3,
                            straight3r3,
                            straight4r3
                        });
                        break;
                }
                break;
        }
        return m;
    }

    int GetThingForTheVariableWhoseNameIsTooLong(Pieces piece) {
        switch (piece) {
            case Pieces.T:
                return 140;
            case Pieces.L:
                return 160;
            case Pieces.Square:
                return 160;
            case Pieces.S:
                return 160;
            case Pieces.Straight:
                return 120;
            default:
                return 69;
        }
    }
}
