using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;

namespace Apitesting
{
    public class Tests
    {
        string auth;
        string token = "";
        //= "{\"email\": \"user17@sector36.com\", \"password\": \"user@12023\"}";
        string baseurl = "http://api.qaauto.co.nz/api";
        string version = "v1";
        //string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOlwvXC9hcGkucWFhdXRvLmNvLm56XC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyMzIxODY4OSwiZXhwIjoxNjIzMjIyMjg5LCJuYmYiOjE2MjMyMTg2ODksImp0aSI6InNBNVZSaGlSb05SelpWZnQiLCJzdWIiOjU0LCJwcnYiOiIyM2JkNWM4OTQ5ZjYwMGFkYjM5ZTcwMWM0MDA4NzJkYjdhNTk3NmY3In0.1FNA2l4JscNhBgJqyYViGIkG84CdmkP48pUag5kcfzA";
        string email = "user17@sector36.com";
        string password = "user@12023";
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //string auth = "{\"email\": \"user17@sector36.com\", \"password\": \"user@12023\"}";
            User user = new User(email,password);
            auth = JsonConvert.SerializeObject(user);
            Auth();
        }

        public void Auth()
        { 
            var client = new RestClient($"{baseurl}/{version}/auth/login");
            //TestContext.WriteLine(Auth);
            var request = new RestRequest(Method.POST);

            //header
            request.AddHeader("Content-Type", "application/json");

            //how to set the body
            request.AddParameter("application/json", auth, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string jsondata = response.Content;
            Tocken data = JsonConvert.DeserializeObject<Tocken>(jsondata);
            token = data.access_token;
            //TestContext.WriteLine(tocken.access_token);
        }

        [Test]
        public void dummy()
        {
            TestContext.WriteLine(token);
        }

        [Test]
        public void GetDepartment()
        {

            string baseurl = "http://api.qaauto.co.nz/api";
            string version = "v1";
            string id = "2";

            var client = new RestClient($"{baseurl}/{version}/departments/{id}");
            //TestContext.WriteLine(Auth);
            var request = new RestRequest(Method.GET);

            //header
            request.AddHeader("Content-Type", "application/json");

            //how to set the body
            //request.AddParameter("application/json", Auth, ParameterType.RequestBody);
            request.AddHeader("Authorization", $"bearer {token}");
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);

        }

        [Test]
        public void PostDepartment()
        {

            string baseurl = "http://api.qaauto.co.nz/api";
            string version = "v1";
            string postbody = "{ \"name\": \"induss\"}";
            var client = new RestClient($"{baseurl}/{version}/departments");
            //TestContext.WriteLine(Auth);
            var request = new RestRequest(Method.POST);

            //header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"bearer {token}");
            //how to set the body
            request.AddParameter("application/json", postbody, ParameterType.RequestBody);
       
            IRestResponse response = client.Execute(request);
            TestContext.WriteLine(response.Content);
        }
    }
}