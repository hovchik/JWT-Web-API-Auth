﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Models
{
    public class EncryptingSymmetricKey : IJwtEncryptingEncodingKey, IJwtEncryptingDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;

        public string SigningAlgorithm { get; } = JwtConstants.DirectKeyUseAlg;

        public string EncryptingAlgorithm { get; } = SecurityAlgorithms.Aes256CbcHmacSha512;

        public EncryptingSymmetricKey(string key)
        {
            this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public SecurityKey GetKey() => this._secretKey;
    }
}