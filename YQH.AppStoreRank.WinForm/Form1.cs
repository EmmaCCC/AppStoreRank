using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YQH.AppStoreRank.BLL.Admin;
using YQH.AppStoreRank.Common;
using YQH.AppStoreRank.Data;
using YQH.AppStoreRank.Data.Models;
using Dapper;

namespace YQH.AppStoreRank.WinForm
{

    public class AccountComparer : IEqualityComparer<Account>
    {
        public bool Equals(Account x, Account y)
        {
            return x.Id != y.Id;
        }

        public int GetHashCode(Account obj)
        {
            return obj.Id.GetHashCode();
        }
    }
    public partial class Form1 : Form
    {

        private static string con = ConfigurationManager.ConnectionStrings["AppStoreRankContext"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] ids = new[] { "8be611b6-8b7c-4ea8-afc4-de50025a9295", "93aa04ee-8a0b-41b0-8a3e-f1c86935ade3", "409da1bc-2fd4-469d-af6b-ca764fdf6088", "a6a64013-b40a-40bb-8b6c-a6a675661331", "7add7e12-8e76-461e-a868-3d4a82f389ec" };

            //string[] ids = new[] { "178349ca-ab35-437b-89f8-29942563cd96" };
            for (int i = 0; i < 1; i++)
            {
                string id = ids[0];
                Task.Run(() =>
                {
                    GetTask(id);
                    //m1();
                });
            }
        }

        public static void m1()
        {
           
        }
        private static void GetTask(string id)
        {
            string url = "http://localhost:31575/api/TaskInfoOrder/Create";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "Post";
                //123213
                string postdata = "{\"orderId\":\"11764892-840F-4970-AF21-ADB088063615\",\"IDFA\":\"11\",\"id\":\"" + id + "\"}";
                byte[] bytes = Encoding.ASCII.GetBytes(postdata);
                request.ContentLength = bytes.Length;

                Stream sendStream = request.GetRequestStream();
                sendStream.Write(bytes, 0, bytes.Length);
                sendStream.Close();

                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    throw new NullReferenceException();
                }
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                var data = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int count = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            count++;

            try
            {

                List<Task> tasks = new List<Task>();
                for (int i = 4000; i < 8000; i++)
                {
                    RedisHelper redis = new RedisHelper();
                    var index = i;
                    Task.Run(() =>
                    {
                        redis.SetString(index.ToString(), "第" + index + "次", TimeSpan.FromHours(1));
                    });

                }
                Task.WhenAll(tasks);
                MessageBox.Show("成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var acc = new Account()
            {
                Id = Guid.NewGuid(),
                UserName = "并发测试",
                Password = "123",
                Type = Data.Enums.AccountType.工作室,
                Amount = 10000
            };

            using (var con = new AppStoreRankContext())
            {
                con.Accounts.Add(acc);
                con.SaveChanges();
            }

            var firContext = new AppStoreRankContext();
            //取第一条记录,并修改一个字段：这里是修改了FirstName
            //先不保存
            var p1 = firContext.Accounts.Find(acc.Id);
            p1.Amount = 123;
            using (var secContext = new AppStoreRankContext())
            {
                var p2 = secContext.Accounts.Find(acc.Id);
                p2.Amount = 456;
                secContext.SaveChanges();
            }
            try
            {
                firContext.SaveChanges();
                Console.WriteLine(" 保存成功");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Entries.First().Entity.GetType().Name + " 保存失败");
            }

            //var firstContext = new AppStoreRankContext();

            //var account = firstContext.Accounts.Find(Guid.Parse("c70b260a-2ca4-4aeb-a7d4-114afa9561f6"));
            //account.Amount = 5;
            //using (var secContext = new AppStoreRankContext())
            //{
            //    var secAccount = secContext.Accounts.Find(Guid.Parse("c70b260a-2ca4-4aeb-a7d4-114afa9561f6"));
            //    secAccount.Amount = 6;
            //    secContext.SaveChanges();
            //}
            //try
            //{
            //    firstContext.SaveChanges();
            //    MessageBox.Show("保存成功");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                IDbConnection connection = new SqlConnection(con);
                List<Account> accs = new List<Account>();
                Account account = new Account();
                account.Amount = 50000;
                account.Id = Guid.NewGuid();
                account.UserName = "dapper1111111";
                account.Password = "dapper11111111111";
                account.IsDisabled = false;
                account.Type = Data.Enums.AccountType.管理员;

                Account account2 = new Account();
                account2.Amount = 50000;
                account2.Id = Guid.NewGuid();
                account2.UserName = "dapper2222";
                account2.Password = "dapper22222222222";
                account2.IsDisabled = false;
                account2.Type = Data.Enums.AccountType.管理员;

                accs.Add(account);
                accs.Add(account2);

                string sql = "INSERT INTO Accounts(Id,UserName,Password,Amount,IsDisabled,Type) VALUES(@Id,@UserName,@Password,@Amount,@IsDisabled,@Type)";

                var result = connection.Execute(sql, accs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            IDbConnection connection = new SqlConnection(con);

            string sql = "SELECT * FROM Accounts a LEFT JOIN OrderInfoes o ON a.Id = o.UserId  WHERE Amount>@Amount1 AND Amount<@Amount2";

            Dictionary<Guid, Account> dicAccs = new Dictionary<Guid, Account>();

            var list = connection.Query<Account, OrderInfo, Account>(sql, (acc, order) =>
            {
               
                acc.OrderInfos.Add(order);
                order.Account = acc;
                return acc;
         

                //if (acc.Id == order.UserId)
                //    acc.OrderInfos.Add(order);

            }, new { Amount1 = 136, Amount2 = 138 },splitOn:"UserId").Distinct(new AccountComparer());

            //var decimals = new decimal[] {137, 50000};
            ////in 操作
            //string sql2 = "SELECT * FROM Accounts WHERE Amount in @Amounts";

            //var list2 = connection.Query<Account>(sql2, new {Amounts= decimals } );


            //多条sql一起执行
            string sql3 = "SELECT * FROM Accounts;SELECT Money,StartTime,EndTIme from OrderInfoes";

            var more = connection.QueryMultiple(sql3);

            var list3 = more.Read<Account>();
            var list4 = more.Read<OrderInfo>();

        }
    }
}
