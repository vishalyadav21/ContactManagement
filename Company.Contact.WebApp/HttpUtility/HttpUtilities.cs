using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Company.Contact.WebApp.HttpUtility
{
    public interface IHttpUtilities<T>
    {
        IEnumerable<Contacts.DomainEntities.Contact> Get();
        Contacts.DomainEntities.Contact Get(string url);

        bool PostRequest(string apiUrl, Contacts.DomainEntities.Contact postObject);

        bool PutRequest(string apiUrl, Contacts.DomainEntities.Contact putObject);

        bool DeleteRequest(string apiUrl, int id);
    }

    public class HttpUtilities<T> : IHttpUtilities<T> where T : class
    {
        private const string ContentType = "application/json";
        public IEnumerable<Contacts.DomainEntities.Contact> Get()
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.Accept, ContentType);
                var response = webClient.DownloadString("http://localhost:56048/v1/contacts");
                var contacts = JsonConvert.DeserializeObject<IEnumerable<Contacts.DomainEntities.Contact>>(response);
                return contacts;
            }
            catch (Exception ex)
            {   
                return new List<Contacts.DomainEntities.Contact>();
            }
        }
        public Contacts.DomainEntities.Contact Get(string url)
        {
            try
            {
                var webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.Accept, ContentType);
                var response = webClient.DownloadString("http://localhost:56048/v1" + url);
                return JsonConvert.DeserializeObject<Contacts.DomainEntities.Contact>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool PostRequest(string apiUrl, Contacts.DomainEntities.Contact postObject)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = ContentType;
                    var apiUrl2 = "http://localhost:56048/v1/contacts/";
                    string data = JsonConvert.SerializeObject(postObject);
                    var response = webClient.UploadString(apiUrl2, data);
                    var result = JsonConvert.DeserializeObject<bool>(response);
                    return result;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PutRequest(string apiUrl, Contacts.DomainEntities.Contact putObject)
        {
            try
            {   
                var apiUrl2 = "http://localhost:56048/v1/contacts/";
                string data = JsonConvert.SerializeObject(putObject);
                var bytes = Encoding.ASCII.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl2);
                request.Method = "PUT";
                request.ContentType = ContentType;
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteRequest(string apiUrl, int id)
        {
            try
            {
                var apiUrl2 = "http://localhost:56048/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl2);
                    var response = client.DeleteAsync("v1/contacts/"+id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                        return false;
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

