﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using MvcStartApp.Models.Db;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRequestRepository _repository;

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next, IRequestRepository requestRepository)
    {
        _next = next;
        _repository = requestRepository;
    }

    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // Для логирования данных о запросе используем свойста объекта HttpContext
        string logUrl = "http://" + context.Request.Host.Value + context.Request.Path;
        await Log(logUrl);
        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
    private async Task Log(string url)
    {
        if (_repository != null)
        {
            await _repository.AddLog(new Request() { Url = url });
        }
        Console.WriteLine($"LOG:  [{DateTime.Now}]: New request to http://{url}");
    }















    //private readonly RequestDelegate _next;

    ///// <summary>
    /////  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    ///// </summary>
    //public LoggingMiddleware(RequestDelegate next)
    //{
    //    _next = next;
    //}

    ///// <summary>
    /////  Необходимо реализовать метод Invoke  или InvokeAsync
    ///// </summary>
    //public async Task InvokeAsync(HttpContext context)
    //{
    //    LogConsole(context);
    //    await LogFile(context);

    //    // Передача запроса далее по конвейеру
    //    await _next.Invoke(context);
    //}
    //private void LogConsole(HttpContext context)
    //{
    //    // Для логирования данных о запросе используем свойста объекта HttpContext
    //    Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    //}

    //private async Task LogFile(HttpContext context)
    //{
    //    // Строка для публикации в лог
    //    string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

    //    // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
    //    string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

    //    // Используем асинхронную запись в файл
    //    await File.AppendAllTextAsync(logFilePath, logMessage);
    //}
}