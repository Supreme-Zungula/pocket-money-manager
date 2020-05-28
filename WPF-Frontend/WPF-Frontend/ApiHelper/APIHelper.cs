using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using WPF_Frontend.Models.Family;

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

        public async Task AddMember(FamilyMemberModel member)
        {
            using HttpResponseMessage response = await ApiClient.PostAsJsonAsync("api/familymember", member);
            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
        }

        #endregion

    }
}
