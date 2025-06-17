using Financial.Chat.Domain.Shared.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.Contracts.Implementations
{
    public class FinancialChatService : IFinancialChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public FinancialChatService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public IFinancialChatApi CreateApi()
        {
            var token = GetToken();
            var httpClient = CreateHttpClientForApi();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return RestService.For<IFinancialChatApi>(httpClient);
        }

        private string GetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }

        public HttpClient CreateHttpClientForApi() =>
            _httpClientFactory.CreateClient("financialchat");
    }
}
