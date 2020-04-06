using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatAppClient.Service;

namespace ChatAppClient
{
    public partial class Form1 : Form
    {

        ClientService clientService = new ClientService();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String host = "localhost";
            int port = 2001;
            
            clientService.Connect(host,port);
        }
    }
}
