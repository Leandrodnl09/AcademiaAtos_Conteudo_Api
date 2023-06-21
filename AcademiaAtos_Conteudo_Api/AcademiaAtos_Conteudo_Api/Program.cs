using AcademiaAtos_Conteudo_Api; // Importando namespace AcademiaAtos_Conteudo_Api
using Newtonsoft.Json; // Importando namespace Newtonsoft.Json
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json; // Importando namespace System.Net.Http.Json
using System.Reflection.Metadata;
using System.Runtime.InteropServices; // Importando namespace System.Runtime.InteropServices

Console.WriteLine("Consumindo a API desenvolvida"); // Imprimindo mensagem no console
int op; // Declaração de variável op do tipo int
string BaseUrl = "http://localhost:5070/"; // Inicializando a variável BaseUrl com a URL base da API
string url = "https://localhost:7263/Primeira/";
string token = "";
Usuario user = new Usuario();

do // Início do loop do-while
{
    Console.WriteLine("Informe a opção desejada:"); // Imprimindo mensagem no console
    Console.WriteLine("1 - Consultar pessoas"); // Imprimindo mensagem no console
    Console.WriteLine("2 - Cadastrar pessoas"); // Imprimindo mensagem no console
    Console.WriteLine("3 - Alterar pessoas"); // Imprimindo mensagem no console
    Console.WriteLine("4 - Excluir pessoas"); // Imprimindo mensagem no console
    Console.WriteLine("5 - Solicitar TOKEN"); // Imprimindo mensagem no console
    Console.WriteLine("6 - Consultar API"); // Imprimindo mensagem no console
    Console.WriteLine("0 - Sair"); // Imprimindo mensagem no console
    op = int.Parse(Console.ReadLine()); // Lendo a opção do usuário e convertendo para int

    switch (op) // Início do switch para a opção selecionada
    {
        case 0: // Caso a opção seja 0
            break; // Sai do switch

        case 1: // Caso a opção seja 1
            List<Pessoa> pessoas = new List<Pessoa>(); // Declaração e inicialização de uma lista de pessoas
            HttpClient client = new HttpClient(); // Instanciando um objeto HttpClient
            client.BaseAddress = new Uri(BaseUrl); // Configurando o endereço base do cliente HTTP
            client.DefaultRequestHeaders.Accept.Clear(); // Limpando os cabeçalhos de requisição padrão
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("''", " "));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); // Adicionando o cabeçalho de aceitação para JSON

            HttpResponseMessage resposta = await client.GetAsync("api/Pessoa/pessoas"); // Enviando uma requisição GET para obter as pessoas

            if (resposta.IsSuccessStatusCode) // Se a resposta for bem-sucedida
            {
                var retorno = resposta.Content.ReadAsStringAsync().Result; // Lendo o conteúdo da resposta como uma string
                pessoas = JsonConvert.DeserializeObject<List<Pessoa>>(retorno); // Deserializando a string de retorno em uma lista de pessoas
            }
            else // Caso contrário
            {
                Console.WriteLine("Erro: " + resposta.StatusCode); // Imprimindo mensagem de erro com o código de status da resposta
            }
            foreach (Pessoa item in pessoas) // Iterando sobre a lista de pessoas
            {
                Console.WriteLine("ID: " + item.Id + "\nNome: " + item.Nome); // Imprimindo o ID e o nome de cada pessoa
            }
            break; // Sai do switch

        case 2: // Caso a opção seja 2
            Pessoa p = new Pessoa(); // Instanciando um novo objeto Pessoa
            Console.WriteLine("Digite o nome da pessoa:"); // Imprimindo mensagem no console
            p.Nome = Console.ReadLine(); // Lendo o nome da pessoa do usuário

            HttpClient cliente = new HttpClient(); // Instanciando um objeto HttpClient

            HttpResponseMessage respostaPost = await cliente.PostAsJsonAsync(BaseUrl + "api/Pessoa/pessoas", p); // Enviando uma requisição POST para cadastrar a pessoa

            Console.WriteLine("Resposta: " + respostaPost.StatusCode); // Imprimindo a resposta da requisição
            break; // Sai do switch

        case 3: // Caso a opção seja 3
            Console.WriteLine("Digite o ID da pessoa que deseja alterar:"); // Imprimindo mensagem no console
            int pessoaId = int.Parse(Console.ReadLine()); // Lendo o ID da pessoa a ser alterada

            HttpClient client3 = new HttpClient(); // Instanciando um objeto HttpClient
            client3.BaseAddress = new Uri(BaseUrl); // Configurando o endereço base do cliente HTTP
            client3.DefaultRequestHeaders.Clear(); // Limpando os cabeçalhos de requisição padrão
            client3.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); // Adicionando o cabeçalho de aceitação para JSON

            HttpResponseMessage responseGet = await client3.GetAsync("api/Pessoa/pessoas/" + pessoaId); // Enviando uma requisição GET para obter os detalhes da pessoa

            if (responseGet.IsSuccessStatusCode) // Se a resposta for bem-sucedida
            {
                var retornoGet = responseGet.Content.ReadAsStringAsync().Result; // Lendo o conteúdo da resposta como uma string
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(retornoGet); // Deserializando a string de retorno em um objeto Pessoa

                Console.WriteLine("Digite o novo nome da pessoa:"); // Imprimindo mensagem no console
                pessoa.Nome = Console.ReadLine(); // Lendo o novo nome da pessoa do usuário

                HttpResponseMessage responsePut = await client3.PutAsJsonAsync("api/Pessoa/pessoas/" + pessoaId, pessoa); // Enviando uma requisição PUT para alterar os dados da pessoa

                if (responsePut.IsSuccessStatusCode) // Se a resposta for bem-sucedida
                {
                    Console.WriteLine("Pessoa alterada com sucesso."); // Imprimindo mensagem de sucesso
                }
                else // Caso contrário
                {
                    Console.WriteLine("Erro: " + responsePut.StatusCode); // Imprimindo mensagem de erro com o código de status da resposta
                }
            }
            else // Caso contrário
            {
                Console.WriteLine("Erro: " + responseGet.StatusCode); // Imprimindo mensagem de erro com o código de status da resposta
            }

            break; // Sai do switch

        case 4: // Caso a opção seja 4
            Console.WriteLine("Digite o ID da pessoa que deseja excluir:"); // Imprimindo mensagem no console
            int pessoaIdExcluir = int.Parse(Console.ReadLine()); // Lendo o ID da pessoa a ser excluída

            HttpClient client4 = new HttpClient(); // Instanciando um objeto HttpClient
            client4.BaseAddress = new Uri(BaseUrl); // Configurando o endereço base do cliente HTTP
            client4.DefaultRequestHeaders.Clear(); // Limpando os cabeçalhos de requisição padrão
            client4.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); // Adicionando o cabeçalho de aceitação para JSON

            HttpResponseMessage responseDelete = await client4.DeleteAsync("api/Pessoa/pessoas/" + pessoaIdExcluir); // Enviando uma requisição DELETE para excluir a pessoa

            if (responseDelete.IsSuccessStatusCode) // Se a resposta for bem-sucedida
            {
                Console.WriteLine("Pessoa excluída com sucesso."); // Imprimindo mensagem de sucesso
            }
            else // Caso contrário
            {
                Console.WriteLine("Erro: " + responseDelete.StatusCode); // Imprimindo mensagem de erro com o código de status da resposta
            }
            break; // Sai do switch
        case 5:
            Console.WriteLine("Informe o usuário:");
            user.Username = Console.ReadLine();
            Console.WriteLine("Informe a senha:");
            user.Password = Console.ReadLine();

            HttpClient clientToken = new HttpClient();
            clientToken.DefaultRequestHeaders.Accept.Clear();
            clientToken.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("''", " "));
            clientToken.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage respToken = clientToken.PostAsJsonAsync(url + "autenticar", user).Result;

            if (respToken.StatusCode == HttpStatusCode.OK)
            {
                token = respToken.Content.ReadAsStringAsync().Result;

                Console.WriteLine(token.Replace("''", " "));
            }
            else
            {
                Console.WriteLine(respToken.StatusCode);
            }
            break;
        case 6:

            HttpClient client6 = new HttpClient();

            client6.BaseAddress = new Uri(url);
            client6.DefaultRequestHeaders.Accept.Clear();
            client6.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client6.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resp = client6.GetAsync("primeiro").Result;

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine(resp.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(resp.StatusCode);
            }
            break;
        default:
            break;
    }
    
} while (op != 0); // Continua o loop enquanto a opção for diferente de 0
