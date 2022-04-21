Public Class Form1
	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

	End Sub

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim table1 As New DataTable
		Dim strType As System.Type = System.Type.GetType("System.String")


		table1.Columns.AddRange(
			{
				New DataColumn("noname", strType),
				New DataColumn("Starbuck", strType),
				New DataColumn("Dunkin", strType),
				New DataColumn("Phin Coffee", strType),
				New DataColumn("Peet's Coffee", strType)
			}
		)

		table1.Rows.Add(New Object() {"Regular(Lg, Sm)", 2.55, 2.19, 2.75, 2.65})
		table1.Rows.Add(New Object() {"Regular(Lg, Sm)", 2.95, 2.79, 3.25, 3.5})
		table1.Rows.Add(New Object() {"Cold Brew(Lg, Sm)", 3.75, 3.19, 3.25, 3.4})
		table1.Rows.Add(New Object() {"Cold Brew(Lg, Sm)", 4.45, 3.79, 4.25, 4.65})
		table1.Rows.Add(New Object() {"Latte(Lg, Sm)", 3.85, 3.09, 4.25, 4.2})
		table1.Rows.Add(New Object() {"Latte(Lg, Sm)", 4.45, 3.89, 4.75, 5.45})
		table1.Rows.Add(New Object() {"Ice latte(Lg, Sm)", 3.95, 3.59, 4.25, 4.2})
		table1.Rows.Add(New Object() {"Ice latte(Lg, Sm)", 5.25, 4.49, 4.75, 5.45})

		DataGridView1.DataSource = table1

		Dim col As DataGridViewMergedTextBoxColumn = New DataGridViewMergedTextBoxColumn()
		Const field = "noname"
		col.HeaderText = field
		col.Name = field
		col.DataPropertyName = field
		Dim colidx As Int32 = DataGridView1.Columns(field).Index
		DataGridView1.Columns.Remove(field)
		col.HeaderText = ""
		DataGridView1.Columns.Insert(colidx, col)

		For Each col2 As DataGridViewColumn In DataGridView1.Columns
			col2.SortMode = DataGridViewColumnSortMode.NotSortable
			col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
		Next

		With Me.DataGridView1
			.RowsDefaultCellStyle.BackColor = Color.LightBlue
			.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige
		End With

		With Me.DataGridView1.Columns(0)
			.ReadOnly = True
			.Frozen = True
			.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
		End With

	End Sub
End Class
