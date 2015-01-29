using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;
using Octokit;
using Repository = LibGit2Sharp.Repository;

namespace GitHubBackup
{
    public partial class Form1 : Form
    {
        private string _folderLocation;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            _folderLocation = folderBrowserDialog1.SelectedPath;
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            var userName = tbUserName.Text;
            if (string.IsNullOrEmpty(userName))
                return;
            if (Directory.Exists("repos") == false)
            {
                Directory.CreateDirectory("repos");
            }
            var github = new GitHubClient(new ProductHeaderValue("GitHubBackTester"));
            var repositories = await github.Repository.GetAllForUser(userName);
         
            foreach (var githubRepository in repositories)
            {
                var path = Path.Combine("repos", githubRepository.Name);
                Repository.Clone(githubRepository.HtmlUrl, path);
            }
        }
    }
}
