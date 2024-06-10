// built-in
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.EntityFrameworkCore;
global using ServiceCollector.Abstractions;
global using Microsoft.Extensions.Options;
global using System.Security.Cryptography;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using System.Security.Claims;
global using ServiceCollector.Core;
global using System.Text.Json;
global using System.Text;
global using System.Net;

// third-party
global using FluentValidation;
global using Carter;

// solution
global using DigitalWallet.Features.Account.Common.Exceptions;
global using DigitalWallet.Features.Wallet.Common.Exceptions;
global using DigitalWallet.Features.Wallet.GetTransactions;
global using DigitalWallet.Features.Wallet.AddBlockBalance;
global using DigitalWallet.Common.Shared.ExceptionHandler;
global using DigitalWallet.Features.Wallet.MoneyTransfer;
global using DigitalWallet.Features.Wallet.CreateWallet;
global using DigitalWallet.Features.Account.CreateUser;
global using DigitalWallet.Features.Account.LoginUser;
global using DigitalWallet.Features.Wallet.GetWallet;
global using DigitalWallet.Common.Shared.Exceptions;
global using DigitalWallet.Features.Account.Common;
global using DigitalWallet.Features.Wallet.Common;
global using DigitalWallet.Common.Extensions;
global using DigitalWallet.Common.Filters;
global using DigitalWallet.Common.Data;
global using DigitalWallet.Common;
