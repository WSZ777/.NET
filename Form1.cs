using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 第五次作业
{
    public partial class Form1 : Form
    {
        private static readonly object lockObject = new object();

        private List<string> crawledUrls;
        public Form1()
        {
            InitializeComponent();
            crawledUrls = new List<string>();
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("搜索按钮被点击");

            string keyword = keywordTextBox.Text;

            crawledUrls.Clear();
            crawledUrlsListBox.Items.Clear();

            await Crawl(keyword);
        }
        private async Task Crawl(string keyword)
        {
            // 使用搜索引擎搜索关键字，并获取搜索结果页面的URL列表
            List<string> searchResults = await SearchEngine.Search(keyword);

            // 创建任务列表，用于并行处理每个搜索结果页面
            List<Task> tasks = new List<Task>();

            foreach (string url in searchResults)
            {
                // 启动任务，爬取每个搜索结果页面上的电话号码
                tasks.Add(Task.Run(() => CrawlPage(url)));
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks);

            // 显示爬取的URL
            foreach (string url in crawledUrls)
            {
                crawledUrlsListBox.Items.Add(url);
            }
        }

        private void CrawlPage(string url)
        {
            // 下载网页内容
            string html = DownloadHtml(url);
            Console.WriteLine("爬虫中" + url);
            // 使用正则表达式匹配电话号码
            List<string> phoneNumbers = ExtractPhoneNumbers(html);

            // 记录电话号码及其对应的URL
            foreach (string phoneNumber in phoneNumbers)
            {
                lock (lockObject)
                {
                    if (!crawledUrls.Contains(phoneNumber))
                    {
                        crawledUrls.Add(phoneNumber + " - " + url);
                    }
                }
            }
        }

        private string DownloadHtml(string url)
        {
            using (WebClient client = new())
            {
                return client.DownloadString(url);
            }
        }

        private List<string> ExtractPhoneNumbers(string html)
        {
            List<string> phoneNumbers = new List<string>();

            // 使用正则表达式匹配电话号码（示例正则表达式可能不完善，请根据实际情况修改）
            Regex regex = new Regex(@"\d{11}");
            MatchCollection matches = regex.Matches(html);

            foreach (Match match in matches)
            {
                phoneNumbers.Add(match.Value);
            }

            return phoneNumbers;
        }
    }

    public static class SearchEngine
    {
        public static Task<List<string>> Search(string keyword)
        {
            // 使用搜索引擎搜索关键字，返回搜索结果页面的URL列表
            // 在此使用异步方法模拟搜索过程
            return Task.Run(() =>
            {
                List<string> searchResults = new List<string>();

                // 模拟搜索过程...
                for (int i = 1; i <= 10; i++)
                {
                    string url = $"https://www.baidu.com/?tn=62095104_27_oem_dg&ch=1={keyword}&page={i}";
                    searchResults.Add(url);
                }

                // 模拟搜索延迟
                Thread.Sleep(2000);

                return searchResults;
            });
        }
    }
}