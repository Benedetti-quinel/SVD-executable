namespace VisualDebugger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            paylineList = new ListBox();
            additionalDataView = new DataGridView();
            tabControl1 = new TabControl();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)additionalDataView).BeginInit();
            SuspendLayout();
            // 
            // paylineList
            // 
            paylineList.BackColor = Color.FromArgb(34, 36, 43);
            paylineList.BorderStyle = BorderStyle.None;
            paylineList.Font = new Font("Arial Rounded MT Bold", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            paylineList.ForeColor = Color.White;
            paylineList.FormattingEnabled = true;
            paylineList.ItemHeight = 15;
            paylineList.Location = new Point(824, 45);
            paylineList.Name = "paylineList";
            paylineList.Size = new Size(176, 495);
            paylineList.TabIndex = 0;
            paylineList.SelectedIndexChanged += paylineList_SelectedIndexChanged;
            // 
            // additionalDataView
            // 
            additionalDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            additionalDataView.BackgroundColor = Color.FromArgb(34, 36, 43);
            additionalDataView.BorderStyle = BorderStyle.None;
            additionalDataView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(24, 24, 30);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.Padding = new Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(24, 24, 30);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            additionalDataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            additionalDataView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(34, 36, 43);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.Padding = new Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(26, 60, 72);
            dataGridViewCellStyle2.SelectionForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            additionalDataView.DefaultCellStyle = dataGridViewCellStyle2;
            additionalDataView.Dock = DockStyle.Bottom;
            additionalDataView.EnableHeadersVisualStyles = false;
            additionalDataView.GridColor = Color.FromArgb(24, 24, 30);
            additionalDataView.Location = new Point(0, 626);
            additionalDataView.Name = "additionalDataView";
            additionalDataView.ReadOnly = true;
            additionalDataView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(24, 24, 30);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            additionalDataView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            additionalDataView.RowHeadersVisible = false;
            additionalDataView.RowTemplate.DefaultCellStyle.ForeColor = Color.WhiteSmoke;
            additionalDataView.RowTemplate.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
            additionalDataView.RowTemplate.Height = 25;
            additionalDataView.ScrollBars = ScrollBars.None;
            additionalDataView.ShowEditingIcon = false;
            additionalDataView.Size = new Size(1000, 124);
            additionalDataView.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Left;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(5, 5, 0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(824, 626);
            tabControl1.TabIndex = 2;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(34, 36, 43);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Gray;
            button1.Location = new Point(960, 0);
            button1.Name = "button1";
            button1.Size = new Size(40, 34);
            button1.TabIndex = 3;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseLeave += button1_MouseLeave;
            button1.MouseHover += button1_MouseHover;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(114, 9, 183);
            label1.Location = new Point(889, 543);
            label1.Name = "label1";
            label1.Size = new Size(111, 37);
            label1.TabIndex = 4;
            label1.Text = "- Sticky";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(6, 214, 160);
            label2.Location = new Point(889, 586);
            label2.Name = "label2";
            label2.Size = new Size(103, 37);
            label2.TabIndex = 5;
            label2.Text = "- Mask";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(34, 36, 43);
            ClientSize = new Size(1000, 750);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(tabControl1);
            Controls.Add(additionalDataView);
            Controls.Add(paylineList);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Slot Visual Debugger";
            MouseDown += Form1_MouseDown;
            ((System.ComponentModel.ISupportInitialize)additionalDataView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private ListBox paylineList;
        private DataGridView additionalDataView;
        private TabControl tabControl1;
        private Button button1;
        private Label label1;
        private Label label2;
    }
}