using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oid_insert
{
    public partial class Form1 : Form
    {
        DBCON.Class1 DBCON = new DBCON.Class1();
        string db = "Server=192.168.0.190; Database=cs; User id=nms; Password=P@ssw0rd";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string txt1 = "";
            //foreach (Control c in groupBox1.Controls)
            //{
            //    if (c.Text != "")
            //    {
            //        //MessageBox.Show(c.Name);
            //        txt1 += c.Text + ",";
            //    }

            //}
            //string txt2 = "";
            //foreach (Control c in groupBox2.Controls)
            //{
            //    if (c.Text != "")
            //    {
            //        //MessageBox.Show(c.Text);
            //        txt2 += c.Text + ",";
            //    }

            //}
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("모델명을 입력해주세요");
                }
                else if (textBox1.Text == "")
                {
                    MessageBox.Show("OID를 입력해주세요");
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("OID를 입력해주세요");
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("OID를 입력해주세요");
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("OID를 입력해주세요");
                }
                //else if (comboBox1.Text == "")
                //{
                //    MessageBox.Show("분류를 골라주세요");
                //}
                else
                {
                    string[] gu1 = textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //Port Name
                    string[] gu2 = textBox2.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    string[] gu3 = textBox3.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //Port status
                    string[] gu4 = textBox4.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //Interface-in
                    string[] gu5 = textBox5.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //Interface-out
                    //string[] gu3 = textBox32.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < gu1.Length; i++)
                    {
                        //MessageBox.Show(gu1[i].ToString());   
                        //MessageBox.Show(gu2[0].ToString());
                        //Server=192.168.0.190; Database=cs; User id=nms; Password=P@ssw0rd
                        MySqlConnection CON = new MySqlConnection(db);
                        CON.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into all_oid(oid,model,description) " +
                            "select @oid,@model,@description";
                        cmd.Parameters.Add("@oid", MySqlDbType.VarChar, 50).Value = gu1[i].ToString();
                        cmd.Parameters.Add("@model", MySqlDbType.VarChar, 50).Value = gu2[0].ToString();
                        //cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = comboBox1.Text;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = "Port Name";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                        CON.Dispose();
                    }

                    for (int i = 0; i < gu3.Length; i++)
                    {
                        //MessageBox.Show(gu1[i].ToString());   
                        //MessageBox.Show(gu2[0].ToString());
                        //Server=192.168.0.190; Database=cs; User id=nms; Password=P@ssw0rd
                        MySqlConnection CON = new MySqlConnection(db);
                        CON.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into all_oid(oid,model,description) " +
                            "select @oid,@model,@description";
                        cmd.Parameters.Add("@oid", MySqlDbType.VarChar, 50).Value = gu3[i].ToString();
                        cmd.Parameters.Add("@model", MySqlDbType.VarChar, 50).Value = gu2[0].ToString();
                        //cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = comboBox1.Text;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = "Port status";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                        CON.Dispose();
                    }

                    for (int i = 0; i < gu4.Length; i++)
                    {
                        //MessageBox.Show(gu1[i].ToString());   
                        //MessageBox.Show(gu2[0].ToString());
                        //Server=192.168.0.190; Database=cs; User id=nms; Password=P@ssw0rd
                        MySqlConnection CON = new MySqlConnection(db);
                        CON.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into all_oid(oid,model,description) " +
                            "select @oid,@model,@description";
                        cmd.Parameters.Add("@oid", MySqlDbType.VarChar, 50).Value = gu4[i].ToString();
                        cmd.Parameters.Add("@model", MySqlDbType.VarChar, 50).Value = gu2[0].ToString();
                        //cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = comboBox1.Text;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = "Interface-in";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                        CON.Dispose();
                    }

                    for (int i = 0; i < gu5.Length; i++)
                    {
                        //MessageBox.Show(gu1[i].ToString());   
                        //MessageBox.Show(gu2[0].ToString());
                        //Server=192.168.0.190; Database=cs; User id=nms; Password=P@ssw0rd
                        MySqlConnection CON = new MySqlConnection(db);
                        CON.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into all_oid(oid,model,description) " +
                            "select @oid,@model,@description";
                        cmd.Parameters.Add("@oid", MySqlDbType.VarChar, 50).Value = gu5[i].ToString();
                        cmd.Parameters.Add("@model", MySqlDbType.VarChar, 50).Value = gu2[0].ToString();
                        //cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = comboBox1.Text;
                        cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50).Value = "Interface-out";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                        CON.Dispose();
                    }

                    MessageBox.Show("등록이 완료되었습니다.");
                    //foreach (Control c in groupBox1.Controls)
                    //{
                    //    if (c.Text != "")
                    //    {
                    //        c.Text = "";
                    //    }
                    //}

                    //foreach (Control c in groupBox2.Controls)
                    //{
                    //    if (c.Text != "")
                    //    {
                    //        c.Text = "";
                    //    }
                    //}

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    
                }

            }
            catch (Exception E)
            {
                //MessageBox.Show(E.Message);
            }

            Form1_Load(this, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
