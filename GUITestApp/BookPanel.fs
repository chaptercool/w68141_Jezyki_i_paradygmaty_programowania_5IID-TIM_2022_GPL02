module BookPanel

open System
open Microsoft.Data.SqlClient
open System.Drawing
open System.Windows.Forms
open System.IO

let connectionString = File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt"))

type Poradnia = {
    Id: int
    Name: string
}

let getPoradnie() =
    use connection = new SqlConnection(connectionString)
    connection.Open()
    let cmd = new SqlCommand("SELECT PoradniaID, PoradniaName FROM Poradnie", connection)
    let reader = cmd.ExecuteReader()
    let poradnieList = new System.Collections.Generic.List<Poradnia>()
    while reader.Read() do
        poradnieList.Add({ Id = reader.["PoradniaID"] :?> int; Name = reader.["PoradniaName"].ToString() })
    poradnieList

let fetchAvailableSlots poradniaID date =
    use connection = new SqlConnection(connectionString)
    connection.Open()
    let query = """
        SELECT L.FName, L.LName, R.DataWizyty, R.Godzina, R.TerminID 
        FROM Rezerwacje R
        JOIN Lekarze L ON R.DrID = L.DrID
        WHERE R.Zarezerwowane = 'NIE' AND R.Pacjent IS NULL AND L.PoradniaID = @PoradniaID AND R.DataWizyty = @Date
    """
    let cmd = new SqlCommand(query, connection)
    cmd.Parameters.AddWithValue("@PoradniaID", poradniaID) |> ignore
    cmd.Parameters.AddWithValue("@Date", date) |> ignore
    let reader = cmd.ExecuteReader()
    let results = new System.Collections.Generic.List<obj[]>()
    while reader.Read() do
        results.Add([|
            box(reader.["TerminID"] :?> int)
            box(reader.["FName"].ToString() + " " + reader.["LName"].ToString())
            box(reader.["Godzina"].ToString())
        |])
    results

let bookSlot (appointmentID: int) (patientName: string) =
    use connection = new SqlConnection(connectionString)
    connection.Open()
    let query = "UPDATE Rezerwacje SET Zarezerwowane = 'TAK', Pacjent = @Pacjent WHERE TerminID = @TerminID"
    let cmd = new SqlCommand(query, connection)
    cmd.Parameters.AddWithValue("@Pacjent", patientName) |> ignore
    cmd.Parameters.AddWithValue("@TerminID", appointmentID) |> ignore
    cmd.ExecuteNonQuery() |> ignore

let public BookWindow = new Form(
    Text = "Rezerwacja wizyty",
    Size = new Size(900, 700),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = true,
    BackColor = Color.White
)

let DockbarTop = new TableLayoutPanel(
    Dock = DockStyle.Top,
    ColumnCount = 4,
    RowCount = 1,
    Padding = new Padding(20)
)
DockbarTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore
DockbarTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore
DockbarTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore
DockbarTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore

let TypeLabel = new Label(
    Text = "Poradnia:",
    Dock = DockStyle.Right
)
DockbarTop.Controls.Add(TypeLabel, 0, 0)

let DropdownType = new ComboBox(
    Dock = DockStyle.Left,
    DropDownStyle = ComboBoxStyle.DropDownList
)
DropdownType.DisplayMember <- "Name"
DropdownType.ValueMember <- "Id"
DropdownType.DataSource <- getPoradnie().ToArray()
DockbarTop.Controls.Add(DropdownType, 1, 0)

let DateLabel = new Label(
    Text = "Data:",
    Dock = DockStyle.Right
)
DockbarTop.Controls.Add(DateLabel, 2, 0)

let DateInput = new DateTimePicker(
    Dock = DockStyle.Left,
    Format = DateTimePickerFormat.Custom,
    CustomFormat = "dd-MM-yyyy"
)
DockbarTop.Controls.Add(DateInput, 3, 0)

let SQLQueryResults = new DataGridView(
    Dock = DockStyle.Fill,
    Visible = false,
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

SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "TerminID", Visible = false))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Lekarz"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Godzina"))

let DockbarBottom = new TableLayoutPanel(
    Dock = DockStyle.Bottom,
    ColumnCount = 3,
    RowCount = 1
)
DockbarBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f)) |> ignore
DockbarBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f)) |> ignore
DockbarBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f)) |> ignore

let CancelButton = new Button(
    Text = "Anuluj",
    Dock = DockStyle.Fill
)
CancelButton.Click.Add(fun _ -> BookWindow.Hide())
DockbarBottom.Controls.Add(CancelButton, 0, 0)

let FindButton = new Button(
    Text = "Szukaj",
    Dock = DockStyle.Fill
)
FindButton.Click.Add(fun _ ->
    try
        let poradniaID = int (DropdownType.SelectedValue.ToString())
        let date = DateInput.Value.Date
        let results = fetchAvailableSlots poradniaID date
        if results.Count > 0 then
            SQLQueryResults.Rows.Clear()
            for row in results do
                SQLQueryResults.Rows.Add(row)
            SQLQueryResults.Visible <- true
        else
            MessageBox.Show("Brak dostępnych terminów (Zapraszamy do medicover)", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
    with
    | ex -> MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
)

DockbarBottom.Controls.Add(FindButton, 1, 0)

let BookButton = new Button(
    Text = "Zarezerwuj",
    Dock = DockStyle.Fill
)
BookButton.Click.Add(fun _ ->
    if SQLQueryResults.SelectedRows.Count > 0 then
        let selectedRow = SQLQueryResults.SelectedRows.[0]
        match selectedRow.Cells.["TerminID"].Value with
        | :? int as appointmentID ->
            let form = new Form(Text = "Wpisz swoje dane")
            let textBox = new TextBox(Dock = DockStyle.Top)
            let buttonOk = new Button(Text = "Zarezerwuj", Dock = DockStyle.Bottom)
            let buttonCancel = new Button(Text = "Anuluj", Dock = DockStyle.Bottom)
            form.MaximizeBox <- false
            form.MinimizeBox <- false
            form.StartPosition <- FormStartPosition.CenterScreen
            form.Controls.Add(textBox)
            form.Controls.Add(buttonOk)
            form.Controls.Add(buttonCancel)

            let mutable patientName = ""
            buttonOk.Click.Add(fun _ ->
                patientName <- textBox.Text
                form.DialogResult <- DialogResult.OK
                form.Close()
            )
            buttonCancel.Click.Add(fun _ -> form.Close())

            let result = form.ShowDialog()
            if result = DialogResult.OK then
                if not (String.IsNullOrEmpty(patientName)) then
                    bookSlot appointmentID patientName
                    MessageBox.Show("Zarezerwowano wizytę!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
                    BookWindow.Hide()
                else
                    MessageBox.Show("Proszę wprowadzić swoje imię i nazwisko.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        | _ -> MessageBox.Show("Nieprawidłowy TerminID.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
    else
        MessageBox.Show("Proszę zaznaczyć termin do rezerwacji.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
)

DockbarBottom.Controls.Add(BookButton, 2, 0)

BookWindow.Controls.Add(DockbarTop)
BookWindow.Controls.Add(SQLQueryResults)
BookWindow.Controls.Add(DockbarBottom)