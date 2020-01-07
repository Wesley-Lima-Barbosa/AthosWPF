using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AthosWPF.Model;
using System.Net.Http.Headers;

namespace AthosWPF.Service
{
    class UsuarioService
    {
        const string urlAPI = "https://api.backendless.com/5B47E127-88D3-2562-FF22-589138DA6B00/AB3D4F84-00B8-4787-FF90-0527E5132500/data/";
        public async Task<List<Usuario>> UsuarioListar()
        {
            try
            {
                HttpClient client = new HttpClient();
                //envia o conteúdo para a url informada
                
                HttpResponseMessage response = await client.GetAsync(urlAPI + "usuario");

                //se foi com sucesso
                if (response.IsSuccessStatusCode)
                {
                    string mensagemRetorno = await response.Content.ReadAsStringAsync();
                    List<Usuario> usuarioRetorno = JsonConvert.DeserializeObject<List<Usuario>>(mensagemRetorno);
                    return usuarioRetorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //caso ocorra algum erro.
                return null;
            }
        }

        public async Task<List<Condominio>> CondominioListar()
        {
            try
            {
                HttpClient client = new HttpClient();
                //envia o conteúdo para a url informada
                HttpResponseMessage response = await client.GetAsync(urlAPI + "condominio");

                //se foi com sucesso
                if (response.IsSuccessStatusCode)
                {
                    string mensagemRetorno = await response.Content.ReadAsStringAsync();
                    List<Condominio> condominioRetorno = JsonConvert.DeserializeObject<List<Condominio>>(mensagemRetorno);
                    return condominioRetorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //caso ocorra algum erro.
                return null;
            }
        }

        public async Task<List<Administradora>> AdministradoraListar()
        {
            try
            {
                HttpClient client = new HttpClient();
                //envia o conteúdo para a url informada
                HttpResponseMessage response = await client.GetAsync(urlAPI + "administradora");

                //se foi com sucesso
                if (response.IsSuccessStatusCode)
                {
                    string mensagemRetorno = await response.Content.ReadAsStringAsync();
                    List<Administradora> administradoraRetorno = JsonConvert.DeserializeObject<List<Administradora>>(mensagemRetorno);
                    return administradoraRetorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //caso ocorra algum erro.
                return null;
            }
        }

        public async Task<List<Email>> EmailListar()
        {
            try
            {
                HttpClient client = new HttpClient();
                //envia o conteúdo para a url informada
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(urlAPI + "email");
                //se foi com sucesso
                if (response.IsSuccessStatusCode)
                {
                    string mensagemRetorno = await response.Content.ReadAsStringAsync();
                    List<Email> emailRetorno = JsonConvert.DeserializeObject<List<Email>>(mensagemRetorno);
                    return emailRetorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //caso ocorra algum erro.
                return null;
            }
        }

        public async Task<Email> Alterar(Email item)
        {
            //converte o objeto para uma string no formato json
            string json = JsonConvert.SerializeObject(item);

            //converte a string para um conteúdo a ser enviado
            StringContent conteudo = new StringContent(
                json, Encoding.UTF8, "application/json");

            try
            {
                HttpClient client = new HttpClient();
                //envia o conteúdo para a url informada
                HttpResponseMessage response = await client.PutAsync(
                    urlAPI + "email", conteudo);

                //se foi com sucesso
                if (response.IsSuccessStatusCode)
                {
                    string mensagemRetorno = await response.Content.ReadAsStringAsync();
                    Email emailRetorno = JsonConvert.DeserializeObject<Email>(mensagemRetorno);
                    return emailRetorno;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //caso ocorra algum erro.
                return null;
            }
        }
    }

}
