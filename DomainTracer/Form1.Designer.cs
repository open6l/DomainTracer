namespace DomainTracer
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_ipOrDomain = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView_Left = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listView_Right = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_save2csv = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.textBox_interval = new System.Windows.Forms.TextBox();
            this.btnSeeLastRow = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaGreen;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1106, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 54);
            this.label1.TabIndex = 2;
            this.label1.Text = "Domain Tracer";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(17, 11);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(55, 42);
            this.btnStartStop.TabIndex = 4;
            this.btnStartStop.Text = "▶";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(258, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "대상 추가";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_ipOrDomain
            // 
            this.textBox_ipOrDomain.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ipOrDomain.ForeColor = System.Drawing.Color.Gray;
            this.textBox_ipOrDomain.Location = new System.Drawing.Point(3, 9);
            this.textBox_ipOrDomain.Name = "textBox_ipOrDomain";
            this.textBox_ipOrDomain.Size = new System.Drawing.Size(249, 43);
            this.textBox_ipOrDomain.TabIndex = 0;
            this.textBox_ipOrDomain.Text = "google.com";
            this.textBox_ipOrDomain.Click += new System.EventHandler(this.textBox_ipOrDomain_Click);
            this.textBox_ipOrDomain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_ipOrDomain_KeyUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1106, 542);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView_Left);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView_Right);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1106, 542);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.TabIndex = 4;
            // 
            // listView_Left
            // 
            this.listView_Left.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Left.FullRowSelect = true;
            this.listView_Left.Location = new System.Drawing.Point(0, 81);
            this.listView_Left.Name = "listView_Left";
            this.listView_Left.Size = new System.Drawing.Size(397, 461);
            this.listView_Left.TabIndex = 6;
            this.listView_Left.UseCompatibleStateImageBehavior = false;
            this.listView_Left.View = System.Windows.Forms.View.Details;
            this.listView_Left.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_Left_ItemSelectionChanged);
            this.listView_Left.SelectedIndexChanged += new System.EventHandler(this.listView_Left_SelectedIndexChanged);
            this.listView_Left.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView_Left_KeyUp);
            this.listView_Left.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_Left_MouseClick);
            this.listView_Left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_Left_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "도메인";
            this.columnHeader1.Width = 346;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.textBox_ipOrDomain);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(397, 81);
            this.panel4.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "모두보기";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // listView_Right
            // 
            this.listView_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Right.FullRowSelect = true;
            this.listView_Right.Location = new System.Drawing.Point(0, 81);
            this.listView_Right.Name = "listView_Right";
            this.listView_Right.Size = new System.Drawing.Size(705, 461);
            this.listView_Right.TabIndex = 5;
            this.listView_Right.UseCompatibleStateImageBehavior = false;
            this.listView_Right.View = System.Windows.Forms.View.Details;
            this.listView_Right.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_Right_ColumnClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_save2csv);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.textBox_interval);
            this.panel3.Controls.Add(this.btnSeeLastRow);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnStartStop);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(705, 81);
            this.panel3.TabIndex = 4;
            // 
            // button_save2csv
            // 
            this.button_save2csv.Location = new System.Drawing.Point(238, 8);
            this.button_save2csv.Name = "button_save2csv";
            this.button_save2csv.Size = new System.Drawing.Size(62, 43);
            this.button_save2csv.TabIndex = 10;
            this.button_save2csv.Text = "CSV";
            this.button_save2csv.UseVisualStyleBackColor = true;
            this.button_save2csv.Click += new System.EventHandler(this.button_save2csv_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Location = new System.Drawing.Point(229, 7);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(3, 66);
            this.panel8.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(80, 8);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(3, 66);
            this.panel7.TabIndex = 8;
            // 
            // textBox_interval
            // 
            this.textBox_interval.Location = new System.Drawing.Point(92, 50);
            this.textBox_interval.Name = "textBox_interval";
            this.textBox_interval.Size = new System.Drawing.Size(29, 21);
            this.textBox_interval.TabIndex = 9;
            this.textBox_interval.Text = "60";
            // 
            // btnSeeLastRow
            // 
            this.btnSeeLastRow.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSeeLastRow.Location = new System.Drawing.Point(165, 8);
            this.btnSeeLastRow.Name = "btnSeeLastRow";
            this.btnSeeLastRow.Size = new System.Drawing.Size(57, 43);
            this.btnSeeLastRow.TabIndex = 8;
            this.btnSeeLastRow.Text = "↓";
            this.btnSeeLastRow.UseVisualStyleBackColor = true;
            this.btnSeeLastRow.Click += new System.EventHandler(this.btnSeeLastRow_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(8, 9);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(3, 66);
            this.panel6.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(156, 8);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 66);
            this.panel5.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "내보내기";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "끝행 보기";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "시간 간격";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "초";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "시작/중지";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 642);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Domain Tracer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_ipOrDomain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView_Left;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView listView_Right;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSeeLastRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_interval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_save2csv;
        private System.Windows.Forms.Label label6;
    }
}

