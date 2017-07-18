using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Data.SQLite;
using System.Collections;

namespace DomainTracer
{
    //var address = Dns.GetHostAddresses(inputText)[0];
    public partial class Form1 : Form
    {
        private ListViewItem _selectedItem = null;
        private string initText = "google.com";
        private static SQLiteConnection conn = null;
        private string DataBaseFilename = "IPLogger.db";
        private bool isTimer1Running = false;
        private bool isFollowLastRow = false;

        public class Row
        {
            public string DateTime;
            public string DomainName;
            public string IPAddr;
            public long RoundTripTime;
            public int TimeToLive;
            public bool DonotFragment;
            public bool isPingOK;
            public string[] ToStringArray()
            {
                string ping = (isPingOK == true) ? "성공" : "실패";
                return new string[] { DateTime, DomainName, IPAddr, ping, RoundTripTime.ToString(), TimeToLive.ToString(), DonotFragment.ToString() };
            }
            public string ToStringCSV()
            {
                string ping = (isPingOK == true) ? "성공" : "실패";
                return string.Format("{0},{1},{2},{3},{4},{5}", DateTime, DomainName, IPAddr, ping, RoundTripTime.ToString(), TimeToLive.ToString());
            }
            public int GetPingOK_ToNumber()
            {
                return (this.isPingOK == true) ? 1 : 0;
            }

            public void SetPingOK(int value)
            {
                this.isPingOK = (value == 1) ? true : false;
            }
            public void SetPingOK(bool value)
            {
                this.isPingOK = value;
            }
            public int GetDonotFragment_ToNumber()
            {
                return (this.DonotFragment == true) ? 1 : 0;
            }
            public void SetDonotFragment(bool value)
            {
                this.DonotFragment = value;
            }
            public void SetDonotFragment(int value)
            {
                this.DonotFragment = (value == 1) ? true : false;
            }

            internal static string GetCSVHeader()
            {
                return string.Format("일시,도메인네임,IP,PING,RTL,TTL");
            }
        }

        class ListViewItemComparer : IComparer
        {
            private int col;
            public string sort = "asc";
            public ListViewItemComparer()
            {
                col = 0;
            }

            /// <summary>
            /// 컬럼과 정렬 기준(asc, desc)을 사용하여 정렬 함.
            /// </summary>
            /// <param name="column">몇 번째 컬럼인지를 나타냄.</param>
            /// <param name="sort">정렬 방법을 나타냄. Ex) asc, desc</param>
            public ListViewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                else
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }//class ListViewItemComparer : IComparer

        /// <summary>
        /// 생성자
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            listView_Left.View = View.Details;
            textBox_ipOrDomain.Text = initText;
            if (listView_Left.Columns.Count > 0)
            {
                listView_Left.Columns[0].Width = listView_Left.Width - 5;
            }

            listView_Right.Columns.Add("일시");
            listView_Right.Columns.Add("도메인");
            listView_Right.Columns.Add("IP주소");
            listView_Right.Columns.Add("Ping결과");
            listView_Right.Columns.Add("RTT(RoundTripTime)");
            listView_Right.Columns.Add("TTL(TimeToLive)");
            listView_Right.Columns.Add("DonotFragment");
            

            listView_Right.Columns[0].Width = 150;
            listView_Right.Columns[1].Width = 150;
            listView_Right.Columns[2].Width = 120;

            //데이터베이스 초기화1
            if ( ! File.Exists(DataBaseFilename))
            {
                SQLiteConnection.CreateFile(DataBaseFilename);
                conn = new SQLiteConnection("Data Source=" + DataBaseFilename + ";Version=3;");
                conn.Open();

                // 디비 초기화 ----------------------------------------------------
                try{
                    string sql01 = "CREATE TABLE domain_list ( " +
                    "no INTEGER primary key autoincrement" +
                    ",domainname varchar(10240) );";
                    SQLiteCommand command01 = new SQLiteCommand(sql01, conn);

                    command01.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    MessageBox.Show("데이터베이스 초기화 오류 01: " + ex.ToString());
                }

                // 디비 초기화 : google.com을 기본값으로 설정 ----------------------------------------------------
                try
                {

                    string sql01 = "INSERT INTO domain_list (domainname) VALUES ('google.com');";
                    SQLiteCommand command01 = new SQLiteCommand(sql01, conn);

                    int result01 = command01.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 초기화 오류 02: " + ex.ToString());
                }

                try
                {
                    //-----------------------------------------------------------------
                    // RTL : round trip time
                    // TTL : Time to Live
                    // DNF : Do Not Fragment
                    string sql02 = "create table pingtable (" +
                                    "no INTEGER primary key autoincrement" +
                                    ",datetime    VARCHAR(20)" +
                                    ",domainname  VARCHAR(8096)" +
                                    ",ip          VARCHAR(15)" +
                                    ",RTL         INTEGER" +
                                    ",TTL         INTEGER" +
                                    ",DNF         INTEGER" +
                                    ",ping_ok     INTEGER);";

                    SQLiteCommand command02 = new SQLiteCommand(sql02, conn);
                    command02.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 초기화 오류 03: " + ex.ToString());
                }
                

                conn.Close();
            }


            // ------------------------------------------------------------------
            // - 데이터베이스 초기화2
            // ------------------------------------------------------------------
            conn = new SQLiteConnection("Data Source=" + DataBaseFilename + ";Version=3;");
            conn.Open();


            // ------------------------------------------------------------------
            // - 왼쪽 리스트뷰 초기화
            // ------------------------------------------------------------------
            try
            {
                string sql = "SELECT domainname FROM domain_list order by domainname asc";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listView_Left.Items.Add(rdr["domainname"].ToString());
                    
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // ------------------------------------------------------------------
            // - 타이머 초기화
            // ------------------------------------------------------------------
            timer1.Tick += Timer1_Tick;


            // ------------------------------------------------------------------
            // - 시작시 왼족 리스트뷰 선택
            // ------------------------------------------------------------------
            if (listView_Left.Items.Count > 0) {
                listView_Left.Items[0].Selected = true;
                listView_Left.Select();
            }
        }

        ~Form1()
        {
            timer1.Stop();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 에러처리
            if (textBox_ipOrDomain.Text == initText)
                return;

            if (textBox_ipOrDomain.Text.Trim() == string.Empty)
                return;

            //TODO: IP주소나 도메인 주소가 아니면 에러
            string inputText = textBox_ipOrDomain.Text;

            foreach(ListViewItem item in this.listView_Left.Items)
            {
                if (item.Text == inputText)
                    return;
            }

            try
            {
                //sql 인젝션 금지
                inputText = inputText.Replace("\'", "").Replace("--", "").Replace("\"", "").Replace("/", "").Replace("#", "");
                string sql = "INSERT INTO domain_list (domainname) VALUES ('"+inputText+"')";
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                int result = command.ExecuteNonQuery();

                listView_Left.Items.Add(inputText);
                foreach(ListViewItem item in listView_Left.Items)
                {
                    item.Selected = false;
                }
                listView_Left.Items[listView_Left.Items.Count - 1].Selected = true;
                listView_Left.Select();

                textBox_ipOrDomain.Text = "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


        private void listView_Left_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem m1 = new MenuItem("삭제");
                MenuItem m2 = new MenuItem("---");
                MenuItem m3 = new MenuItem("속성");

                m.MenuItems.Add(m1);
                m.MenuItems.Add(m2);
                m.MenuItems.Add(m3);
                m1.Click += listBox_Right_contextMenu_Delete_Click;

                _selectedItem = listView_Left.GetItemAt(e.X, e.Y);
                
                if (_selectedItem != null)
                {
                    m.Show((Control)sender, new Point(e.X, e.Y));
                }
            }
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            if (listView_Left.Columns.Count > 0) {
                listView_Left.Columns[0].Width = listView_Left.Width-5;
            }
                
        }

        private void textBox_ipOrDomain_Click(object sender, EventArgs e)
        {
            if(textBox_ipOrDomain.Text == initText) {
                textBox_ipOrDomain.Text = "";
                initText = "";
            }
        }

        private void listView_Left_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _selectedItem = e.Item;
        }

        private void listView_Left_KeyUp(object sender, KeyEventArgs e)
        {
            if (_selectedItem == null)
                return;

            if (e.KeyValue == 46/* Delete */)
            {
                if (MessageBox.Show(_selectedItem.Text + "을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //왼쪽 리스트뷰에서 삭제
                    listView_Left.Items.Remove(_selectedItem);

                    //DB에서 삭제
                    string sql = string.Format("DELETE FROM pingtable WHERE domainname = '{0}';", _selectedItem.Text);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    int rst = cmd.ExecuteNonQuery();

                    sql = string.Format("DELETE FROM domain_list WHERE domainname = '{0}';", _selectedItem.Text);
                    cmd = new SQLiteCommand(sql, conn);
                    rst = cmd.ExecuteNonQuery();

                    //초기화
                    _selectedItem = null;
                    listView_Right.Items.Clear();
                }
            }
        }

        private void listBox_Right_contextMenu_Delete_Click(object sender, EventArgs e)
        {
            if (_selectedItem == null)
                return;

            if (MessageBox.Show(_selectedItem.Text + "을 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //왼쪽 리스트뷰에서 삭제
                listView_Left.Items.Remove(_selectedItem);

                //DB에서 삭제
                string sql = string.Format("DELETE FROM pingtable WHERE domainname = '{0}';", _selectedItem.Text);
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                int rst = cmd.ExecuteNonQuery();

                //초기화
                _selectedItem = null;
                listView_Right.Items.Clear();
            }
        }

        private void textBox_ipOrDomain_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13 /* Enter */)
                button1_Click(null, null);
        }



        /// <summary>
        /// PING을 날리고 결과를 Row class로 반환한다.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Row DoPing(string domain)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(domain, timeout, buffer, options);

            Row row = new Row();
            row.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            row.DomainName = domain;
            row.IPAddr = Dns.GetHostAddresses(domain)[0].ToString();
            row.SetPingOK(0);

            if (reply.Status == IPStatus.Success)
            {
                row.RoundTripTime = reply.RoundtripTime;
                row.TimeToLive = reply.Options.Ttl;
                row.DonotFragment = reply.Options.DontFragment;
                row.SetPingOK(1);
            }
            return row;
        }

        private void listView_Left_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void listView_Left_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                return;

            if (listView_Left.SelectedItems.Count == 1)
            {
                _selectedItem = listView_Left.SelectedItems[0];
                string selectedItemText = _selectedItem.Text;

                try
                {

                    listView_Right.Items.Clear();

                    string sql = string.Format("SELECT datetime, domainname, ip, RTL, TTL, DNF, ping_ok FROM pingtable WHERE domainname='{0}' ORDER BY datetime ASC;", selectedItemText);

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Row r = new Row();
                        r.DateTime = rdr["datetime"].ToString();
                        r.DomainName = rdr["domainname"].ToString();
                        r.IPAddr = rdr["ip"].ToString();
                        r.RoundTripTime = int.Parse(rdr["RTL"].ToString());
                        r.TimeToLive = int.Parse(rdr["TTL"].ToString());
                        r.SetDonotFragment(int.Parse(rdr["DNF"].ToString()));
                        r.SetPingOK(int.Parse(rdr["ping_ok"].ToString()));

                        var row = new ListViewItem(r.ToStringArray());
                        listView_Right.Items.Add(row);

                    }

                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {

                    listView_Right.Items.Clear();

                    string sql = string.Format("SELECT datetime, domainname, ip, RTL, TTL, DNF, ping_ok FROM pingtable ORDER BY datetime ASC;");

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Row r = new Row();
                        r.DateTime = rdr["datetime"].ToString();
                        r.DomainName = rdr["domainname"].ToString();
                        r.IPAddr = rdr["ip"].ToString();
                        r.RoundTripTime = int.Parse(rdr["RTL"].ToString());
                        r.TimeToLive = int.Parse(rdr["TTL"].ToString());
                        r.SetDonotFragment(int.Parse(rdr["DNF"].ToString()));
                        r.SetPingOK(int.Parse(rdr["ping_ok"].ToString()));

                        var row = new ListViewItem(r.ToStringArray());
                        listView_Right.Items.Add(row);

                    }

                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {

                    listView_Right.Items.Clear();

                    if (_selectedItem == null)
                        return;

                    string selectedItemText = _selectedItem.Text;

                    string sql = string.Format("SELECT datetime, domainname, ip, RTL, TTL, DNF, ping_ok FROM pingtable WHERE domainname='{0}' ORDER BY datetime ASC;", selectedItemText);

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Row r = new Row();
                        r.DateTime = rdr["datetime"].ToString();
                        r.DomainName = rdr["domainname"].ToString();
                        r.IPAddr = rdr["ip"].ToString();
                        r.RoundTripTime = int.Parse(rdr["RTL"].ToString());
                        r.TimeToLive = int.Parse(rdr["TTL"].ToString());
                        r.SetDonotFragment(int.Parse(rdr["DNF"].ToString()));
                        r.SetPingOK(int.Parse(rdr["ping_ok"].ToString()));

                        var row = new ListViewItem(r.ToStringArray());
                        listView_Right.Items.Add(row);

                    }

                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void listView_Right_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // 방향 초기화
            for (int i = 0; i < listView_Right.Columns.Count; i++)
            {
                listView_Right.Columns[i].Text = listView_Right.Columns[i].Text.Replace(" △", "");
                listView_Right.Columns[i].Text = listView_Right.Columns[i].Text.Replace(" ▽", "");
            }

            // DESC
            if (this.listView_Right.Sorting == SortOrder.Ascending || listView_Right.Sorting == SortOrder.None)
            {
                this.listView_Right.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                listView_Right.Sorting = SortOrder.Descending;
                listView_Right.Columns[e.Column].Text = listView_Right.Columns[e.Column].Text + " ▽";
            }
            // ASC
            else
            {
                this.listView_Right.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                listView_Right.Sorting = SortOrder.Ascending;
                listView_Right.Columns[e.Column].Text = listView_Right.Columns[e.Column].Text + " △";
            }
            listView_Right.Sort();

            // 컬럼 갯수가 변경되는 구조라면 sorter를 null 처리하여야 함
            listView_Right.ListViewItemSorter = null;

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            //타이머 초기화
            if (isTimer1Running == false)
            {
                timer1.Interval = int.Parse(textBox_interval.Text) * 1000;
                timer1.Start();
                isTimer1Running = true;
                btnStartStop.Text = "■";

                foreach (ListViewItem item in listView_Left.Items)
                {
                    item.Selected = false;
                }

                if(listView_Left.Items.Count > 0)
                {
                    listView_Left.Items[0].Selected = true;
                    _selectedItem = listView_Left.Items[0];
                    listView_Left.Select();
                }
                

            }
            else
            {
                timer1.Stop();
                isTimer1Running = false;
                btnStartStop.Text = "▶";
            }
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //메인폼 왼쪽 리스트뷰에서 도메인 목록을 가져와서 순서대로 데이터를 수집한다.
            foreach( ListViewItem domain in listView_Left.Items)
            {
                try
                {
                    Row row = DoPing(domain.Text);

                    //---- 오른쪽 리스트뷰에 추가 ----
                    //모두보기 해제(개별보기)일 때, 선택된 도메인만 오른쪽 리스트뷰에 추가
                    if (checkBox1.Checked == true)
                    {
                        var newRowItem = new ListViewItem(row.ToStringArray());
                        listView_Right.Items.Add(newRowItem);
                        if (this.isFollowLastRow)
                            this.listView_Right.Items[listView_Right.Items.Count - 1].EnsureVisible();
                    }
                    else if (checkBox1.Checked == false && _selectedItem.Text == domain.Text)
                    {
                        var newRowItem = new ListViewItem(row.ToStringArray());
                        listView_Right.Items.Add(newRowItem);
                        if(this.isFollowLastRow)
                            this.listView_Right.Items[listView_Right.Items.Count - 1].EnsureVisible();
                    }


                    //---- DB에 추가 ----
                    string sql = string.Format("INSERT INTO pingtable (datetime, domainname, ip, RTL, TTL, DNF, ping_ok) VALUES ('{0}', '{1}', '{2}', {3}, {4}, {5}, {6});"
                        , row.DateTime, row.DomainName, row.IPAddr, row.RoundTripTime, row.TimeToLive, row.GetDonotFragment_ToNumber(), row.GetPingOK_ToNumber());
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    int rst = cmd.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnSeeLastRow_Click(object sender, EventArgs e)
        {
            if (isFollowLastRow == true)
            {
                isFollowLastRow = false;
            }
            else
            {
                isFollowLastRow = true;
            }

        }

        private void button_save2csv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV (Comma Separated Value)|*.csv";
            saveFileDialog1.Title = "Save an CSV File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                StreamWriter fs = new StreamWriter(saveFileDialog1.OpenFile(), Encoding.UTF8);


                try
                {
                    string sql = string.Format("SELECT datetime, domainname, ip, RTL, TTL, DNF, ping_ok FROM pingtable ORDER BY datetime ASC;");

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    fs.WriteLine(Row.GetCSVHeader());

                    while (rdr.Read())
                    {
                        Row r = new Row();
                        r.DateTime = rdr["datetime"].ToString();
                        r.DomainName = rdr["domainname"].ToString();
                        r.IPAddr = rdr["ip"].ToString();
                        r.RoundTripTime = int.Parse(rdr["RTL"].ToString());
                        r.TimeToLive = int.Parse(rdr["TTL"].ToString());
                        r.SetDonotFragment(int.Parse(rdr["DNF"].ToString()));
                        r.SetPingOK(int.Parse(rdr["ping_ok"].ToString()));

                        fs.WriteLine(r.ToStringCSV());
                    }

                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                fs.Close();
            }
        }

    }
}
