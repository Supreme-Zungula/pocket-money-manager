using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.BankAccount;
using WPF_Frontend.Models.Family;
using WPF_Frontend.Models.Transactions;

namespace WPF_Frontend.ApiHelper
{
    public class APIHelper
    {
        private HttpClient ApiClient { get; set; }

        public APIHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:52654/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Get Methods

        public IEnumerable<UserModel> AllUsers()
        {
            HttpResponseMessage response = ApiClient.GetAsync("api/user/allUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<UserModel> people = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
                return people;
            }
            return null;
        }

        public IEnumerable<BankAccountModel> GetAllAccounts()
        {
            HttpResponseMessage response = ApiClient.GetAsync("api/BankAccounts").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<BankAccountModel> people = response.Content.ReadAsAsync<IEnumerable<BankAccountModel>>().Result;
                return people;
            }
            return null;
        }

        public IEnumerable<TransactionsModel> GetAllTransactions()
        {
            HttpResponseMessage response = ApiClient.GetAsync("api/Transaction/all").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<TransactionsModel> people = response.Content.ReadAsAsync<IEnumerable<TransactionsModel>>().Result;
                return people;
            }
            return null;
        }

        public UserModel LoginUser(string phone)
        {
            HttpResponseMessage response = ApiClient.GetAsync($"api/user/getbyphone/{phone}").Result;
            if (response.IsSuccessStatusCode)
            {
                UserModel user = response.Content.ReadAsAsync<UserModel>().Result;
                return user;
            }
            return null;
        }

        public UserModel GetUserByPhone(string phone)
        {
            HttpResponseMessage response = ApiClient.GetAsync($"api/user/getbyphone/{phone}").Result;
            if (response.IsSuccessStatusCode)
            {
                UserModel user = response.Content.ReadAsAsync<UserModel>().Result;
                return user;
            }
            return null;
        }

        public IEnumerable<FamilyMemberModel> GetAllFamilyMembers(int familyId)
        {
            HttpResponseMessage response = ApiClient.GetAsync($"api/familymember/getmembers/{familyId}").Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<FamilyMemberModel> members = response.Content.ReadAsAsync<IEnumerable<FamilyMemberModel>>().Result;
                return members;
            }
            return null;
        }

        public List<TransactionsModel> GetTransactionsById(string UserId)
        {
            IEnumerable<TransactionsModel> transactions = StoredTransactions.Transactions;
            if (transactions == null)
                return null;
            var result = from trans in transactions
                         where trans.AccountNo == UserId
                         select trans;
            return result.ToList<TransactionsModel>();
        }

        #endregion
        #region Post Methods

        public async Task RegisterUser(UserModel user)
        {
            using HttpResponseMessage response = await ApiClient.PostAsJsonAsync("api/user", user);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Registered Successfully");
            }
            else
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task SetBankAccount(BankAccountModel userDetails)
        {
            using HttpResponseMessage response = await ApiClient.PostAsJsonAsync("api/bankaccounts", userDetails);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Bank Account Created");
            }
            else
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task AddMember(FamilyMemberModel member)
        {
            UserModel setId = GetUserByPhone(member.Phone);
            member.Id = setId.Id;
            using HttpResponseMessage response = await ApiClient.PostAsJsonAsync("api/familymember", member);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task SendTransaction(TransactionsModel Details)
        {
            using HttpResponseMessage response = await ApiClient.PostAsJsonAsync("api/Transaction", Details);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transaction successful");
            }
            else
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }
        #endregion

        #region Put Methods
        public async Task UpdateMember(FamilyMemberModel member)
        {
            using HttpResponseMessage response = await ApiClient.PutAsJsonAsync("api/familymember", member);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task UpdateMember(UserModel member)
        {
            UserModel setPass = GetUserByPhone(member.Phone);
            member.Password = setPass.Password;
            member.Role = setPass.Role;
            using HttpResponseMessage response = await ApiClient.PutAsJsonAsync("api/user", member);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task UpdateAccount(BankAccountModel acc)
        {
            using HttpResponseMessage response = await ApiClient.PutAsJsonAsync($"api/BankAccounts/{acc.Id}", acc);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }
        #endregion

        #region Delete Methods
        public async Task DeleteMember(FamilyMemberModel member)
        {
            using HttpResponseMessage response = await ApiClient.DeleteAsync($"api/familymember/{member.Phone}");
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task DeleteMember(UserModel member)
        {
            using HttpResponseMessage response = await ApiClient.DeleteAsync($"api/user/{member.Phone}");
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        public async Task DeleteTransaction(TransactionsModel trans)
        {
            using HttpResponseMessage response = await ApiClient.DeleteAsync($"api/Transaction/{trans.Id}");
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }
        #endregion
    }
}