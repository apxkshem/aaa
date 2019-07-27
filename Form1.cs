using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;
using System.Windows.Media.Imaging;

namespace aaa
{
    public partial class Form1 : Form
    {
        Thread _gio_th_work = null;
        bool form_th_work_c = true;
        bool form_th_work_c1 = false;

        Dictionary<int, Test1> test1_dic = new Dictionary<int, Test1>();

        int form_wid = 0;
        int form_hei = 0;
        int file_count = 0;
        int panel_wid1 = 0;
        int panel_hei1 = 400;
        int panel_wid11 = 0;
        int panel_hei11 = 400;
        //PictureBox[] pictureBox1 = new PictureBox[100];
        //PictureBox pictureBox2 = new PictureBox();
        PictureBox[] pictureBox1 = null;
        //PictureBox[] pictureBox11 = null;
        Panel[] panel1 = null;
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //---------------------------------------
            //듀얼 모니터
            /*
            this.StartPosition = FormStartPosition.Manual;
            Rectangle fullScrenn_bounds = Rectangle.Empty;

            foreach (var screen in Screen.AllScreens)
            {
                fullScrenn_bounds = Rectangle.Union(fullScrenn_bounds, screen.Bounds);
            }
            this.ClientSize = new Size(fullScrenn_bounds.Width, fullScrenn_bounds.Height);
            this.Location = new Point(fullScrenn_bounds.Left, fullScrenn_bounds.Top);
            */
            //---------------------------------------
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < 100; i++)
            {
                Test1 test1 = new Test1();
                test1.name = string.Format("a{0}", i + 1);
                test1_dic.Add(i, test1);
            }

            foreach (KeyValuePair<int, Test1> temp in test1_dic)
            {
                Console.WriteLine("{0} {1} ", temp.Key, temp.Value.name);
            }

            for (int i = 0; i < test1_dic.Count; i++)
            {
                Console.WriteLine("{0} ", test1_dic[i].name);
            }
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int w = 0, h = 0, w_p=0;

            textBox1.Text = "10";
            textBox2.Text = "30";

            form_wid = Screen.PrimaryScreen.Bounds.Width;
            form_hei = Screen.PrimaryScreen.Bounds.Height;
            //--------------------------------
            //듀얼 모니터
            //form_wid = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            //form_hei = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            //--------------------------------

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.StartupPath + @"\\img");

            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                //if (File.Extension.ToLower().CompareTo(".xrv") == 0)
                //{
                    //String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    String FullFileName = File.FullName;

                //MessageBox.Show(FullFileName + " " + FileNameOnly);
                //}
                FileInfo f = new FileInfo(FullFileName);
                //DataTable dt_metadata = GetDate(f);
                GetDate(f, ref w, ref h);

                w_p = (w * panel_hei1) / h;

                Test1 test1 = new Test1();
                test1.name = FullFileName;
                test1.wid = w;
                test1.hei = h;
                test1.wid_r = w_p;
                test1.hei_r = panel_hei1;
                test1.loc_a = panel_wid1;
                test1_dic.Add(file_count, test1);

Console.WriteLine("{0}     {1} {2}    {3} {4}", file_count, w, h, w_p, panel_hei1);
                if (panel_wid1 < form_wid)
                    panel_wid11 += w_p;

                panel_wid1 += 10;
                panel_wid1 += w_p;

                file_count++;
            }

            pictureBox1 = new PictureBox[file_count];
            //pictureBox11 = new PictureBox[file_count];
            panel1 = new Panel[file_count];
            for (int i=0;i< file_count; i++)
            {
Console.WriteLine("panel loc {0}  {1}", i, test1_dic[i].loc_a);
                panel1[i] = new Panel();
                panel1[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                panel1[i].Location = new System.Drawing.Point(test1_dic[i].loc_a, 50);
                panel1[i].Name = "panel"+i;
                panel1[i].Size = new System.Drawing.Size(test1_dic[i].wid_r, panel_hei1);
                //panel1[i].BackColor = Color.Black;
                this.Controls.Add(panel1[i]);

                //------------------------
                pictureBox1[i] = new PictureBox();
                //pictureBox1[i].Location = new System.Drawing.Point(i * 500, 0);
                //pictureBox1[i].Location = new System.Drawing.Point(test1_dic[i].loc_a, 0);
                pictureBox1[i].Location = new System.Drawing.Point(0, 0);
                pictureBox1[i].Name = "pictureBox"+i;
                pictureBox1[i].Size = new System.Drawing.Size(test1_dic[i].wid_r, panel_hei1);
                pictureBox1[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                //pictureBox1[i].BackColor = Color.Black;
                panel1[i].Controls.Add(pictureBox1[i]);

                //pictureBox1[i].Load(Application.StartupPath + @"\\img\\" + (i + 1) + ".jpg");
                pictureBox1[i].Load(test1_dic[i].name);

                //----------------------------
                /*
                if (test1_dic[i].loc_a < form_wid)
                {
                    pictureBox11[i] = new PictureBox();

                    //pictureBox1[i].Location = new System.Drawing.Point(i * 500, 0);
                    pictureBox11[i].Location = new System.Drawing.Point(test1_dic[i].loc_a, 0);
                    pictureBox11[i].Name = "pictureBox2";
                    pictureBox11[i].Size = new System.Drawing.Size(test1_dic[i].wid_r, panel_hei11);
                    pictureBox11[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    pictureBox11[i].BackColor = Color.Black;
                    panel2.Controls.Add(pictureBox11[i]);

                    //pictureBox1[i].Load(Application.StartupPath + @"\\img\\" + (i + 1) + ".jpg");
                    pictureBox11[i].Load(test1_dic[i].name);
                }
                */
            }
            //---------------------------------
            //panel1.Size = new System.Drawing.Size(panel_wid1, panel_hei1);
            //panel2.Size = new System.Drawing.Size(panel_wid11, panel_hei11);


            //pictureBox1[1].Load(Application.StartupPath + @"\\beach2.jpg");


            form_th_work_c1 = true;

            _gio_th_work = new Thread(new ThreadStart(form_th_work)); // 탐지 폼 처리 확인
            _gio_th_work.Start();

        }
        private void form_th_work()
        {
            int x = 0, count1=0;
            int p11_p = 0;
            int w = 0;

            try
            {
                while (form_th_work_c)
                {
                    /*
                                        if (form_th_work_c1)
                                        {
                                                int k = 0;
                                                //Thread thread = new Thread(new ThreadStart(delegate () // thread 생성
                                                //{
                                                    //Invoke를 통해 lbl_Result 컨트롤에 결과값을 업데이트한다.
                                                    this.Invoke(new Action(delegate () // this == Form 이다. Form이 아닌 컨트롤의 Invoke를 직접호출해도 무방하다.
                                                    {
                    System.Diagnostics.Debug.WriteLine("count {0} {1}", count1, x);
                                                        while (true)
                                                        {
                                                            panel1.Location = new System.Drawing.Point(x, 50);

                                                            p11_p = x + panel_wid1 - 2;
                                                            if (p11_p <= form_wid)
                                                            {
                                                                panel2.Location = new System.Drawing.Point(p11_p, 50);
                                                                panel2.Update();

                                                                panel2.Show();
                                                            }
                                                            else
                                                                panel2.Hide();

                                                            panel1.Update();

                                                            if (x <= -panel_wid1)
                                                            {
                                                                x = 0;
                                                                count1 = 0;
                                                                break;
                                                            }

                                                            if (k >= test1_dic[count1].wid_r)
                                                            {
                                                                count1++;
                                                                break;
                                                            }

                                                            if(k < 5 || k > test1_dic[count1].wid_r - 5)
                                                                Thread.Sleep(1);
                                                            else
                                                                Thread.Sleep(30);

                                                            k++;
                                                            x -= 1;
                                                        }
                                                    }));
                                                //}));
                                                //thread.Start(); // thread 실행하여 병렬작업 시작
                                        }
                    */
                    if (form_th_work_c1)
                    {
                        int k = 0;
                        //Thread thread = new Thread(new ThreadStart(delegate () // thread 생성
                        //{
                        //Invoke를 통해 lbl_Result 컨트롤에 결과값을 업데이트한다.
System.Diagnostics.Debug.WriteLine("count {0} {1}", count1, x);
                        while (true)
                        {
                            this.Invoke(new Action(delegate () // this == Form 이다. Form이 아닌 컨트롤의 Invoke를 직접호출해도 무방하다.
                            {
                                w = 0;
                                for (int i = 0; i < file_count; i++)
                                {
                                    panel1[i].Location = new System.Drawing.Point(test1_dic[i].loc_a + x + (form_wid/2) - test1_dic[i].wid_r, 50);
//if(i==0)
//    System.Diagnostics.Debug.WriteLine("=== {0} {1} {2}    {3}", test1_dic[i].loc_a ,x ,form_wid, test1_dic[i].loc_a + x + form_wid);
                                    //if (w > form_wid)
                                    //    break;
                                    w += test1_dic[i].wid_r;
                                    panel1[i].Update();
                                }

                                //p11_p = x + panel_wid1 - 3;
                                /*
                                p11_p = x + panel_wid1 - 1;
                                if (p11_p <= form_wid)
                                {
                                    panel2.Location = new System.Drawing.Point(p11_p, 50);
                                    //panel2.Update();

                                    panel2.Show();
                                }
                                else
                                    panel2.Hide();
                                    */

                                //panel1.Update();

                            }));

                            if (x <= -(panel_wid1 - test1_dic[file_count - 1].wid_r))
                            {
                                x = 0;
                                count1 = 0;
                                //break;
                            }

                            if (k >= test1_dic[count1].wid_r)
                            {
                                count1++;
                                break;
                            }

                            if (k < 5 || k > test1_dic[count1].wid_r - 5)
                                Thread.Sleep(1);
                            else
                                Thread.Sleep(textBox2.Text == "" ? 1 : Convert.ToInt32(textBox2.Text));

                            k++;
                            x -= 1;
                        }
                        //}));
                        //thread.Start(); // thread 실행하여 병렬작업 시작
                    }
                    Thread.Sleep(1000 * (textBox1.Text == "" ? 1 : Convert.ToInt32(textBox1.Text)));
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message.ToString());
                //err_log(string.Format("{0}\r\n{1}", "em_sound_play()", ex.Message));
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form_th_work_c = false;
            if (_gio_th_work != null)
                //_gio_th_work.Join();
                _gio_th_work.Abort();

        }

        //private DataTable GetDate(FileInfo f)
        private void GetDate(FileInfo f, ref int wid, ref int hei)
        {
            using (FileStream fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read, FileShare.Read)) // 선택된 파일의 FileStream 을 생성합니다. 
            {
                //DataTable dt1 = new DataTable(); // 새로운 DataTable을 생성합니다.
                /*
                dt1.Columns.Add("attribute"); // 속성 이름을 저장합니다.
                dt1.Columns.Add("value"); // 속성 값을 저장합니다.*/

                BitmapSource img = BitmapFrame.Create(fs); // 선택된 파일의 FileStream 를 활용하여 BitmapSource 를 생성합니다.
                BitmapMetadata md = (BitmapMetadata)img.Metadata; // 

                // BitmapMetadata 에서 제공하는 모든 속성 값을 불러옵니다. 시작
                // https://msdn.microsoft.com/ko-kr/library/system.windows.media.imaging.bitmapmetadata(v=vs.110).aspx
                /*dt1.Rows.Add("ApplicationName", md.ApplicationName);
                dt1.Rows.Add("Author", md.Author);
                dt1.Rows.Add("CameraManufacturer", md.CameraManufacturer);
                dt1.Rows.Add("CameraModel", md.CameraModel);
                dt1.Rows.Add("CanFreeze", md.CanFreeze);
                dt1.Rows.Add("Comment", md.Comment);
                dt1.Rows.Add("Copyright", md.Copyright);
                dt1.Rows.Add("DateTaken", md.DateTaken);
                dt1.Rows.Add("DependencyObjectType", md.DependencyObjectType);
                dt1.Rows.Add("Dispatcher", md.Dispatcher);
                dt1.Rows.Add("Format", md.Format);
                dt1.Rows.Add("IsFixedSize", md.IsFixedSize);
                dt1.Rows.Add("IsFrozen", md.IsFrozen);
                dt1.Rows.Add("IsReadOnly", md.IsReadOnly);
                dt1.Rows.Add("IsSealed", md.IsSealed);
                dt1.Rows.Add("Keywords", md.Keywords);
                dt1.Rows.Add("Location", md.Location);
                dt1.Rows.Add("Rating", md.Rating);
                dt1.Rows.Add("Subject", md.Subject);
                dt1.Rows.Add("Title", md.Title);*/

                //dt1.Rows.Add("Width", img.Width);
                //dt1.Rows.Add("Height", img.Height);
                wid = (int)img.Width;
                hei = (int)img.Height;

                // BitmapMetadata 에서 제공하는 모든 속성 값을 불러옵니다. 끝

                //return dt1;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                Application.Exit();
            }
        }
    }
    public class Test1
    {
        public string name;
        public int wid;
        public int hei;
        public int wid_r;
        public int hei_r;
        public int loc_a;
    }

}