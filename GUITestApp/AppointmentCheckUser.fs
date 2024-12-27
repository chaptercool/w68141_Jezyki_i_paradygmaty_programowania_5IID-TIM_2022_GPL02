module AppointmentCheckUser

open System
open System.Drawing
open System.Windows.Forms
open Microsoft.Data.SqlClient
open System.IO

let iconQuestion = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_99.ico"))
let iconInfo = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_81.ico"))


let public CheckWindowQuestion = new Form(
    Text = "Wprowadź swoje dane",
    Size = new Size(400, 200),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = false,
    ControlBox = false,
    BackColor = Color.White,
    Icon = iconQuestion
)

let layout = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 4,
    ColumnCount = 1,
    Padding = new Padding(10)
)

layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore

let InfoLabel = new Label(
    Text = "Wprowadź dane które byli użyte podczas rezerwacji wizyt",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill
)
layout.Controls.Add(InfoLabel, 0, 0)

let txtBox = new TextBox(
    Text = "",
    Multiline = false,
    Dock = DockStyle.Fill
)

let CheckUserWindow = new Form(
    Text = "Znaleziono wizyty",
    Size = new Size(800, 600),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = true,
    Icon = iconInfo,
    BackColor = Color.White
)

let queryLayout = new TableLayoutPanel(
    RowCount = 4,
    ColumnCount = 1,
    Dock = DockStyle.Fill,
    Padding = new Padding(10)
)

queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore
queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore
queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore


let queryLabel = new Label(
    Text = "Twoje najbliższe wizyty:",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill,
    Font = new Font("Arial", 24.0f, FontStyle.Bold)
)
queryLayout.Controls.Add(queryLabel, 0, 0)

let fetchUserAppointments userName =
    use connection = new SqlConnection(File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")))
    connection.Open()
    let query = """
        select l.FName, l.LName, p.PoradniaName, r.DataWizyty, r.Godzina from Rezerwacje as r
        inner join Lekarze as l
        on r.DrID = l.DrID inner join Poradnie as p
        on l.PoradniaID = p.PoradniaID
        where r.Zarezerwowane = 'TAK' and r.Pacjent = @Pacjent
    """
    let cmd = new SqlCommand(query, connection)
    cmd.Parameters.AddWithValue("@Pacjent", userName) |> ignore
    let reader = cmd.ExecuteReader()
    let results = new System.Collections.Generic.List<obj[]>()
    while reader.Read() do
        results.Add([|
            box(reader.["FName"].ToString() + " " + reader.["LName"].ToString())
            box(reader.["PoradniaName"].ToString())
            box(reader.["DataWizyty"].ToString())
            box(reader.["Godzina"].ToString())
        |])
    results


let SQLQueryResults = new DataGridView(
    Dock = DockStyle.Fill,
    Visible = true,
    AllowUserToAddRows = false,
    BackgroundColor = Color.White,
    ForeColor = Color.Black,
    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle(
        BackColor = Color.Navy,
        ForeColor = Color.White,
        Font = new Font("Arial", 12.0f, FontStyle.Bold)
    ),
    DefaultCellStyle = new DataGridViewCellStyle(
        BackColor = Color.White,
        ForeColor = Color.Black,
        SelectionBackColor = Color.Blue,
        SelectionForeColor = Color.White
    )
)
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Lekarz"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Poradnia"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Data Wizyty"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Godzina"))
queryLayout.Controls.Add(SQLQueryResults, 0, 1)

let CloseButton = new Button(
    Text = "Zakończ przegląd",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CloseButton.Click.Add(fun _ -> CheckUserWindow.Hide())
queryLayout.Controls.Add(CloseButton, 0, 2)

layout.Controls.Add(txtBox, 0, 1)

let CheckButton = new Button(
    Text = "Sprawdź",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CheckButton.Click.Add(fun _ -> 
    if txtBox.Text = "" then
       MessageBox.Show("Nie wprowadzone żadne dane", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) |> ignore
    else
        let ConnectionTestDialog = new Form(
            ControlBox = false,
            MinimizeBox = false,
            MaximizeBox = false,
            StartPosition = FormStartPosition.CenterScreen,
            Size = new Size(200, 150),
            Text = "Proszę czekać..."
        )

        let ConnectionTestLabel = new Label(
            Text = "Sprawdzenie połączenia z bazą danych...",
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top
        )
        ConnectionTestDialog.Controls.Add(ConnectionTestLabel)

        let PBar = new ProgressBar(
            Location = new Point(100, 75),
            Size = new Size(50, 25),
            Style = ProgressBarStyle.Marquee,
            Dock = DockStyle.Bottom
        )

        ConnectionTestDialog.Controls.Add(PBar)
        try
            ConnectionTestDialog.Show()
            let connectionString = File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt"))
            use connection = new SqlConnection(connectionString)
            connection.Open()
            CheckWindowQuestion.Hide()
            CheckUserWindow.Show()
            ConnectionTestDialog.Close()

            try
                let results = fetchUserAppointments(txtBox.Text)
                if results.Count > 0 then
                    SQLQueryResults.Rows.Clear()
                    for row in results do
                        SQLQueryResults.Rows.Add(row)
                else
                    MessageBox.Show("Nie znaleziono żadnych terminów. Niepoprawna pisownia lub na takie imię nie było składane żadne rezerwacje", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
                    CheckUserWindow.Hide()
            with
            | ex -> MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore

        with
        | ex -> MessageBox.Show("Błąd połączenia z bazą danych! Sprawdź, czy baza nie jest offline albo konfigurację połączenia w zakładce 'Inne'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
                ConnectionTestDialog.Close()
                CheckWindowQuestion.Hide()
)
layout.Controls.Add(CheckButton, 0, 2)

let CancelButton = new Button(
    Text = "Anuluj",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CancelButton.Click.Add(fun _ -> CheckWindowQuestion.Hide())
layout.Controls.Add(CancelButton, 0, 3)
CheckWindowQuestion.Controls.Add(layout)
CheckUserWindow.Controls.Add(queryLayout)


//
