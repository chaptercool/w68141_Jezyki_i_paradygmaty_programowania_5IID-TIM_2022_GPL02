module GuiExample

open System.Drawing
open System.Windows.Forms
open System

let f = new Form()
//f.BackColor <- Color.BlueViolet
f.MinimizeBox <- true
f.MaximizeBox <- false
f.StartPosition <- FormStartPosition.CenterScreen
f.Size <- new Size(500, 700)

let button  = new Button(Text = "Close app")
button.Click.Add(fun _ -> f.Close())
button.ForeColor <- Color.Black
button.BackColor <- Color.White
button.AutoSize <- true
button.Dock <- DockStyle.Bottom

let button2 = new Button(Text = "Message")
button2.Click.Add(fun _ -> 
    MessageBox.Show("This is the information message.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
)
button2.ForeColor <- Color.Black
button2.BackColor <- Color.White
//button2.AutoSize <- true
button2.Dock <- DockStyle.Bottom

let button3 = new Button(Text = "Error")
button3.Click.Add(fun _ -> 
    MessageBox.Show("This is the error message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
)
button3.ForeColor <- Color.Black
button3.BackColor <- Color.White
//button3.AutoSize <- true
button3.Dock <- DockStyle.Bottom

let textLabel = new Label(Text = "Hello World")
textLabel.ForeColor <- Color.Black
textLabel.Dock <- DockStyle.Top
textLabel.TextAlign <- ContentAlignment.MiddleCenter
textLabel.Font <- new Font("Arial", 20.0f)

f.Controls.Add(button)
f.Controls.Add(button2)
f.Controls.Add(button3)
f.Controls.Add(textLabel)

Application.EnableVisualStyles()
Application.Run(f)