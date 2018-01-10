using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Task
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog;
        FolderConvertor folderConvertor;
        public Form1()
        {
            InitializeComponent();

            this.folderBrowserDialog = new FolderBrowserDialog();
            this.openFileDialog = new OpenFileDialog();
            this.folderConvertor = new FolderConvertor();

          this.textBoxDeserializeSaveIn.Text=this.textBoxSaveIn.Text = Directory.GetCurrentDirectory();

           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if(folderBrowserDialog.ShowDialog()==DialogResult.OK)
            {
                this.SelectedFoledertextBox.Text = folderBrowserDialog.SelectedPath;
               
            }
        }

        

        private void Serializebutton_Click(object sender, EventArgs e)
        {
            if(this.progressBar1.MarqueeAnimationSpeed==200)
            {
                MessageBox.Show("You already serialize");
                return;
            }
            if(this.SelectedFoledertextBox.Text==String.Empty)
            {
                MessageBox.Show("You didn't select path");
            }
            else
            {
                WriteNameForm writeNameForm = new WriteNameForm();
                if (writeNameForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Task.Run(()=> serialize(this.SelectedFoledertextBox.Text, textBoxSaveIn.Text, writeNameForm.Filename));
                       

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        private void serialize(string p1,string p2,string p3)
        {
            progressBar1.Invoke(new Action(() => progressBar1.MarqueeAnimationSpeed = 200));

            folderConvertor.Serialize(p1, p2, p3);
            progressBar1.Invoke(new Action(() => progressBar1.MarqueeAnimationSpeed = 0));
            progressBar1.Invoke(new Action(() => progressBar1.Refresh()));


            MessageBox.Show("Serealization completed");
        }
        private void deserialize(string p1,string p2)
        {
            progressBar2.Invoke(new Action(() => progressBar2.MarqueeAnimationSpeed = 200));
            
            folderConvertor.Deserialize(p1, p2);
            progressBar2.Invoke(new Action(() => progressBar2.MarqueeAnimationSpeed = 0));
            progressBar2.Invoke(new Action(() => progressBar2.Refresh()));

            MessageBox.Show("Deserialization Completed");


        }



        private void buttonDeserialize_Click(object sender, EventArgs e)
        {
            if (this.progressBar2.MarqueeAnimationSpeed == 200)
            {
                MessageBox.Show("You already deserialize");
                return;
            }
            if (this.textBoxDeserializeSelectFile.Text == string.Empty)
            {
                MessageBox.Show("Youd Didn't select file");
            }
            else
            {
                try
                {
                    Task.Run(() => deserialize(this.textBoxDeserializeSelectFile.Text, this.textBoxDeserializeSaveIn.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(
                folderBrowserDialog.ShowDialog()==
                DialogResult.OK)
            {
                this.textBoxSaveIn.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                this.textBoxDeserializeSelectFile.Text = openFileDialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (
               folderBrowserDialog.ShowDialog() ==
               DialogResult.OK)
            {
                this.textBoxDeserializeSaveIn.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
