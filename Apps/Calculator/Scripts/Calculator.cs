using Godot;
using System;

public class Calculator : Button {
    string number1 = "";
    string number2 = "";
    double currentResult = 0;
    string @operator = "";
    Label expressionThing;
    bool next = false;

    public override void _Ready() {
        base._Ready();
        expressionThing = GetNode<Label>("../Expression");
        // this is fine
        GetNode<Button>("../Clear").Connect("pressed", this, nameof(Clear));
        GetNode<Button>("../Undo").Connect("pressed", this, nameof(Backspace));
        GetNode<Button>("../Negate").Connect("pressed", this, nameof(Negate));
        GetNode<Button>("../Divide").Connect("pressed", this, nameof(Divide));
        GetNode<Button>("../Multiply").Connect("pressed", this, nameof(Multiply));
        GetNode<Button>("../Seven").Connect("pressed", this, nameof(Seven));
        GetNode<Button>("../Eight").Connect("pressed", this, nameof(Eight));
        GetNode<Button>("../Nine").Connect("pressed", this, nameof(Nine));
        GetNode<Button>("../Subtract").Connect("pressed", this, nameof(Subtract));
        GetNode<Button>("../Four").Connect("pressed", this, nameof(Four));
        GetNode<Button>("../Five").Connect("pressed", this, nameof(Five));
        GetNode<Button>("../Six").Connect("pressed", this, nameof(Six));
        GetNode<Button>("../Add").Connect("pressed", this, nameof(Add));
        GetNode<Button>("../One").Connect("pressed", this, nameof(One));
        GetNode<Button>("../Two").Connect("pressed", this, nameof(Two));
        GetNode<Button>("../Three").Connect("pressed", this, nameof(Three));
        GetNode<Button>("../Zero").Connect("pressed", this, nameof(Zero));
        GetNode<Button>("../Dot").Connect("pressed", this, nameof(Dot));
        Connect("pressed", this, nameof(Equal));
    }

    void Clear() {
        number1 = "";
        number2 = "";
        currentResult = 0;
        @operator = "";
        expressionThing.Text = "";
        next = false;
    }

    void Backspace() {
        // this is ridiculous
        if (next) {
            if (number2 == "")
                return;

            number2 = number2.Remove(number2.Length-1, 1);
            if (number2.EndsWith("."))
                number2 = number2.Remove(number2.Length-1, 1);
            expressionThing.Text = number2;
        } else {
            if (number1 == "")
                return;

            number1 = number1.Remove(number1.Length-1, 1);
            if (number1.EndsWith("."))
                number1 = number1.Remove(number1.Length-1, 1);
            expressionThing.Text = number1;
        }
    }

    void Negate() {
        if (next) {
            double hjkgjkfgjhgekg;
            try {
                hjkgjkfgjhgekg = double.Parse(number2);
            } catch {
                expressionThing.Text = "Syntax Error";
                number2 = "";
                return;
            }
            number2 = (-hjkgjkfgjhgekg).ToString();
            expressionThing.Text = number2;
        } else {
            double hjkgjkfgjhgekg;
            try {
                hjkgjkfgjhgekg = double.Parse(number1);
            } catch {
                expressionThing.Text = "Syntax Error";
                number1 = "";
                return;
            }
            number1 = (-hjkgjkfgjhgekg).ToString();
            expressionThing.Text = number1;
        }
    }

    void Divide() {
        next = true;
        expressionThing.Text = "";

        if (number1 != "" && number2 != "") {
            Equal();
        }

        @operator = "/";
    }

    void Multiply() {
        next = true;
        expressionThing.Text = "";

        if (number1 != "" && number2 != "") {
            Equal();
        }

        @operator = "*";
    }

    void Seven() {
        if (next) {
            number2 += "7";
            expressionThing.Text = number2;
        } else {
            number1 += "7";
            expressionThing.Text = number1;
        }
    }

    void Eight() {
        if (next) {
            number2 += "8";
            expressionThing.Text = number2;
        } else {
            number1 += "8";
            expressionThing.Text = number1;
        }
    }

    void Nine() {
        if (next) {
            number2 += "9";
            expressionThing.Text = number2;
        } else {
            number1 += "9";
            expressionThing.Text = number1;
        }
    }

    void Subtract() {
        next = true;
        expressionThing.Text = "";

        if (number1 != "" && number2 != "") {
            Equal();
        }

        @operator = "-";
    }

    void Four() {
        if (next) {
            number2 += "4";
            expressionThing.Text = number2;
        } else {
            number1 += "4";
            expressionThing.Text = number1;
        }
    }

    void Five() {
        if (next) {
            number2 += "5";
            expressionThing.Text = number2;
        } else {
            number1 += "5";
            expressionThing.Text = number1;
        }
    }

    void Six() {
        if (next) {
            number2 += "6";
            expressionThing.Text = number2;
        } else {
            number1 += "6";
            expressionThing.Text = number1;
        }
    }

    void Add() {
        next = true;
        expressionThing.Text = "";

        if (number1 != "" && number2 != "") {
            Equal();
        }

        @operator = "+";
    }

    void One() {
        if (next) {
            number2 += "1";
            expressionThing.Text = number2;
        } else {
            number1 += "1";
            expressionThing.Text = number1;
        }
    }

    void Two() {
        if (next) {
            number2 += "2";
            expressionThing.Text = number2;
        } else {
            number1 += "2";
            expressionThing.Text = number1;
        }
    }

    void Three() {
        if (next) {
            number2 += "3";
            expressionThing.Text = number2;
        } else {
            number1 += "3";
            expressionThing.Text = number1;
        }
    }

    void Zero() {
        if (next) {
            number2 += "0";
            expressionThing.Text = number2;
        } else {
            number1 += "0";
            expressionThing.Text = number1;
        }
    }

    void Dot() {
        if (next) {
            number2 += ".";
            expressionThing.Text = number2;
        } else {
            number1 += ".";
            expressionThing.Text = number1;
        }
    }

    void Equal() {
        double one;
        double two;
        try {
            one = double.Parse(number1);
            two = double.Parse(number2);
        } catch {
            Clear();
            expressionThing.Text = "Syntax Error";
            return;
        }

        switch (@operator) {
            case "+":
                currentResult = one + two;
                number1 = currentResult.ToString();
                number2 = "";
                expressionThing.Text = $"{currentResult}";
                break;
            case "-":
                currentResult = one - two;
                number1 = currentResult.ToString();
                number2 = "";
                expressionThing.Text = $"{currentResult}";
                break;
            case "*":
                currentResult = one * two;
                number1 = currentResult.ToString();
                number2 = "";
                expressionThing.Text = $"{currentResult}";
                break;
            case "/":
                if (number2 == "0") {
                    Clear();
                    expressionThing.Text = "nan";
                    break;
                }

                currentResult = one / two;
                number1 = currentResult.ToString();
                number2 = "";
                expressionThing.Text = $"{currentResult}";
                break;
        }
    }
}
