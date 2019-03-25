using ProjectC.BLL;
using ProjectC.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProjectC
{
    public partial class Form1 : Form
    {
        GuestBLL guestBLL;
        RoomBLL roomBLL;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DBConnection conn;
        SqlCommand cmd;
        string connString = @"uid=sa;pwd=123456789;Initial Catalog=Hotels;Data Source=NGHIALPGSE63314\LPGN";
        

        public Form1()
        {
            InitializeComponent();
            roomBLL = new RoomBLL();
            guestBLL = new GuestBLL();
            conn = new DBConnection();
            cmd = new SqlCommand();
            
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = roomBLL.GetRoomList().ToList();
            dataGridView2.DataSource = guestBLL.GetCustomerList().ToList();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = roomBLL.GetRoomList().ToList();
            dataGridView2.DataSource = guestBLL.GetCustomerList().ToList();
            dataGridView2.Columns["status"].Visible = false;
            dataGridView2.Columns["roomid"].Visible = false;
        }

       private void check()
        {
            string ch = "Select count(*) from login where username='" + txtRoomnumber.Text + "')";
        }

        private void bAddRoom_Click(object sender, EventArgs e)
        {
            int roomnumber = Convert.ToInt32(txtRoomnumber.Text);
            string status = cbStatus.Text;
            SqlConnection conn = new SqlConnection(connString);
            try
            {

                cmd = new SqlCommand("Select * from Room where Roomnumber='" + txtRoomnumber.Text + "'",conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if(i > 0)
                {
                    MessageBox.Show("Room " + txtRoomnumber.Text + " already exist");
                    ds.Clear();
                }
                 else if (cbStatus.Text == "Available" || cbStatus.Text == "In-used")
                {
                    roomBLL.insertRoom(roomnumber, status);
                    MessageBox.Show("Insert succesful");
                }
               
                else
                {
                    MessageBox.Show("Invalid");
                }
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bUpRoom_Click(object sender, EventArgs e)
        {
            string status = cbStatus.Text;
            int roomnumber = Convert.ToInt32(txtRoomnumber.Text);

            try
            {
                if (cbStatus.Text == "Available" || cbStatus.Text == "In-used")
                {
                    roomBLL.UpdateRoom(status, roomnumber);
                    MessageBox.Show("Update successfull");
                }
                else
                {
                    MessageBox.Show("Invalid");
                }
               
                                 
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bRemoveRoom_Click(object sender, EventArgs e)
        {
            int roomnumber = Convert.ToInt32(txtRoomnumber.Text);
            int roomid = Convert.ToInt32(txtRoomid.Text);
            try
            {
                if(roomnumber == roomid)
                {
                    MessageBox.Show("Roomnumber " + txtRoomnumber.Text + " is used");
                }
                else
                {
                    roomBLL.deleteRoom(roomnumber);
                    MessageBox.Show("Remove successfull");
                }
                                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
           
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtRoomnumber.Text = dataGridView1.SelectedRows[0].Cells["roomnumber"].Value.ToString();
                cbStatus.Text = dataGridView1.SelectedRows[0].Cells["status"].Value.ToString();
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                txtRoomid.Text = dataGridView2.SelectedRows[0].Cells["roomid"].Value.ToString();  
                txtPhone.Text = dataGridView2.SelectedRows[0].Cells["phone"].Value.ToString();
                txtID.Text = dataGridView2.SelectedRows[0].Cells["id"].Value.ToString();
                txtName.Text = dataGridView2.SelectedRows[0].Cells["name"].Value.ToString();
                txtAddress.Text = dataGridView2.SelectedRows[0].Cells["address"].Value.ToString();
              

            }
        }

        private bool Check()
        {
           
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1 = new SqlCommand("Select * from Guest where id='" + txtID.Text + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("Id " + txtID.Text + " already exist");
                    ds.Clear();
                }

            return Check();

        }

        private void bRegister_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            string name = txtName.Text;
            string address = txtAddress.Text;
            int phone = Convert.ToInt32(txtPhone.Text);
            int roomid = Convert.ToInt32(txtRoomid.Text);
            SqlConnection conn = new SqlConnection(connString);
           
            try
            {
                
                cmd = new SqlCommand("Select * from Guest where Roomid='" + txtRoomid.Text + "'", conn);
              
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                int i = ds.Tables[0].Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("Room " + txtID.Text + " is used");
                    ds.Clear();
                }
               
                else
                {
                    check();
                    guestBLL.insertGuest(id, name, address, phone, roomid);
                    MessageBox.Show("Insert successfull");
                } 
                         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            string name = txtName.Text;
            string address = txtAddress.Text;
            int phone = Convert.ToInt32(txtPhone.Text);
            int roomid = Convert.ToInt32(txtRoomid.Text);

            try
            {
                guestBLL.insertGuest(id, name, address, phone, roomid);
                MessageBox.Show("Update successfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            try
            {
               guestBLL.deleteCustomer(id);
                MessageBox.Show("Delete successfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GuestBLL p = new GuestBLL();
                dataGridView2.DataSource = p.GetPersons(Convert.ToInt32(txtSearch.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            room = roomBLL.getRoom(Convert.ToInt32(txtRoomnumber.Text));
           
        }

        private void txtRoomnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
           
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadCombobox()
        {
            GuestBLL objBll = new GuestBLL();
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "id";
            comboBox1.ValueMember = "roomid";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtRoomid.Clear();
            txtPhone.Clear();
        }
    }
}
