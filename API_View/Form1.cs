using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API;
using Newtonsoft.Json;
using ViewModels;

namespace API_View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        private async void GetAll()
        {
            using(var client = new HttpClient())
            {
                using(var response = await client.GetAsync("https://localhost:44329/api/Libros"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var LibrosJsonString = await response.Content.ReadAsStringAsync();
                        dataGridView1.DataSource = JsonConvert.DeserializeObject<List<LibrosViewModel>>(LibrosJsonString)
                            .ToList();
                    }
                    else
                    {
                        MessageBox.Show("cagaste, tu api no sirve");
                    }
                }
            }
        }
    }
}
