module AdminPanel

open System.Drawing
open System.Windows.Forms
open System
open System.IO

let public AdminForm = new Form(
    Text = "Ustawienia systemowe",
    Size = new Size(400, 300),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = false,
    BackColor = Color.White
)

let layout = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 2,
    ColumnCount = 2,
    Padding = new Padding(20)
)

layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f)) |> ignore
layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f)) |> ignore

let txtLabel = new Label(
    Text = "Konfig. bazy"
)
layout.Controls.Add(txtLabel, 0, 0)

let readFileAsText (filePath: string) = 
    File.ReadAllText(filePath)

let content = readFileAsText (Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt"))

let txtBox = new TextBox(
    Text = content
)

layout.Controls.Add(txtBox, 1, 0)

let CancelButton = new Button(
    Text = "Anuluj",
    Size = new Size(100, 50),
    Anchor = AnchorStyles.None
)
CancelButton.Click.Add(fun _ -> AdminForm.Close())
layout.Controls.Add(CancelButton, 0, 1)

let SaveButton = new Button(
    Text = "Zapisz",
    Size = new Size(100, 50),
    Anchor = AnchorStyles.None
)

let newContent = txtBox.Text

SaveButton.Click.Add(fun _ ->
    let writeNewContent (filePath: string) (newContent: string) =
        try File.WriteAllText(filePath, newContent)
            MessageBox.Show("Zapisano ustawienia", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        with
            | ex -> MessageBox.Show("Wystąpił błąd podczas zapisywania konfiguracji", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
    writeNewContent (Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")) newContent |> ignore
    AdminForm.Close()
)
layout.Controls.Add(SaveButton, 1, 1)

AdminForm.Controls.Add(layout)